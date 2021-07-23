using MP.Common;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace VAS.Dealer.Services
{
    public class TokenServices : ITokenServices
    {
        readonly MP_Context _Context;
        private readonly IConfiguration Configuration;

        public TokenServices(MP_Context vendorContext, IConfiguration configuration)
        {
            _Context = vendorContext;
            Configuration = configuration;
        }

        public UserTokenDTO GetUserToken(MP_Account user)
        {
            return this.GenUserToken(user);
        }

        public UserTokenDTO Logon(MP_Account user)
        {
            MP_Account tempUser = _Context.Account.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (tempUser == null || !tempUser.IsActive) return null;
            string _PassHash = GeneratorPassword.EncodePassword(user.Password, tempUser.PasswordFormat, tempUser.PasswordSalt);
            if (tempUser.Password != _PassHash)
                return null;
            return this.GenUserToken(user);
        }

        public UserTokenDTO RefreshUserToken(string oldToken)
        {
            var principal = this.GetClaimsPrincipalByToken(oldToken);
            if (principal != null && principal.Identity.Name != null)
            {
                var user = _Context.Account.Where(x => x.Id == int.Parse(principal.Identity.Name)).FirstOrDefault();
                return this.GenUserToken(user);
            }
            return null;

        }

        public ClaimsPrincipal GetClaimsPrincipalByToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("UserTokenSetting:Secret").Value)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private UserTokenDTO GenUserToken(MP_Account user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("UserTokenSetting:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),

                Expires = DateTime.Now.AddHours(double.Parse(Configuration.GetSection("UserTokenSetting:Expires").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserTokenDTO userToken = new UserTokenDTO
            {
                Id = user.Id.ToString(),
                Accesstoken = tokenHandler.WriteToken(token),
                ExpiredAt = tokenDescriptor.Expires,
                Username = user.UserName
            };
            return userToken;
        }

        public bool VerifyAccessToken(string accesstoken)
        {
            var principal = this.GetClaimsPrincipalByToken(accesstoken);
            return principal != null;
        }
    }
}
