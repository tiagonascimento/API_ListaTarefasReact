using BankMore.Application.service.jwt;
using ListaTarefas.Infra.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Npgsql;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ListaTarefas.Infra.repository
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfra(this IServiceCollection service, IConfiguration config)
        {
            addRepositorio(service);
            AddContex_Postgre(service, config);
            JWT(service, config);
        }
        private static void addRepositorio(IServiceCollection service)
        {
            service.AddScoped<ITaskRepository, TaskRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IJwt, Jwt>();
           
        }
        private static void AddContex_Postgre(IServiceCollection services, IConfiguration config)
        {
            var strConnection = config["ConnectionStrings:PostgresConnection"];

            // 1. Cria o data source com enum mapeado
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(strConnection);
            dataSourceBuilder.MapEnum<ListaTarefas.Domain.enums.TaskStatus> ("task_status"); // nome do tipo no PostgreSQL
            var dataSource = dataSourceBuilder.Build();

            // 2. Registra o DbContext usando o data source           
            services.AddDbContext<TarefasDbContext>(opt =>
            {
                opt.UseNpgsql(strConnection, npgsqlOptions =>
                {
                    npgsqlOptions.MapEnum<ListaTarefas.Domain.enums.TaskStatus>("task_status");
                });
                
            });
        }

        private static void JWT(IServiceCollection service, IConfiguration config)
        {
            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = config["jwt:issuer"],
                    ValidAudience = config["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}
