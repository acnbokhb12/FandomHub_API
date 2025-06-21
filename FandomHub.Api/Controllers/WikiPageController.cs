using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FandomHub.Api.Controllers
{
	[Route("api/v1/wiki-pages")]
	[ApiController]
	public class WikiPageController : ControllerBase
	{
		private readonly IWikiPageService _wikiPageService;
		public WikiPageController(IWikiPageService wikiPageService)
		{
			_wikiPageService = wikiPageService;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateWikiPage([FromBody] WikiPageCreateRequest request)
		{
			if (request == null)
			{
				return BadRequest("Request cannot be null");
			}
			try
			{
				string userId = GetUserId();
				var result = await _wikiPageService.CreateWikiPage(request, userId);
				if (result == null)
				{
					return BadRequest("Failed to create wiki page");
				}
				return CreatedAtAction(nameof(CreateWikiPage), new { id = result.WikiPageId }, result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating wiki page: {ex.Message}");
			}
		}

		private string GetUserId()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return userId;
		}
	}
}
