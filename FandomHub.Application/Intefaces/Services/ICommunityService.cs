using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface ICommunityService : IBaseService<Community, int>
	{
		Task<CommunityResponse?> GetCommunityByIdActive(int id);

		Task<List<CommunityResponse>> GetAllActive();

		Task<CommunityResponse> CreateCommunity(CommunityCreateRequest request, string userId);

		Task<CommunityResponse?> UpdateCommunity(CommunityUpdateRequest request, string userId);

		Task<bool> DeleteCommunity(int id, string userId);

		Task<PagedCommunityResponse> GetAllActivePagedAsync(PaginationRequest request, string baseUrl);


	}
}
