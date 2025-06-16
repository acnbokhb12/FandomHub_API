using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FandomHub.Api.Controllers
{
	[Route("api/v1/categories")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IHubCategoryService _hubCategoryService;
		public CategoryController(IHubCategoryService hubCategoryService, ICategoryService categoryService)
		{
			_hubCategoryService = hubCategoryService;
			_categoryService = categoryService;
		}


		[HttpGet("/api/v1/hubs/{id}/categories")]
		public async Task<IActionResult> GetCategoriesByHubId([FromRoute] int id)
		{
			try
			{
				var hub = await _hubCategoryService.GetCategoriesByHubId(id);
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] int id)
		{
			try
			{
				var category = await _categoryService.GetCategoryByIdWithCondition(id);
				if(category == null)
				{
					return NotFound(new { message = "Category not found" });
				}
				return Ok(new { data = category });

			}catch(Exception ex)
			{
				return BadRequest(new {message = ex.Message});
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			try
			{
				var categories = await _categoryService.GetCategoriesWithCondition();
				return Ok(new { data = categories });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
