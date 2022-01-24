using Gestor_API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gestor_API.Services
{
    public static class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, usuario.ds_nome),
                    new Claim(ClaimTypes.Role, usuario.Id.ToString())
                }),
                Expires = System.DateTime.UtcNow.AddHours(8), SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = tokenhandler.CreateToken(tokenDescription);

            return tokenhandler.WriteToken(Token);


        }
    }
}
