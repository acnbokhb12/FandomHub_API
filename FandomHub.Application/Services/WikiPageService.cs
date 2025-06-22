using AutoMapper;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class WikiPageService : BaseService<WikiPage, int>, IWikiPageService
	{
		private readonly IWikiPageRepository _wikiPageRepository;
		private readonly IEditHistoryRepository _editHistoryRepo;
		private readonly ISlugHelper _slugHelper;
		private readonly IMapper _mapper;
		public WikiPageService(
			IWikiPageRepository wikiPageRepository,
			IEditHistoryRepository editHistoryRepo,
			ISlugHelper slugHelper,
			IMapper mapper
			) : base(wikiPageRepository)
		{
			_wikiPageRepository = wikiPageRepository;
			_editHistoryRepo = editHistoryRepo;
			_slugHelper = slugHelper;
			_mapper = mapper;
		}

		public async Task<WikiPageResponse?> CreateWikiPage(WikiPageCreateRequest request, string userId)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(request.Title))
					throw new ArgumentException("Title is required");

				string slug = _slugHelper.SlugifyEdit(request.Title ?? string.Empty);
				var wikiPage = _mapper.Map<WikiPage>(request);
				wikiPage.Slug = slug;
				wikiPage.CreatedAt = DateTime.Now.TrimToSecond();
				wikiPage.CreatedBy = userId;

				var newWikiPage = await _wikiPageRepository.CreateAsync(wikiPage);
				var editHistory = new EditHistory
				{
					TargetEntityType = nameof(WikiPage),
					TargetEntityId = newWikiPage.WikiPageId,
					PreviousContent = null, // No previous content on create
					ChangeSummary = "Wiki page created",
					CreatedBy = userId,
					CreatedAt = DateTime.Now.TrimToSecond()
				}; 
				await _editHistoryRepo.CreateAsync(editHistory);

				return _mapper.Map<WikiPageResponse>(newWikiPage);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating wiki page: {ex.Message}", ex);
			}
		}

		public async Task<WikiPageResponse?> GetWikiPageByIdAsync(int id)
		{
			try
			{
				var wikiPage = await _wikiPageRepository.GetWikiPageByIdAsync(id);
				return _mapper.Map<WikiPageResponse>(wikiPage);
			}
			catch
			{
				throw;
			}
		}
	}
}
