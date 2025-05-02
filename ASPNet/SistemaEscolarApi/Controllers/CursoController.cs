using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
       public async Task<ActionResult<IEnumerable<CursoDTO>>> Get()

        {
            var cursos = await _context.Cursos
                .Select(cursos => new CursoDTO
                {
                    Id = cursos.Id,
                    Descricao = cursos.Descricao
                })
                .ToListAsync();
                return Ok(cursos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CursoDTO cursoDto){
            var curso = new Curso
            {
                Descricao = cursoDto.Descricao
            };
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CursoDTO cursoDto){
            var curso = await _context.Cursos.FindAsync(id);
            if(curso == null) return NotFound("Curso Não Encontrado!");

            curso.Descricao = cursoDto.Descricao;

            // _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
            var curso = await _context.Cursos.FindAsync(id);
            if(curso == null) return NotFound("Curso Não Encontrado!");

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            
            return NoContent();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> GetById(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound("Curso não encontrado.");

            var cursoDto = new CursoDTO
            {
                Id = curso.Id,
                Descricao = curso.Descricao
            };

            return Ok(cursoDto);
        }
    }
}