namespace BancoDigital.Api.Auth;

public sealed record LoginValidationResults
{
    public bool Sucesso { get; init; }

    public string Mensagem { get; init; } = string.Empty;

    public int? UsuarioId { get; init; }

    public int? ClienteId { get; init; }

    public string? Login { get; init; }

    public string? Email { get; init; }

    public bool? PrecisaTrocarSenha { get; init; }

    public string? Perfis { get; init; }
}