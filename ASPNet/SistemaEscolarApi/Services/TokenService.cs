using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SistemaEscolarApi.Services
{
    public class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("minha-chave-ultra-segura-com-32-caracteres");
            //chave secreta para assinar o token. Deve ser mantida em segredo e não deve ser exposta
            
            var tokenDescriptor = new SecurityTokenDescriptor {
                // SecurityTokenDescriptor é uma classe que contém as informações do token, aqui pode colocar o tempo de expiração.
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Expira em 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // SymmetricSecurityKey é uma classe que representa uma chave simétrica, ou seja, a mesma chave é usada para assinar e verificar o token. SecurityAlgorithms.HmacSha256Signature é o algoritmo de assinatura usado para assinar o token.
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); // Cria o token com as informações do SecurityTokenDescriptor
            return tokenHandler.WriteToken(token); // Escreve o token em formato string e retorna


        }
    }
}