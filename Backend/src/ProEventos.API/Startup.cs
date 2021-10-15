using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.Helpers;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.API
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
            // AddNewtonSoftJson corrigi problema de referencia ciclica
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    j => j.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos API", Version = "v1" });
            });

            // Services
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();

            // AutoMappers
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Data
            services.AddDbContext<ProEventosContext>(
                context => context.UseSqlServer(Configuration.GetConnectionString("App"))
            );
            services.AddScoped<IEventoRespository, EventoRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1");
                });
            }

            // Configurando o storage das imagens
            // app.UseStaticFiles(new StaticFileOptions() { 
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources" )),
            //     RequestPath = new PathString("/Resources")
            // });

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Liberando cors
            app.UseCors(cors => cors
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
