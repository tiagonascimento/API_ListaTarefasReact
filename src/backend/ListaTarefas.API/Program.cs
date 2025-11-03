using ListaTarefas.API.Filter;
using ListaTarefas.Aplication;
using ListaTarefas.Infra.repository;
using ListaTarefas.Infra.Security.jwt.Configuration;
using ListaTarefas.Utils.cryptography.cryptosettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:80");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configurar Swagger com suporte a JWT
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "tarefas API",
        Version = "v1",
        Description = "API para sistema tarefas",
        Contact = new OpenApiContact
        {
            Name = "Suporte BankMore",
            Email = "tarefas@tarefas.com"
        }
    });
    // Adicione esta linha para corrigir o host base
    options.AddServer(new OpenApiServer
    {
        Url = "/api"
    });


    // Configurar segurança JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando o esquema Bearer. 
                      \r\n\r\nDigite 'Bearer' [espaço] e então seu token na caixa de texto abaixo.
                      \r\n\r\nExemplo: 'Bearer seu-token-jwt-aqui'",
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
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

});

builder.Services.Configure<CryptoSettings>(
    builder.Configuration.GetSection("CryptoSettings"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("jwt"));



//injecao de dependencia / estenção de classe
builder.Services.AddAplication();
builder.Services.AddInfra(builder.Configuration);


//filtro
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarefas API v1");
        c.RoutePrefix = "swagger";
    });
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
