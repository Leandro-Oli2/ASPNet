using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//para executar os comandos eu preciso isnalar os pacotes ASP.NET Core com o comando: dotnet add package Microsoft.AspNetCore

//Vamos usar a ferramenta Swagger para documentar a API, que ja esta incluida no ASP.NET Core, mas precisa de um pacote para funcionar, e nisso precisamos executa o comando: dotnet add package Swashbuckle.AspNetCore

//Chamar as bibliotecas do ASP.NET Core

using Microsoft.AspNetCore.Builder;//Isso serve para configurar a aplicação, interfaces e classes  
using Microsoft.Extensions.Hosting;//Isso serve para configurar a aplicação, interfaces e classes  
using Microsoft.Extensions.DependencyInjection;//Isso serve para configurar a aplicação, interfaces e classes  

namespace Exemplo2
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            // Criando o Builder para a aplicação ASP.NET Core
            var builder = WebApplication.CreateBuilder(args);

            // Adicionando serviços de controllers
            builder.Services.AddControllers();

            // Configurando Swagger para documentação da API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configurar Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            // Redirecionamento para HTTPS
            app.UseHttpsRedirection();

            // Adicionar autorização
            app.UseAuthorization();

            // Mapear os controllers
            app.MapControllers();

            // Executar a aplicação
            app.Run();
        }
    }
}