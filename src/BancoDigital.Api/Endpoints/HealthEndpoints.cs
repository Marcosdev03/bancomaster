using BancoDigital.Api.Data;
using Dapper;

namespace BancoDigital.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/health")
            .WithTags("Health");

        group.MapGet("/", () => Results.Ok(new
        {
            status = "Healthy",
            service = "BancoDigital.Api",
            checkedAt = DateTimeOffset.UtcNow
        }))
        .WithName("GetHealth")
        .WithSummary("Verifica se a API esta respondendo.");

        group.MapGet("/database", async (SqlConnectionFactory connectionFactory) =>
        {
            await using var connection = connectionFactory.Create();

            var result = await connection.QuerySingleAsync<DatabaseHealthResult>(
                """
                SELECT
                    DB_NAME() AS DatabaseName,
                    COUNT(*) AS TotalClientes
                FROM dbo.Clientes;
                """);

            return Results.Ok(new
            {
                status = "Healthy",
                database = result.DatabaseName,
                totalClientes = result.TotalClientes,
                checkedAt = DateTimeOffset.UtcNow
            });
        })
        .WithName("GetDatabaseHealth")
        .WithSummary("Verifica conexao com o SQL Server e a base BancoTreino.");

        return app;
    }

    private sealed record DatabaseHealthResult(string DatabaseName, int TotalClientes);
}
