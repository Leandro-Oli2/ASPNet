using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;



namespace ASPNet
{
    public class Executar
    {
        public static void Main(string[] args){
            var Builder = WebApplication.CreateBuilder(args);
            //Criar a aplicação
            var app = Builder.Build(); // configurar

            app.UseStaticFiles(); // para poder ativar arquivos estaticos

            app.UseRouting();

            app.UseEndpoints(endpoins =>{
                endpoins.MapGet("/", async context =>{
                    context.Response.Redirect("/index.html");
                });
            });

            app.Run();

        }
        // public static IHostBuilder CreateHostBuilder(string[] args)=>{
        //     //Define o metodo que é responsavel para criar o host de aplicação
        //     Host.CreateDefaultBuilder(args) // cria um host com configuração ambiente
        //         .ConfigureWebHostDefault(webBuilder)
        // }
    }
}