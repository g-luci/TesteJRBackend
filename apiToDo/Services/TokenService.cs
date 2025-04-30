using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using apiToDo.Models;

namespace apiToDo.Services
{
    public class TokenService
    {
        //Método responsável por gerar um tokken para uma tarefa
        public static object GenerateToken(Tarefas tarefa)
        {
            //chama a chave e criptografa
            var key = Encoding.ASCII.GetBytes("9siNtbVImxoiOVPGfKtpefsKjKK2nP6RVeXFrvqGM");
            //configura o token
            var tokenConfig = new SecurityTokenDescriptor
            {
                //Define os dados que estarão dentro do token
                Subject = new ClaimsIdentity(new Claim[]
                {   //Adiciona o Id da tarefa como claim
                    new Claim("tarefaId", tarefa.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),//Define o tempo de expiração 
                //tipo de assinatura e criptografia
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //chama a classe JwtSecurityTokenHandler para manipular o tokken
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);//Cria o token
            var tokenString = tokenHandler.WriteToken(token);//armazena a string de token

            //retorna o token
            return new
            {
                Token = tokenString,
            };
        }
    }
}
