using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Curso)
                .Select(d => new DisciplinaDTO
                {
                    Descricao = d.Descricao,
                    Curso = d.Curso.Descricao
                })
                .ToListAsync();

            return Ok(disciplinas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO disciplinaDto)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDto.Curso);
            if (curso == null) return BadRequest("Curso não encontrado.");

            var disciplina = new Disciplina
            {
                Descricao = disciplinaDto.Descricao,
                CursoId = curso.Id
            };

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDto)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null) return NotFound("Disciplina não encontrada.");

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDto.Curso);
            if (curso == null) return BadRequest("Curso não encontrado.");

            disciplina.Descricao = disciplinaDto.Descricao;
            disciplina.CursoId = curso.Id;

            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null) return NotFound("Disciplina não encontrada.");

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
