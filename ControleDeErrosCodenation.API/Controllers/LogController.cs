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
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _repo;
        private readonly IMapper _mapper;

        public LogController(ILogRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/<LogController>
        [HttpGet]
        public ActionResult<IEnumerable<LogDTO>> GetActiveLogs()
        {
            return Ok(_mapper.Map<List<LogDTO>>(_repo.ActiveLogs()));
        }

        // GET: api/<LogController>/archived
        [Route("archived")]
        [HttpGet]
        public ActionResult<IEnumerable<LogDTO>> GetArchivedLogs()
        {
            return Ok(_mapper.Map<List<LogDTO>>(_repo.ArchivedLogs()));
        }


        // GET api/<LogController>/5
        [HttpGet("{id:int}")]
        public ActionResult<LogDTO> Get(int id)
        {
            var levelFound = _repo.SelecionarPorId(id);
            if (levelFound == null)
                return NotFound(new { error = new ArrayList() { new { Log = "Log not found!" } } });
            return Ok(_mapper.Map<LogDTO>(_repo.SelecionarPorId(id)));
        }

        // POST api/<LogController>
        [HttpPost]
        public ActionResult<Object> Post([FromBody] LogDTO logDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newLog = _mapper.Map<Log>(logDTO);
            ArrayList errorsList = new ArrayList();
            if (newLog.IdEnvironment == -1)
                errorsList.Add(new { Environment = "Environment '" + logDTO.Environment + "' not found!" });
            if (newLog.IdLevel == -1)
                errorsList.Add(new { Level = "Level '" + logDTO.Level + "' not found!" });
            if (errorsList.Count > 0)
                return BadRequest(new { error = errorsList });
            newLog.Date = DateTime.Now;
            newLog.Archived = false;
            _repo.Incluir(newLog);
            return Ok(new { success = "Log created!" });
        }

        // PUT api/<LogController>
        [HttpPut]
        public ActionResult<Object> Put([FromBody] LogDTO logDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newLog = _mapper.Map<Log>(logDTO);
            ArrayList errorsList = new ArrayList();
            if (newLog.Id == 0)
                errorsList.Add(new { Id = "The Id field is required!" });
            else
            {
                var levelFound = _repo.SelecionarPorId(logDTO.Id);
                if (levelFound == null)
                    errorsList.Add(new { Id = "Id '" + logDTO.Id + "' not found!" });
            }
            if (newLog.IdEnvironment == -1)
                errorsList.Add(new { Environment = "Environment '" + logDTO.Environment + "' not found!" });
            if (newLog.IdLevel == -1)
                errorsList.Add(new { Level = "Level '" + logDTO.Level + "' not found!" });
            if (errorsList.Count > 0)
                return BadRequest(new { error = errorsList });
            newLog.Date = DateTime.Now;
            _repo.Alterar(newLog);
            return Ok(new { success = "Log updated!" });
        }

        // DELETE api/<LogController>/5
        [HttpDelete("{id}")]
        public ActionResult<Object> Delete(int id)
        {
            var levelFound = _repo.SelecionarPorId(id);
            if (levelFound == null)
                return NotFound(new { error = new ArrayList() { new { Log = "Log not found!" } } });
            
            _repo.Excluir(id);
            return Ok(new { success = "Log deleted!" });
        }
    }
}
