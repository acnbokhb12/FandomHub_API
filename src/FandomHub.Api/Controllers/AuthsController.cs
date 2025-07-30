using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FandomHub.Api.Controllers
{
	[Route("api/v1/auth")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly ITokenService _tokenService;
		private readonly IEmailService _emailService;

		public AuthsController(
			IAuthService authService,
			IUserService userService,
			IRefreshTokenService refreshTokenService,
			ITokenService tokenService,
			IEmailService emailService)
		{
			_authService = authService;
			_userService = userService;
			_refreshTokenService = refreshTokenService;
			_tokenService = tokenService;
			_emailService = emailService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisteAccount([FromBody] RegisterRequest request)
		{
			try
			{
				var (token, userInfo) = await _authService.RegisterAsync(request);
				var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(userInfo.UserId); 
				return Ok(new
				{
					success = true,
					data = new
					{
						Token = token,
						RefreshToken = refreshToken,
						User = userInfo
					}
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					success = false,
					message = ex.Message
				});
			}
		}

		[HttpPost("signin")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				var (token, userInfo) = await _authService.LoginAsync(request);
				var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(userInfo.UserId);
				userInfo.UserName = "";
				return Ok(new
				{
					success = true,
					data = new
					{
						Token = token,
						RefreshToken = refreshToken,
						User = userInfo
					}
				});
			}
			catch
			{
				return Unauthorized(new
				{
					success = false,
					message = "We don't recognize these credentials. Try again or register a new account."
				});
			}
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
		{
			var refreshToken = await _refreshTokenService.GetRefreshTokenAsync(request.RefreshToken);

			if (refreshToken == null || !refreshToken.IsActive)
			{
				return Unauthorized("Invalid or expired refresh token");
			}

			var user = await _userService.FindByIdAsync(refreshToken.UserId);
			if (user == null)
			{
				return Unauthorized("User not found");
			}

			refreshToken.Revoked = true;
			var token = _tokenService.GenerateToken(user.UserId, user.UserName, user.Role);
			var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.UserId);
			user.UserName = "";
			return Ok(new
			{
				success = true,
				data = new
				{
					Token = token,
					RefreshToken = newRefreshToken,
					User = user
				}
			});
		}

		[HttpPost("test-welcome-email")]
		public async Task<IActionResult> WelcomeFandomSend(string email, string userName, string fullName)
		{
			try
			{
				var verificationLink = $"https://fandomhub.com/verify?token={Guid.NewGuid()}";
				var result = await _emailService.SendWelcomeEmailAsync(
					email,
					fullName,
					userName,
					verificationLink
				);
				return Ok(new
				{
					success = true,
					message = "Gui thanh cong"
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					success = false,
					message = ex.Message
				});
			}
		}
	}
}
