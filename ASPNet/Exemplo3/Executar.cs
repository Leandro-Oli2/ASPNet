using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Chamar as bibliotecas do ASP.NET
//Executar o comando para instalar o pacote: dotnet add package Microsoft.AspNetCore
//Comando para instalar o Swagger: dotnet add package Swashbuckle.AspNetCore
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Exemplo3.Database;

namespace Exemplo3
{
    public class Executar
    {
        public static void Main(string[] args){
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            builder.Services.AddDbContext<AppDbContext>(options=>options.UseNpgsql(connectionString));
            //adiciona o serviço ao Banco de Dados

            builder.Services.AddControllers();//Adiciona serviço ao controller

            //Habilitar o Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}