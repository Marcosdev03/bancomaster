using BancoDigital.Api.Data;
using Dapper;

namespace BancoDigital.Api.Clientes;


public static class ClientesEndpoints
{
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clientes")
            .WithTags("Clientes");

        group.MapGet("/", async (SqlConnectionFactory connectionFactory) =>
        {
            await using var connection = connectionFactory.Create();

            var clientes = await connection.QueryAsync<ClienteResumoResponse>(
                """
                SELECT TOP 50
                    ClienteId,
                    Nome,
                    Cidade,
                    Estado,
                    StatusCliente
                FROM dbo.Clientes
                ORDER BY ClienteId;
                """);

            return Results.Ok(clientes);

        })
        .WithName("ListarClientes")
        .WithOpenApi();

        return app;
    }
}