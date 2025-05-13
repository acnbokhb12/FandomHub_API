using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FandomHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
		private readonly ICommunityService _service;
        public CommunityController(ICommunityService communityService)
        {
            _service = communityService;
        }

        [HttpGet("CreateCommunity")]
        public async Task<IActionResult> CreatCommunity(CommunityCreateRequest request)
        {
            try
            {
                string userId = GetUserId();
                var response = await _service.CreateCommunity(request, userId);
                return Ok(new
                {
                    data = response
                });
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }	 
		}

		private string GetUserId()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return  userId;
		}
	}
}
