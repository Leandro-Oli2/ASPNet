using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.DB;
using Microsoft.AspNetCore.Mvc;


namespace SistemaEscolarApi.Controllers
{
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaAlunoCursoController(AppDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaAlunoCurso>>> Get()
        {
            var registros = await _context.DisciplinaAlunoCursos
                .Select(d => new DisciplinaAlunoCursoDTO
                {
                    AlunoId = d.AlunoId,
                    CursoId = d.CursoId,
                    DisciplinaId = d.DisciplinaId,
                })
                .ToListAsync();

                return Ok(registros); // Retorna a lista de alunos com status 200 
        }

        [HttpPost]
        public async Task<ActionResult<DisciplinaAlunoCurso>> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var disciplinaAlunoCurso = new DisciplinaAlunoCurso
            {
                AlunoId = disciplinaAlunoCursoDTO.AlunoId,
                CursoId = disciplinaAlunoCursoDTO.CursoId,
                DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId,
            };
           
            _context.DisciplinaAlunoCursos.Add(disciplinaAlunoCurso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var disciplinaAlunoCurso = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (disciplinaAlunoCurso == null) return NotFound("Registro Não Encontrado!");

            disciplinaAlunoCurso.AlunoId = disciplinaAlunoCursoDTO.AlunoId;
            disciplinaAlunoCurso.CursoId = disciplinaAlunoCursoDTO.CursoId;
            disciplinaAlunoCurso.DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplinaAlunoCurso = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (disciplinaAlunoCurso == null) return NotFound("Registro Não Encontrado!");

            _context.DisciplinaAlunoCursos.Remove(disciplinaAlunoCurso);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}