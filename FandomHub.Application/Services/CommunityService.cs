using AutoMapper; 
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class CommunityService : BaseService<Community, int>, ICommunityService
	{
		private readonly ICommunityRepository _communityRepo;
		private readonly IEditHistoryRepository _editHistoryRepo;
		private readonly IHubCategoryRepository _hubCategoryRepo;
		private readonly ICommunityCategoryRepository _communityCategoryRepo;
		private readonly IMapper _mapper;
		public CommunityService(
			ICommunityRepository communityRepo,
			IEditHistoryRepository editHistoryRepo,
			IHubCategoryRepository hubCategoryRepo,
			ICommunityCategoryRepository communityCategoryRepo,
			IMapper mapper
			) : base(communityRepo)
		{
			_communityRepo = communityRepo;
			_editHistoryRepo = editHistoryRepo;
			_hubCategoryRepo = hubCategoryRepo;
			_communityCategoryRepo = communityCategoryRepo;
			_mapper = mapper;
		}
		public async Task<CommunityResponse?> GetCommunityByIdActive(int id)
		{
			try
			{
				var community = await _communityRepo.GetByIdActive(id);
				if (community == null) return null;
				return _mapper.Map<CommunityResponse>(community);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error fetching community by ID: {ex.Message}", ex);
			}
		}

		public async Task<List<CommunityResponse>> GetAllActive()
		{
			var activeCommunities = await _communityRepo.GetAllActive();
			return _mapper.Map<List<CommunityResponse>>(activeCommunities);
		}

		public async Task<CommunityResponse> CreateCommunity(CommunityCreateRequest request, string userId)
		{ 
			var community = _mapper.Map<Community>(request);
			community.CreatedAt = DateTime.Now.TrimToSecond(); 
			community.CreatedBy = userId;

			// Save the community
			var createdCommunity = await _communityRepo.CreateAsync(community);

			try
			{
				var list = request.ListCategories.Select(catId => new CommunityCategory
				{
					CommunityId = createdCommunity.CommunityId,
					CategoryID = catId
				}).ToList();

				await _communityCategoryRepo.CreateRangeAsync(list);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.InnerException?.Message);
				throw;
			}

			// Save to EditHistory
			var editHistory = new EditHistory
			{
				TargetEntityType = nameof(Community),
				TargetEntityId = createdCommunity.CommunityId,
				PreviousContent = null, // No previous content on create
				ChangeSummary = "Community created",
				CreatedBy = userId,
				CreatedAt = DateTime.Now.TrimToSecond()
			};
			await _editHistoryRepo.CreateAsync(editHistory);

			// Map to response DTO
			var response = _mapper.Map<CommunityResponse>(createdCommunity);

			return response;
		}

		public async Task<CommunityResponse?> UpdateCommunity(CommunityUpdateRequest request, string userId)
		{
			try
			{
				var community = await _communityRepo.GetByIdActive(request.CommunityId);
				if (community == null) return null;

				DateTime now = DateTime.Now;
				DateTime trimmed = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

				_mapper.Map(request, community);
				community.UpdatedAt = DateTime.Now.TrimToSecond();
				community.UpdatedBy = userId;

				// Save to EditHistory
				var editHistory = new EditHistory
				{
					TargetEntityType = nameof(Community),
					TargetEntityId = request.CommunityId,
					PreviousContent = null,
					ChangeSummary = "Community updated",
					UpdatedBy = userId,
					UpdatedAt = DateTime.Now
				};
				await _editHistoryRepo.CreateAsync(editHistory);

				return _mapper.Map<CommunityResponse>(community);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error updating community: {ex.Message}", ex);
			}
		}

		public async Task<bool> DeleteCommunity(int id, string userId)
		{
			try
			{

				var community = await _communityRepo.GetByIdActive(id);
				if (community == null) return false;
				community.IsActive = false;
				community.DeleteAt = DateTime.Now.TrimToSecond();
				community.DeleteBy = userId;

				var editHistory = new EditHistory
				{
					TargetEntityType = nameof(Community),
					TargetEntityId = id,
					PreviousContent = null,
					ChangeSummary = "Community deleted",
					CreatedBy = userId,
					CreatedAt = DateTime.Now.TrimToSecond()
				};
				await _editHistoryRepo.CreateAsync(editHistory);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting community: {ex.Message}", ex);
			}
		}

		public async Task<PagedCommunityResponse> GetAllActivePagedAsync(PaginationRequest request, string baseUrl)
		{
			var validPage = request.GetValidPage();
			var validPerPageSize = request.GetValidPerPage();

			var paginatedCommunities = await _communityRepo.GetAllActivePagedAsync(validPage, validPerPageSize);
			var communityResponses = _mapper.Map<List<CommunityResponse>>(paginatedCommunities.Items);

			return new PagedCommunityResponse
			{
				Data = communityResponses,
				Metadata = new PaginationMetadata
				{
					Page = validPage,
					PerPage = validPerPageSize,
					PageCount = communityResponses.Count,
					TotalCount = paginatedCommunities.TotalCount,
					Links = BuildPaginationLinks(paginatedCommunities, baseUrl, validPerPageSize)
				}
			};
		}

		private PaginationLinks BuildPaginationLinks( 
			PaginatedList<Community> paginatedList,
			string baseUrl,
		    int pageSize)
		{
			return new PaginationLinks
			{
				Self = $"{baseUrl}?page={paginatedList.PageNumber}&per_page={pageSize}",
				First = $"{baseUrl}?page=1&per_page={pageSize}",
				Previous = paginatedList.HasPreviousPage
					? $"{baseUrl}?page={paginatedList.PageNumber - 1}&per_page={pageSize}"
					: null,
				Next = paginatedList.HasNextPage
					? $"{baseUrl}?page={paginatedList.PageNumber + 1}&per_page={pageSize}"
					: null,
				Last = $"{baseUrl}?page={paginatedList.TotalPages}&per_page={pageSize}"
			};
		}
	}
}
