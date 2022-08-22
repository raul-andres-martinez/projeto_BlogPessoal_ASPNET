using BlogAPI.Src.Contextos;
using BlogAPI.Src.Repo;
using BlogAPI.Src.Repo.Launch;
using BlogAPI.Src.Services;
using BlogAPI.Src.Services.Implements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI
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
            // CONTEXTO / Configura��es de Banco de dados mySQL
            if (Configuration["Enviroment:Start"] == "PROD")
            {
                services.AddEntityFrameworkNpgsql()
                .AddDbContext<BlogPessoalContext>(
                opt =>
                opt.UseNpgsql(Configuration["ConnectionStringsProd:DefaultConnection"]));
            }
            else {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<BlogPessoalContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                 }

            // REPOS
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<ITheme, ThemeRepo>();
            services.AddScoped<IPost, PostRepo>();

            //Controladores
            services.AddCors();
            services.AddControllers();

            //Services
            services.AddScoped<IAuthentication, AuthenticationServices>();

            //Token JWTBearer
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Swagger
            services.AddSwaggerGen(
                s =>
                {
                    s.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Blog Pessoal",
                        Version = "v1"
                    });
                    s.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT authorization header uses: Bearer + JWT Token",
                        });
                    s.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new List<string>()
                            }
                        });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    s.IncludeXmlComments(xmlPath);
                });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlogPessoalContext context)
        {
            //Ambiente de Desenvolvimento
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogPessoal v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            //Ambiente de produ��o
            context.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogPessoal v1");
                c.RoutePrefix = String.Empty;
            });

            //Route
            app.UseRouting();

            app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            //Auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
