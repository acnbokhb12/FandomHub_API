using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FandomHub.Api.Controllers
{
	[Route("api/v1/fcmtokens")]
	[ApiController]
	public class FcmTokenController : ControllerBase
	{
		private readonly IFcmTokenService _fcmTokenService;
		private readonly IFcmService _fcmService;
		public FcmTokenController(
			IFcmTokenService fcmTokenService, 
			IFcmService fcmService)
		{
			_fcmTokenService = fcmTokenService;
			_fcmService = fcmService;
		}


		[HttpPost]
		public async Task<IActionResult> TestNotification([FromBody] FcmTokenRequest request)
		{
			try
			{
				var userId = GetUserId();
				if (string.IsNullOrEmpty(userId))
				{
					return Unauthorized(new { message = "User not authenticated" });
				}
				var result = await _fcmTokenService.SaveDeviceTokenAsync(request, userId);
				if (result)
				{
					return Ok(new { message = "FCM token saved successfully" });
				}
				else
				{
					return BadRequest(new { message = "Failed to save FCM token" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("firebase-status")]
		public IActionResult GetFirebaseStatus()
		{
			try
			{
				var app = FirebaseAdmin.FirebaseApp.DefaultInstance;
				return Ok(new
				{
					Status = "Firebase initialized",
					ProjectId = app.Options.ProjectId
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					Status = "Firebase not initialized",
					Error = ex.Message
				});
			}
		}

		[HttpPost("test-notification")]
		public async Task<IActionResult> TestNotification([FromBody] TestNotificationRequest request)
		{
			try
			{
				var result = await _fcmService.SendNotificationAsync(
					request.Token,
					request.Title,
					request.Body);

				return Ok(new { Success = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Error = ex.Message });
			}
		}


		private string GetUserId()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return userId;
		}
	}
}
