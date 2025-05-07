using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FandomHub.Api.Controllers
{
	[Route("auths/")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

		[HttpPost("register")]
		public async Task<IActionResult> RegisteAccount([FromBody]RegisterRequest request)
		{
			try
			{
				var token = await _authService.RegisterAsync(request);
				return Ok(new { Token = token });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				var token = await _authService.LoginAsync(request);
				return Ok(new { Token = token });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

	}
}
