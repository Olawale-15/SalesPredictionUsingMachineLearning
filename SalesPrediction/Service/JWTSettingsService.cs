using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IJWTSettingsService;
using SalesPrediction.Interface.IRepository;
using SalesPrediction.Interface.IService;
using SalesPrediction.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesPrediction.Service
{
    public class JWTSettingsService : IJWTSettings
    {
        private readonly ITraderRepository _traderRepository;
        private readonly JWTSettings _jwtSettings;

        public JWTSettingsService(IOptions<JWTSettings> jwtSettings, ITraderRepository traderRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _traderRepository = traderRepository;
        }
        public string GenerateToken(Trader trader)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, trader.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, trader.FirstName),
            };

            //if(trader.Roles != null && trader.Roles.Any())
            //{
            //    foreach(var role in trader.Roles)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            //    }
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
