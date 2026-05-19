using BancoDigital.Api.Data;
using BancoDigital.Api.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Banco Digital API",
        Version = "v1",
        Description = "API de treino para simular um ecossistema bancario com SQL Server, .NET, Kafka e Kubernetes."
    });
});

builder.Services.AddSingleton<SqlConnectionFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Banco Digital API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Banco Digital API v1");
    });
}

app.UseHttpsRedirection();

app.MapHealthEndpoints();

app.Run();

public partial class Program;
