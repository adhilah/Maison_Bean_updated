using MaisonBean.Application.Common;
using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MaisonBean.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings =
            jwtSettings.Value;
    }

    // GENERATE ACCESS TOKEN
    public string GenerateToken(
        AppUser user,
        IList<string> roles)
    {
        // CLAIMS
        var claims =
    new List<Claim>
    {
        // USER ID

        new Claim(
            "id",
            user.Id.ToString()
        ),

        // EMAIL

        new Claim(
            "email",
            user.Email ?? string.Empty
        ),

        // TOKEN VERSION

        new Claim(
            "tokenVersion",
            user.TokenVersion.ToString()
        )
    };

        // ROLES
        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    "ROLE",
                    role.ToUpper()
                )
            );
        }

        // SECRET KEY
        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtSettings.SecretKey
                )
            );

        // CREDENTIALS
        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

        // TOKEN
        var token =
            new JwtSecurityToken(
                issuer:
                    _jwtSettings.Issuer,

                audience:
                    _jwtSettings.Audience,

                claims:
                    claims,

                expires:
                    DateTime.UtcNow.AddMinutes(
                        _jwtSettings.ExpiryInMinutes
                    ),

                signingCredentials:
                    credentials
            );

        // RETURN TOKEN
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    // GENERATE REFRESH TOKEN
    public string GenerateRefreshToken()
    {
        var bytes =
            new byte[64];

        using var rng =
            RandomNumberGenerator.Create();

        rng.GetBytes(bytes);

        return Convert
            .ToBase64String(bytes);
    }
}