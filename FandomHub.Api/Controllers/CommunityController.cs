using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Application.Services;
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

		 
		[HttpGet]
		public async Task<ActionResult<PagedCommunityResponse>> GetAll(
			[FromQuery] int page = 1,
			[FromQuery] int per_page = 20,
			[FromQuery] string include = "")
		{
			try
			{
				var request = new PaginationRequest
				{
					Page = page,
					PerPage = per_page
				};
				var baseUrl = $"{Request.Path}{Request.QueryString}";
				bool includeMetadata = include?.Contains("metadata") ?? true;
				if (!includeMetadata)
				{
					// Return simple list without pagination metadata
					var simpleResult = await _service.GetAllActivePagedAsync(request, baseUrl); 
					return Ok(simpleResult);
				}
				var result = await _service.GetAllActivePagedAsync(request, baseUrl);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
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




		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommunityCreateRequest request)
        {
            try
            {
                string userId = GetUserId();
                var result = await _service.CreateCommunity(request, userId);
				return CreatedAtAction(
					nameof(GetById),
					new { id = result.CommunityId },
					result);
			}
			catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }	 
		}

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CommunityUpdateRequest request)
		{
			try
			{
				string userId = GetUserId();
				var result = await _service.UpdateCommunity(request, userId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			try
			{
				string userId = GetUserId();
				var result = await _service.DeleteCommunity(id, userId);
				if (!result) return NotFound(new { message = "Community not found or you do not have permission to delete it" });
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}							

		//[HttpGet]
		//public async Task<IActionResult> GetAllActiveCommunities()
		//{
		//	try
		//	{
		//		var communities = await _service.GetAllActive();
		//		if (communities == null || !communities.Any()) return NotFound(new { message = "No communities found" });
		//		return Ok(new { data = communities });
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(new { message = ex.Message });
		//	}
		//}

		private string GetUserId()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return  userId;
		}
	}
}
