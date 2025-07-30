using Microsoft.AspNetCore.Identity;

namespace FandomHub.Infrastructure.Services
{
	public class AuthService : IAuthService
	{ 
		private readonly UserManager<IdentityApplicationUser> _userManager;
		private readonly ITokenService _tokenService; 
        public AuthService(UserManager<IdentityApplicationUser> userManager, ITokenService tokenService)
        { 
            _userManager = userManager;
			_tokenService = tokenService;
        }

		public async Task<(string Token, AuthResponse UserInfo)> LoginAsync(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);
			if (user == null)
			{
				throw new Exception("Invalid UserName or Password.");
			}
			var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
			if (!isPasswordValid)
			{
				Console.WriteLine("Invalid password.");
				throw new Exception("Invalid UserName or password.");
			}
			var roles = await _userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault() ?? "User";
            Console.WriteLine("UserId v1: "+ user.Id);
			var token = _tokenService.GenerateToken(user.Id, user.UserName, role);
			var userInfo = new AuthResponse
			{
				UserId = user.Id,
				FullName = user.FullName,
				Role = role
			};
			return (token, userInfo);
		}

		public async Task<(string Token, AuthResponse UserInfo)> RegisterAsync(RegisterRequest request)
		{
			var existUserName = await _userManager.FindByNameAsync(request.UserName);
			if (existUserName != null)
			{ 
				throw new Exception("Username is already taken.");
			}
			var existEmail = await _userManager.FindByEmailAsync(request.Email);
			if (existEmail != null)
			{
				throw new Exception("Email is already taken.");
			}

			var user = new IdentityApplicationUser
			{ 
				UserName = request.UserName,
				Email = request.Email,
				BirthDay = request.BirthDay
			};
			var result = await _userManager.CreateAsync(user,request.Password);
			if (!result.Succeeded)
			{  
				throw new Exception("Registration failed");
			}
			var roleResult = await _userManager.AddToRoleAsync(user, "User");
			if (!roleResult.Succeeded)
			{  
				throw new Exception("Failed to assign role.");
			} 
			var token = _tokenService.GenerateToken(user.Id, user.UserName, "User");
			var roles = await _userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault();
			var userInfo = new AuthResponse
			{
				UserId = user.Id,
				UserName= "",
				Role = role
			};
			return (token, userInfo);
			;
		}
	}
}
