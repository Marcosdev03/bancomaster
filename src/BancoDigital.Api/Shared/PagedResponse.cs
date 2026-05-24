namespace BancoDigital.Api.Shared;

public sealed record PagedResponse<T>(
    int Page,
    int PageSize,
    int TotalItems,
    int TotalPages,
    IReadOnlyCollection<T> Items
);
