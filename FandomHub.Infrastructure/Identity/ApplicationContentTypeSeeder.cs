using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Identity
{
	public class ApplicationContentTypeSeeder
	{
		public static async Task SeedContentTypesAsync(FandomHubDbContext context)
		{
			var contentTypes = new List<ContentType>
		{
			new ContentType { Name = "News", Slug = "news", Description = "Latest updates and announcements", isActive = true },
			new ContentType { Name = "Blogs", Slug = "blogs", Description = "User or staff blog entries", isActive = true },
			new ContentType { Name = "Articles", Slug = "articles", Description = "Long-form informational content", isActive = true },
			new ContentType { Name = "Gallery", Slug = "gallery", Description = "Image or media galleries", isActive = true },
			new ContentType { Name = "Wikis", Slug = "wikis", Description = "Wikis for everythings", isActive = true }
		};

			foreach (var type in contentTypes)
			{
				bool exists = await context.ContentTypes
					.AnyAsync(ct => ct.Name!.ToLower() == type.Name!.ToLower());

				if (!exists)
				{
					await context.ContentTypes.AddAsync(type);
				}
			}

			await context.SaveChangesAsync();
		}
	}
}
