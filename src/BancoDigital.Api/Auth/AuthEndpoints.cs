using BancoDigital.Api.Data;
using Dapper;

namespace BancoDigital.Api.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("/login", async (
            LoginRequest request,
            SqlConnectionFactory connectionFactory,
            TokenService tokenService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Login) ||
                string.IsNullOrWhiteSpace(request.Senha))
            {
                return Results.BadRequest(new
                {
                    message = "Login e senha são obrigatórios."
                });
            }

            await using var connection = connectionFactory.Create();

            var result = await connection.QuerySingleAsync<LoginValidationResults>(
                """
                EXEC dbo.sp_ValidarLoginTreino
                    @Login = @Login,
                    @Senha = @Senha;
                """,
                new
                {
                    request.Login,
                    request.Senha
                });

            if (!result.Sucesso)
            {
                return Results.Unauthorized();
            }

            var token = tokenService.GenerateToken(result);

            return Results.Ok(token);
        })
        .WithName("Login")
        .AllowAnonymous()
        .WithOpenApi();

        return app;

    }
}