using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FandomHub.Api.Controllers
{
	[Route("api/v1/categories")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IHubCategoryService _hubCategoryService;
		public CategoryController(IHubCategoryService hubCategoryService)
		{
			_hubCategoryService = hubCategoryService;
		}


		[HttpGet("/api/v1/hubs/{hubId}/categories")]
		public async Task<IActionResult> GetCategoriesByHubId([FromRoute] int hubId)
		{
			try
			{
				var hub = await _hubCategoryService.GetCategoriesByHubId(hubId);
				if (hub == null)
				{
					return NotFound(new { message = "Hub not found" });
				}
				return Ok(new { data = hub });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
