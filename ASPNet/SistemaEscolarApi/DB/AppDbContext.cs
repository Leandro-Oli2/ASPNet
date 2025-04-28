using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarApi.DB
{
    public class AppDbContext : DbContext{
        public DBSet<Aluno> Alunos { get; set; }
        public DBSet<Curso> Cursos { get; set; }
        public DBSet<Disciplina> Disciplinas { get; set; }
        public DBSet<DisciplinaAlunoCurso> DisciplinaAlunoCurso { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisciplinaAlunoCurso>() // Configura a a entidade DisciplinaAlunoCurso
                .HasKey(dc => new { dc.AlunoId, dc.DisciplinaId, dc.CursoId });

        }
    }
}