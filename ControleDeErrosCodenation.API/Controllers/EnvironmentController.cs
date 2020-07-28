using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ControleDeErrosCodenation.API.DTOs;
using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Environment = ControleDeErrosCodenation.Domain.Models.Environment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleDeErrosCodenation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentRepository _repo;
        private readonly ILogRepository _repoLogs;
        private readonly IMapper _mapper;

        public EnvironmentController(IEnvironmentRepository repo, ILogRepository repoLogs, IMapper mapper)
        {
            _repo = repo;
            _repoLogs = repoLogs;
            _mapper = mapper;
        }

        // GET: api/<EnvironmentController>
        [HttpGet]
        public ActionResult<IEnumerable<EnvironmentDTO>> GetAll()
        {
            return Ok(_mapper.Map<List<EnvironmentDTO>>(_repo.SelecionarTodos()));
        }

        // POST api/<EnvironmentController>
        [HttpPost]
        public ActionResult<Object> Post([FromBody] EnvironmentDTO environmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var envFound = _repo.SelecionarPorNome(environmentDTO.Name);
            if (envFound != null)
                return BadRequest(new { errors = new { Name = new ArrayList() { "Name '" + environmentDTO.Name + "' already exists!" } } });
            _repo.Incluir(_mapper.Map<Environment>(environmentDTO));
            return Ok(new { success = "Environment '" + environmentDTO.Name + "' created!" });
        }

        // PUT api/<EnvironmentController>
        [HttpPut]
        public ActionResult<Object> Put([FromBody] UpdateEnvironmentDTO newEnvironmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var envFound = _repo.SelecionarPorNome(newEnvironmentDTO.Name);
            if (envFound == null)
                return NotFound(new { errors = new { Name = new ArrayList() { "Name '" + newEnvironmentDTO.Name + "' not found!" } } });
            envFound.Name = newEnvironmentDTO.NewName;
            _repo.Alterar(envFound);
            return Ok(new { success = "Environment updated!" });
        }

        // DELETE api/<EnvironmentController>
        [HttpDelete]
        public ActionResult<Object> Delete([FromBody] EnvironmentDTO environmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var envFound = _repo.SelecionarPorNome(environmentDTO.Name);
            if (envFound == null)
                return NotFound(new { errors = new { Name = new ArrayList() { "Name '" + environmentDTO.Name + "' not found!" } } });
            if (_repoLogs.SelecionarTodos().Where(x => x.IdEnvironment == envFound.Id).ToList().Count > 0)
                return BadRequest(new { errors = new { Logs = new ArrayList() { "You cannot delete this environment, there are logs linked to it! Delete all linked logs before deleting this environment." } } });
            _repo.Excluir(envFound.Id);
            return Ok(new { success = "Environment '" + environmentDTO.Name + "' deleted!" });
        }
    }
}
