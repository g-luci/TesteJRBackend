using apiToDo.Repository;
using apiToDo.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiToDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITarefaRepository, TarefaRepository>(); //Injeção de dependencia
            services.AddTransient<TarefaSeeder>();//Injeção de dependencia
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiToDo", Version = "v1" });

                //Define uma configuração de segurança
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT no formato: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //Define a exigência de segurança para as rotas protegidas
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        // Define o esquema utilizado
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        //Lista de escopos
                        new List<string>()
                    }
                });
            });

            //Armazena a chave e criptografa
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSettings:SecretKey"]);

            //Adiciona o serviço de autenticação
            services.AddAuthentication(x =>
            {
                //Define o esquema padrão para autenticação
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //Define o esquema padrão de desafio
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //Desabilita a exigência de HTTPS
                x.RequireHttpsMetadata = false;
                //Salva o token no contexto atual
                x.SaveToken = true;
                //Define como o token JWT será validado
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //Habilita a validação da chave
                    ValidateIssuerSigningKey = true,
                    //Define qual é a chave secreta usada para validar
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //Desabilita validação do Issuer
                    ValidateIssuer = false,
                    //Desabilita validação do Audience
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {
                    //Evento disparado quando a autenticação falha
                    OnChallenge = context =>
                    {
                        //Impede o comportamento padrão de retornar uma resposta automática
                        context.HandleResponse();

                        //Define status code da resposta
                        context.Response.StatusCode = 401;
                        //Define o tipo da resposta
                        context.Response.ContentType = "application/json";

                        //Cria uma mensagem personalizada serializando para JSON
                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            status = 401,
                            message = "Token ausente, inválido ou expirado. Acesso negado."
                        });

                        //Retorna o JSON
                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "apiToDo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Cria um escopo de serviço
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Solicita a instancia da classe
                var seeder = scope.ServiceProvider.GetRequiredService<TarefaSeeder>();
                //chama o metodo para popular
                seeder.Popular();
            }
        }
    }
}
