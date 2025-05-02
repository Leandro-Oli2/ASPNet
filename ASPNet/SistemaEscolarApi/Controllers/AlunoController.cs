using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolarApi.DB;

namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>>
        Get()
        {
            var alunos = await _context.Alunos
            .Include(a => a.Curso)
            .Select(a => new AlunoDTO
            {
                Id = a.Id,
                Nome = a.Nome,
                Curso = a.Curso.Descricao
            })
            .ToListAsync();

            return Ok(alunos); // Retorna a lista de alunos com status 200 
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDto){
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDto.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado.");
            
            var aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                CursoId = Curso.Id
            };
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Aluno cadastrado com sucesso!"});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlunoDTO alunoDto){
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound("Aluno não encontrado.");

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDto.Curso);
            if (curso == null) return BadRequest("Curso não encontrado.");

            aluno.Nome = alunoDto.Nome;
            aluno.CursoId = curso.Id;

            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Aluno atualizado com sucesso!"});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
            var aluno = await _context.Alunos.FindAsync(id);
            if(aluno == null) return NotFound("Aluno não encontrado.");

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            
            return Ok(new {mensagem = "Aluno removido com sucesso!"});
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetById(int id){
            var aluno = await _context.Alunos
            .Include(a => a.Curso)
            .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return NotFound("Aluno não encontrado.");

            var alunoDto = new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Curso = aluno.Curso.Descricao
            };

            return Ok(alunoDto);
        }
    }
}