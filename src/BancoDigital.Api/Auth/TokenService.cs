using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BancoDigital.Api.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BancoDigital.Api.Auth;

public sealed class TokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public LoginResponse GenerateToken(LoginValidationResults user)
    {
        if (user.UsuarioId is null || string.IsNullOrWhiteSpace(user.Login))
        {
            throw new InvalidOperationException("Usuario valido sem dados obrigatorios para geracao de token.");
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UsuarioId.Value.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Login),
            new("login", user.Login)
        };

        if (user.ClienteId is not null)
        {
            claims.Add(new Claim("clienteId", user.ClienteId.Value.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(user.Perfis))
        {
            var perfis = user.Perfis.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var perfil in perfis)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireDate = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expireDate,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse(
            accessToken,
            "Bearer",
            _jwtOptions.ExpirationMinutes * 60
        );
    }
}