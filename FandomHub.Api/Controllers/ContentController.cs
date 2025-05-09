using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using System.Security.Claims;

namespace FandomHub.Api.Controllers
{
	[Route("content/")]
	[ApiController]
	public class ContentController : ControllerBase
	{
		private readonly IContentService _contentService;
        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

		[HttpPost("create"), Authorize]
		public async Task<IActionResult> CreateNewContent(ContentCreateRequest request)
		{
			try
			{
				string? userId = GetUserId();
				if (userId == null)
				{
					return BadRequest();
				}
				var response = await _contentService.CreateContent(request, userId); 
				return Ok(new
				{
					message = "Success",
					data = response
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					message = "Have Something wrong in process create"
				});
			}
			 
		}	


		private string? GetUserId()
		{
			string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			string? userName = User.FindFirst(ClaimTypes.Name)?.Value;
			Console.WriteLine($"userId v3: {userId}"); 
			return userId;
		}
	}
}
