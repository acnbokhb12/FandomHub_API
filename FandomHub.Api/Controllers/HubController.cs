using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FandomHub.Api.Controllers
{
	[Route("hub/")]
	[ApiController]
	public class HubController : ControllerBase
	{
		private readonly IHubService _hubService;
		public HubController(IHubService hubService)
		{
			_hubService = hubService;	
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var hubs = await _hubService.GetAllAsync();
				return Ok(new { data = hubs });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById([FromRoute]int id)
		{
			try
			{
				var hub = await _hubService.GetCategoriesByHubId(id);
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
