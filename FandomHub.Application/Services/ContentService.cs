using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Common;
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
	public class ContentService : IContentService
	{
		private readonly IContentRepository _contentRepository;
		private readonly IContentEditHistoryRepository _contentEditHistoryRepository;
		private readonly ISlugHelper _slugHelper;
		private readonly IMapper _mapper;

        public ContentService(
			IContentRepository contentRepository, 
			IContentEditHistoryRepository contentEditHistoryRepository, 
			ISlugHelper slugHelper, 
			IMapper mapper)
        {
            _contentRepository = contentRepository;
			_contentEditHistoryRepository = contentEditHistoryRepository;
			_slugHelper = slugHelper;
			_mapper = mapper;
        }

		public async Task<bool> CheckSlugInContentWithContentType(SlugContentWithTypeRequest request)
		{
			var result = await _contentRepository.CheckSlugInContentWithContentType(request);
			return result;
		}

		public async Task<ContentResponse> CreateContent(ContentCreateRequest request, string userId)
		{
			DateTime now = DateTime.Now;
			DateTime trimmed = new DateTime(
				now.Year, now.Month, now.Day,
				now.Hour, now.Minute, now.Second
			);
			try
			{
				var content = _mapper.Map<Content>(request);
				content.Slug = _slugHelper.SlugifyEdit(content.Slug);
				content.CreatedById = userId;
				content.CreatedAt = trimmed;

				content.ContentCategories = request.CategoryIds
					.Select(catId => new ContentCategory
					{
						CategoryID = catId
					}).ToList();  

				var createdContent = await _contentRepository.CreateAsync(content);
				var history = new ContentEditHistory
				{
					ContentID = createdContent.ContentID,
					ChangeSummary = "Initial content creation",
					OldContent = request.ContentText,
					EditedById = userId,
					EditedAt = trimmed
				};
				var createdHistory = await _contentEditHistoryRepository.CreateAsync(history);

				var response = _mapper.Map<ContentResponse>(createdContent);
				 
				return response;
			}
			catch (Exception ex)
			{
				throw new Exception("Have some thing wrong in process create content");
			}  
		}
	}
}
