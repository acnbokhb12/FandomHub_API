using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Common;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FandomHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FandomHub.Infrastructure.Repositories
{
	public class ContentRepository : BaseRepo<Content, int>, IContentRepository
	{
		private readonly FandomHubDbContext _context;
		public ContentRepository(FandomHubDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> CheckSlugInContentWithContentType(SlugContentWithTypeRequest request)
		{
			var contentID = await _context.Contents.Where(c => c.Slug.Equals(request.Slug, StringComparison.OrdinalIgnoreCase) && c.ContentTypeID == request.ContentTypeID).Select(c=>c.ContentID).FirstOrDefaultAsync();
			if(contentID == 0)
			{
				return false;
			}
			return true; 
		}
	}
}
