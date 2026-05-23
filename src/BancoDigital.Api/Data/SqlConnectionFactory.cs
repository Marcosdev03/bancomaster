using Microsoft.Data.SqlClient;

namespace BancoDigital.Api.Data;

public sealed class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BancoTreino")
            ?? throw new InvalidOperationException("Connection string 'BancoTreino' is not configured.  ");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}