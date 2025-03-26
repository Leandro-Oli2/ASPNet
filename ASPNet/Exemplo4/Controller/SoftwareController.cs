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
    public class SoftwareController : ControllerBase
    {
        private readonly AppDbContext _context; 

        public SoftwareController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<IEnumerable<Software>> Get() 
        {
            
            return await _context.Softwares.ToListAsync();  
        }

        [HttpPost]
        public async Task<ActionResult<Software>> Post([FromBody] Software software) 
        {
            _context.Softwares.Add(software); 
            await _context.SaveChangesAsync(); 
            return software; 
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<Software>> Put(int id, [FromBody] Software software) 
        {
            var existente = await _context.Softwares.FindAsync(id); 
            if (existente == null) return NotFound(); 
            existente.Produto = software.Produto;
            existente.HardDisk = software.HardDisk;
            existente.MemoriaRam = software.MemoriaRam;

            await _context.SaveChangesAsync(); 
            return existente; 
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> Delete(int id) 
        {
            var existente = await _context.Softwares.FindAsync(id); 
            if (existente == null) return NotFound(); 
            _context.Softwares.Remove(existente); 
            await _context.SaveChangesAsync(); 
            return NoContent(); 
        }
    }
}