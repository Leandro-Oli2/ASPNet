using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Exemplo4.Database;
using Exemplo4.Models;
namespace Exemplo4.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context; 

        public UsuarioController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<IEnumerable<Usuario>> Get() 
        {
            
            return await _context.Usuarios.ToListAsync();  
        }

        [HttpPost] // Define que esse método é um POST
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario usuario) // Task é um método assíncrono, ActionResult é o tipo de retorno do método, [FromBody] indica que o usuário vai ser passado no corpo da requisição
        {
            _context.Usuarios.Add(usuario); // Adiciona o usuário no banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return usuario; // Retorna o usuário que foi adicionado
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<Usuario>> Put(int id, [FromBody] Usuario usuario) 
        {
            var existente = await _context.Usuarios.FindAsync(id); 
            if (existente == null) return NotFound(); 
            existente.Nome = usuario.Nome; 
            existente.Especialidade = usuario.Especialidade; 

            await _context.SaveChangesAsync(); 
            return existente; 

        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> Delete(int id) 
        {
            var existente = await _context.Usuarios.FindAsync(id); 
            if (existente == null) return NotFound(); 
            _context.Usuarios.Remove(existente); 
            await _context.SaveChangesAsync(); 
            return NoContent(); 
        }
    }
}