using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleDeErrosCodenation.API.DTOs;
using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleDeErrosCodenation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelRepository _repo;
        private readonly ILogRepository _repoLogs;
        private readonly IMapper _mapper;

        public LevelController(ILevelRepository repo, ILogRepository repoLogs, IMapper mapper)
        {
            _repo = repo;
            _repoLogs = repoLogs;
            _mapper = mapper;
        }

        // GET: api/<LevelController>
        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetAll()
        {
            return Ok(_mapper.Map<List<LevelDTO>>(_repo.SelecionarTodos()));
        }

        // POST api/<LevelController>
        [HttpPost]
        public ActionResult<Object> Post([FromBody] LevelDTO levelDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var levelFound = _repo.SelecionarPorNome(levelDTO.Name);
            if (levelFound != null)
                return BadRequest(new { errors = new { Name = new ArrayList(){"Name '" + levelDTO.Name + "' already exists!" }} });
            _repo.Incluir(_mapper.Map<Level>(levelDTO));
            return Ok(new { success = "Level '" + levelDTO.Name + "' created!" });
        }

        // PUT api/<LevelController>
        [HttpPut]
        public ActionResult<Object> Put([FromBody] UpdateLevelDTO newLevelDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var levelFound = _repo.SelecionarPorNome(newLevelDTO.Name);
            if (levelFound == null)
                return NotFound(new { errors = new { Name = new ArrayList(){"Name '" + newLevelDTO.Name + "' not found!" }} });
            levelFound.Name = newLevelDTO.NewName;
            _repo.Alterar(levelFound);
            return Ok(new { success = "Level updated!" });
        }

        // DELETE api/<LevelController>
        [HttpDelete]
        public ActionResult<Object> Delete([FromBody] LevelDTO levelDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var levelFound = _repo.SelecionarPorNome(levelDTO.Name);
            if (levelFound == null)
                return NotFound(new { errors = new { Name = new ArrayList() { "Name '" + levelDTO.Name + "' not found!" } } });
            if(_repoLogs.SelecionarTodos().Where(x => x.IdLevel == levelFound.Id).ToList().Count > 0)
                return BadRequest(new { errors = new { Logs = new ArrayList() { "You cannot delete this level, there are logs linked to it! Delete all linked logs before deleting this level." } } });
            _repo.Excluir(levelFound.Id);
            return Ok(new { success = "Level '" + levelDTO.Name + "' deleted!" });
        }
    }
}
