using BancoDigital.Api.Data;
using Dapper;

namespace BancoDigital.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/health")
            .WithTags("Health");

        group.MapGet("/live", () =>
        {
            return Results.Ok(new
            {
                status = "healthy",
                checkedAt = DateTimeOffset.UtcNow
            });
        })
        .WithName("GetLiveness")
        .WithOpenApi();

        group.MapGet("/ready", async (SqlConnectionFactory connectionFactory) =>
        {
            try
            {
                await using var connection = connectionFactory.Create();

                var canConnect = await connection.ExecuteScalarAsync<int>(
                    """
                    SELECT 1;
                    """);

                if (canConnect != 1)
                {
                    return Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
                }

                return Results.Ok(new
                {
                    status = "ready",
                    checkedAt = DateTimeOffset.UtcNow
                });
            }
            catch
            {
                return Results.Json(
                    new
                    {
                        status = "not_ready",
                        checkedAt = DateTimeOffset.UtcNow
                    },
                    statusCode: StatusCodes.Status503ServiceUnavailable);
            }
        })
        .WithName("GetReadiness")
        .WithOpenApi();

        return app;
    }
}