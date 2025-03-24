using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exemplo2.Models;
namespace Exemplo2.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario { Id = 1, Nome = "João", Email = "Joao@gmail.com" },
            new Usuario { Id = 2, Nome = "Maria", Email = "Maria@gmail.com" },
            new Usuario { Id = 3, Nome = "Jose", Email = "Jose@gmail.com" }
        };

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return usuarios;
        }

        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario);
            return usuario;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            var usuarioExistente = usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usuarioExistente = usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioExistente != null)
            {
                usuarios.Remove(usuarioExistente);
            }
        }
    }
}