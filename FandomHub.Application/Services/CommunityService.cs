using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
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
		public async Task<CommunityResponse> CreateCommunity(CommunityCreateRequest request, string userId)
		{
			DateTime now = DateTime.Now;
			DateTime trimmed = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

			var community = _mapper.Map<Community>(request);
			community.CreatedAt = trimmed;
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
				CreatedAt = trimmed
			};
			await _editHistoryRepo.CreateAsync(editHistory);

			// Map to response DTO
			var response = _mapper.Map<CommunityResponse>(createdCommunity);

			return response;
		}
	}
}
