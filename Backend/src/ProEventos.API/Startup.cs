using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using ProEventos.Application;
using ProEventos.Domain;
using ProEventos.Persistence;
using System;
using System.IO;

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
            services.AddSwaggerGen();

            // Services
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();

            // AutoMappers
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Data
            services.AddDbContext<ProEventosContext>(
                context => context.UseNpgsql(Configuration.GetConnectionString("App"))
            );
            services.AddScoped<IEventoRespository, EventoRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IPalestranteRepository, PalestranteRepository>();
            services.AddScoped<IRedeSocialRepository, RedeSocialRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            // Configurando o storage das imagens
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseHttpsRedirection();

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
