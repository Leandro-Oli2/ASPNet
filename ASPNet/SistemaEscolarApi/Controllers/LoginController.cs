using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.Services;
using SistemaEscolarApi.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController  : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if(string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Usuario e Senha são obrigatórios.");
            }

            var users = new List<Usuario>{
                new Usuario { Username = "admin", Password = "admin", Role = "Administrador" },
                new Usuario { Username = "func", Password = "user", Role = "Funcionario" }
            };
            var user = users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            

            if(user == null){
                return Unauthorized(new { mensagem = "Usuario ou senha inválidos." });
            }

            var token = TokenService.GenerateToken(user);
            return Ok(new {token});
        }
    }
}