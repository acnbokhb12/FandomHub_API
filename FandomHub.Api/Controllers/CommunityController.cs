using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FandomHub.Api.Controllers
{
    [Route("api/v1/communities")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
		private readonly ICommunityService _service;
        public CommunityController(ICommunityService communityService)
        {
            _service = communityService;
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCommunityById([FromRoute] int id)
		{
			try
			{
				var community = await _service.GetCommunityByIdActive(id);
				if (community == null) return NotFound(new { message = "Community not found" });
				return Ok(new { data = community });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllActiveCommunities()
		{
			try
			{
				var communities = await _service.GetAllActive();
				if (communities == null || !communities.Any()) return NotFound(new { message = "No communities found" });
				return Ok(new { data = communities });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCommunity([FromBody] CommunityCreateRequest request)
        {
            try
            {
                string userId = GetUserId();
                var community = await _service.CreateCommunity(request, userId);
                return Ok(new
                {
                    data = community
				});
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }	 
		}

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCommunity([FromBody] CommunityUpdateRequest request)
		{
			try
			{
				string userId = GetUserId();
				var community = await _service.UpdateCommunity(request, userId);
				return Ok(new
				{
					data = community
				});
			}
			catch (Exception ex)
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
