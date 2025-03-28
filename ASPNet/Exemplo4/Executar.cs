using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Exemplo4.Database;  

namespace Exemplo4 
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lê a string de conexão do appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            // Adiciona o DbContext ao serviço de dependência
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            // Adiciona o serviço de Controllers
            builder.Services.AddControllers();

            // Habilita o Swagger para documentação da API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           var app = builder.Build();

        // Habilita o Swagger no pipeline
        app.UseSwagger();
        app.UseSwaggerUI();

        // Configura redirecionamento HTTPS
        app.UseHttpsRedirection();

        // Ativa o uso de arquivos estáticos (para o index.html funcionar)
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Ativa a autorização
        app.UseAuthorization();

        // Mapeia os controllers
        app.MapControllers();

        // Roda a aplicação
        app.Run();
        }
    }
}
