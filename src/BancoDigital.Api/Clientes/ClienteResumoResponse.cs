namespace BancoDigital.Api.Clientes;

public sealed record ClienteResumoResponse(
    int ClienteId,
    string Nome,
    string Cidade,
    string Estado,
    string StatusCliente
);