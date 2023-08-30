using Back_End.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_End.Utils
{
    public class JwtConfigurator
    {
        public static string GetToken(User user, IConfiguration configuration)
        {
            string SecretKey = configuration["Jwt:SecretKey"];
            string Issuer = configuration["Jwt:Issuer"];
            string Audience = configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              
                new Claim("IdUser",user.Id.ToString())
            };

            var token = new JwtSecurityToken(   
                issuer: Issuer,
                audience: Audience,
                claims:claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static int getTokenIdUser(ClaimsIdentity claims)
        {
            if (claims != null)
            {

                foreach (var claim in claims.Claims)
                {
                    if (claim.Type == "IdUser")
                    {
                        return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }
    }
}
