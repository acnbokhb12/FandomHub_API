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
	public class ApplicationHubCategorySeeder
	{
		public static async Task SeedAsync(FandomHubDbContext context)
		{
			var hubCategoryMap = new Dictionary<string, List<string>>
	{
		{ "Games", new List<string> { "Music", "TV", "Video Games", "Books", "Comics", "Fanon", "Movies", "Anime" } },
		{ "Movies", new List<string> { "Music", "TV", "Video Games", "Books", "Comics", "Fanon", "Anime" } },
		{ "Anime", new List<string> { "Music", "TV", "Video Games", "Books", "Comics", "Fanon", "Movies" } },
		{ "Comics", new List<string> { "Music", "TV", "Video Games", "Books", "Fanon", "Movies", "Anime" } },
		{ "Books", new List<string> { "Music", "TV", "Video Games", "Comics", "Fanon", "Movies", "Anime" } },
		{ "TV", new List<string> { "Music", "Video Games", "Books", "Comics", "Fanon", "Movies", "Anime" } },
		{ "Music", new List<string> { "TV", "Video Games", "Books", "Comics", "Fanon", "Movies", "Anime" } },
		{ "Lifestyle", new List<string> { "Humor", "Toys", "Food and Drink", "Travel", "Education", "Finance", "Politics", "Technology", "Science", "Philosophy", "Sports", "Creative", "Auto", "Home and Garden" } },
		{ "Orther", new List<string> { "Music", "TV", "Video Games", "Books", "Comics", "Fanon", "Movies", "Anime" } },
	};

			foreach (var hubEntry in hubCategoryMap)
			{
				var hubName = hubEntry.Key.Trim().ToLower();
				var hub = await context.Hubs
					.FirstOrDefaultAsync(h => h.Name.ToLower().Trim() == hubName);

				if (hub == null)
				{ 
					continue;
				}
				 

				foreach (var categoryNameRaw in hubEntry.Value)
				{
					var categoryName = categoryNameRaw.Trim().ToLower();
					var category = await context.Categories
						.FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == categoryName);

					if (category == null)
					{ 
						continue;
					}

					var exists = await context.HubCategories
						.AnyAsync(hc => hc.HubId == hub.HubId && hc.CategoryID == category.CategoryID);

					if (!exists)
					{
						await context.HubCategories.AddAsync(new HubCategory
						{
							HubId = hub.HubId,
							CategoryID = category.CategoryID
						}); 
					}
					else
					{
						 
					}
				}
			}

			await context.SaveChangesAsync(); 
		}

	}
}
