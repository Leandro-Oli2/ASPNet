using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.DB;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

//comando para fazer o migrations
//dotnet ef migrations add InitialCreate -s

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();





