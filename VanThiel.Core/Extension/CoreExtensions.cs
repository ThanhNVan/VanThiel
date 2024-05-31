using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VanThiel.Domain.DTOs.ReturnModel;
namespace VanThiel.Core.Extension;

public static class CoreExtensions
{
    public static string HashPassword512(string str)
    {
        var message = Encoding.UTF8.GetBytes(str);
        using (var alg = SHA512.Create())
        {
            string hex = "";

            var hashValue = alg.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex;
        }
    }

    public static string GenerateUserAccessToken(UserAccessInfo model, string secret)
    {
        var result = string.Empty;
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var secretKeyBytes = Encoding.UTF8.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("UserId", model.Id),
                new Claim("FullName", model.Fullname),
                new Claim("PhoneNumber", model.PhoneNumber),
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.Role, "User"),
            }),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        result = jwtTokenHandler.WriteToken(token);

        return result;
    }
}
