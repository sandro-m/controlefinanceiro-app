using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API
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
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoBD")));

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Contexto>();

            services.AddCors(); // Compartilhamento de serviços entre UI e back-end

            // Configurar o projeto Angular 
            services.AddSpaStaticFiles(diretorio => {
                diretorio.RootPath = "ControleFinanceiro-UI";
            });

            services.AddControllers()
                .AddJsonOptions(opcoes => 
                { 
                    opcoes.JsonSerializerOptions.IgnoreNullValues = true; 
                })
                .AddNewtonsoftJson(opcoes => {
                    opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opcoes => {
                opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles(); // new

            app.UseSpaStaticFiles(); // new

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = ConfigurationPath.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI");

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            }); // new
        }
    }
}
