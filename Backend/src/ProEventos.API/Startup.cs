using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.Helpers;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;
using ProEventos.Persistence;
using ProEventos.Persistence.Repositories;
using ProUsers.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Injeção de dependencias
        /// </summary>
        /// <param name="services"></param>
        public void DependencyInjection(IServiceCollection services)
        {
            // AutoMappers
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProEventosProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Health Check
            services.AddHealthChecks();

            // Services
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();
            services.AddTransient<IAccountService, AccountService>();

            // Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventoRespository, EventoRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IPalestranteRepository, PalestranteRepository>();
            services.AddScoped<IRedeSocialRepository, RedeSocialRepository>();

            // Context
            var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");
            services.AddDbContext<ProEventosContext>(
                context => context.UseNpgsql(dbConnection)
            );

            // Authentication
            services
            .AddIdentityCore<User>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                }
            )
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<ProEventosContext>()
            .AddDefaultTokenProviders();

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        /// <summary>
        /// Configura��es do swagger
        /// </summary>
        /// <param name="services"></param>
        public void SwaggerConfigurations(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                        Entre com 'Bearer' [espa�o] ent�o coloque seu token. 
                        Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AddNewtonSoftJson corrigi problema de referencia ciclica
            services
            .AddControllers()
            .AddJsonOptions(
                options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            )
            .AddNewtonsoftJson(
                j => j.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors();

            SwaggerConfigurations(services);

            DependencyInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.Equals("Development"))
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            // Configurando o storage das imagens
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

            app.UseHttpsRedirection();
            app.UseHealthChecks("/health");
            app.UseRouting();

            // Adicionando suporte a autentica��o e autoriza��o
            app.UseAuthentication();
            app.UseAuthorization();

            // Liberando cors
            app.UseCors(cors => cors
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
