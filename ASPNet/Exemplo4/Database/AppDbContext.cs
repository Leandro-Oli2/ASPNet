using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Exemplo4.Database  
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Software> Softwares { get; set; }
        public DbSet<Models.Maquina> Maquinas { get; set; }

    }
}
