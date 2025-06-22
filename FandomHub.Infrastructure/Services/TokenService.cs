using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FandomHub.Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
		public string GenerateToken(string userId, string username, string role)
		{ 
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.Role, role),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};  
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			 
			int expiryTime;
			if (!int.TryParse(_configuration["Jwt:ExpiryTime"], out expiryTime))
			{
				expiryTime = 120; // Giá trị mặc định nếu không thể lấy được từ cấu hình
			} 
			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(expiryTime), // Dùng thời gian đã lấy được
				signingCredentials: creds); 

			return new JwtSecurityTokenHandler().WriteToken(token);
		} 

	}
}
