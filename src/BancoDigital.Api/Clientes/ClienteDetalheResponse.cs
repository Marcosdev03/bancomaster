namespace BancoDigital.Api.Clientes;

public sealed record ClienteDetalheResponse(
    int ClienteId,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    decimal RendaMensal,
    int ScoreCredito,
    string Cidade,
    string Estado,
    string StatusCliente
);

