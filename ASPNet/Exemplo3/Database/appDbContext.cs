using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

namespace Exemplo3.Database
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options){

        }
        public DbSet<Models.Usuario> Usuarios {get; set;}
    }
}