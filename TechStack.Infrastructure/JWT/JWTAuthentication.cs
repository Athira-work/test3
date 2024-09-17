using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Shared;

namespace TechStack.Infrastructure.JWT
{
    public class JWTAuthentication : IAppAuthentication
    {
        private readonly JWTAppSettings appSettings;
        public JWTAuthentication(IOptions<JWTAppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        public string Authentication(string username, string password)
        {
            var user = GetAppUsers.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (!(username.Equals(username) || password.Equals(password)))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(appSettings.Secret);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
        private List<AppUser> GetAppUsers = new List<AppUser>
        {
            new AppUser {  Username = Constants.AuthTRMUser, Password =  Constants.AuthTRMPassword},
            new AppUser {  Username = Constants.AuthCDUser, Password = Constants.AuthCDPassword }
        };
    }
}
