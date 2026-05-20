namespace BancoDigital.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/health")
        .WithTags("Health");

        group.MapGet("/", () =>
        {
            return Results.Ok(new
            {
                status = "healthy",
                service = "BancoDigital.Api",
                checkedAt = DateTime.UtcNow

            });

        })
        .WithName("GetHealthStatus")
        .WithOpenApi();

        return app;

        }    
}