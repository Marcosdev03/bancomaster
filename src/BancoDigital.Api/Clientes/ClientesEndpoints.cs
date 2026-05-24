using BancoDigital.Api.Data;
using BancoDigital.Api.Shared;
using Dapper;

namespace BancoDigital.Api.Clientes;

public static class ClientesEndpoints
{
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clientes")
            .WithTags("Clientes");

        group.MapGet("/", async (
            int? page,
            int? pageSize,
            SqlConnectionFactory connectionFactory) =>
        {
            var currentPage = page.GetValueOrDefault(1);
            var currentPageSize = pageSize.GetValueOrDefault(20);

            if (currentPage < 1)
            {
                return Results.BadRequest(new
                {
                    message = "O parametro page deve ser maior ou igual a 1."
                });
            }

            if (currentPageSize < 1 || currentPageSize > 100)
            {
                return Results.BadRequest(new
                {
                    message = "O parametro pageSize deve ficar entre 1 e 100."
                });
            }

            var offset = (currentPage - 1) * currentPageSize;

            await using var connection = connectionFactory.Create();

            var totalItems = await connection.ExecuteScalarAsync<int>(
                """
                SELECT COUNT(*)
                FROM dbo.Clientes;
                """);

            var clientes = (await connection.QueryAsync<ClienteResumoResponse>(
                """
                SELECT
                    ClienteId,
                    Nome,
                    Cidade,
                    Estado,
                    StatusCliente
                FROM dbo.Clientes
                ORDER BY ClienteId
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;
                """,
                new
                {
                    Offset = offset,
                    PageSize = currentPageSize
                })).ToList();

            var totalPages = (int)Math.Ceiling(totalItems / (double)currentPageSize);

            return Results.Ok(new PagedResponse<ClienteResumoResponse>(
                currentPage,
                currentPageSize,
                totalItems,
                totalPages,
                clientes
            ));
        })
        .WithName("ListarClientes")
        .WithOpenApi();

        return app;
    }
}