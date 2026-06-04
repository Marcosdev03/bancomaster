namespace BancoDigital.Api.Auth;

public sealed record LoginRequest(
    string Login,
    string Senha
);