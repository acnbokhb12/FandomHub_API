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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			try
			{
				var wikiPage = await _wikiPageService.GetWikiPageByIdAsync(id);
				if (wikiPage == null)
				{
					return NotFound(new { message = "Wiki page not found" });
				}
				return Ok(wikiPage);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving wiki page: {ex.Message}");
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] WikiPageCreateRequest request)
		{
			if (request == null)
			{
				return BadRequest("Request cannot be null");
			}
			try
			{
				string userId = GetUserId();
				var response = await _wikiPageService.CreateWikiPage(request, userId);
				if (response == null)
				{
					return BadRequest("Failed to create wiki page");
				}
				return CreatedAtAction(nameof(Create), new { id = response.WikiPageId }, response);
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
