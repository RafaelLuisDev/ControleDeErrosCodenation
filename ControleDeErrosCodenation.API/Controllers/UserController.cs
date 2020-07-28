using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using ControleDeErrosCodenation.API.DTOs;
using ControleDeErrosCodenation.API.Services;
using ControleDeErrosCodenation.Data.Repository;
using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeErrosCodenation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDTO userDTO)
        {
            User user = _repo.SelecionarUser(userDTO.Username, userDTO.Password);

            if (user == null)
                return NotFound(new { message = "Username or password invalid!" });

            var token = TokenService.GenerateToken(user);
            return Ok(new { success = "Token created!", token });
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Object> GetAllUsers()
        {
            return Ok(_repo.SelecionarTodos());
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Object> GetUser(int id)
        {
            return Ok(_repo.SelecionarPorId(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Object> PostUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            User newUser = _mapper.Map<User>(userDTO);
            newUser.Role = "User";
            _repo.Incluir(newUser);
            return Ok(new { success = "User created!" });
        }

        [HttpDelete]
        [Authorize]
        public ActionResult<Object> DeleteUser([FromBody] UserDTO userDTO)
        {
            var userFound = _repo.SelecionarUser(userDTO.Username, userDTO.Password);
            if (userFound == null)
                return NotFound(new { message = "Username or password invalid!" });
            if (userFound.Role.ToLower() != "user")
                return BadRequest(new { errors = new ArrayList() { new { message = "Informed user isn't a simple User! In this route, you only update simple Users!" } } });
            _repo.Excluir(userFound.Id);
            return Ok(new { success = "User deleted!" });
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public ActionResult<Object> Authenticated() => Ok(new { success = String.Format("Authenticated - {0}", User.Identity.Name) });


        // POST api/user/admin
        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<Object> PostAdmin([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            User newUser = _mapper.Map<User>(userDTO);
            newUser.Role = "Admin";
            _repo.Incluir(newUser);
            return Ok(new { success = "User Admin created!" });
        }

        [HttpPut]
        [Route("upgrade/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Object> UpgradeUser(int id)
        {
            var updateUser = _repo.SelecionarPorId(id);
            if (updateUser == null)
                return NotFound(new { message = "Id '" + id + "' not found!" });
            updateUser.Role = "Admin";
            _repo.Alterar(updateUser);
            return Ok(new { success = "User updated!" });
        }

        [HttpPut]
        [Route("downgrade/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Object> DowngradeUser(int id)
        {
            var updateUser = _repo.SelecionarPorId(id);
            if (updateUser == null)
                return NotFound(new { message = "Id '" + id + "' not found!" });
            updateUser.Role = "User";
            _repo.Alterar(updateUser);
            return Ok(new { success = "User updated!" });
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<Object> DeleteAdmin([FromBody] UserDTO userDTO)
        {
            var userFound = _repo.SelecionarUser(userDTO.Username, userDTO.Password);
            if (userFound == null)
                return NotFound(new { message = "Username or password invalid!" });
            if (_repo.SelecionarTodos().Count == 1)
                return BadRequest(new { errors = new ArrayList() { new { message = "This administrator could not be deleted. This is the last administrator and at least one administrator is required!" } } });
            _repo.Excluir(userFound.Id);
            return Ok(new { success = "User Admin deleted!" });
        }
    }
}
