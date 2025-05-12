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
	public class ApplicationCategorySeeder
	{
		public static async Task SeedCategoriesAsync(FandomHubDbContext context)
		{
			var categories = new List<Category>
		{
			new Category { Name = "TV", Slug = "tv", IsActive = true },
			new Category { Name = "Video Games", Slug = "video_games", IsActive = true },
			new Category { Name = "Anime", Slug = "anime", IsActive = true },
			new Category { Name = "Movies", Slug = "movies", IsActive = true },
			new Category { Name = "Fanon", Slug = "fanon", IsActive = true },
			new Category { Name = "Books", Slug = "books", IsActive = true },
			new Category { Name = "Comics", Slug = "comics", IsActive = true },
			new Category { Name = "Music", Slug = "music", IsActive = true },

		};

			foreach (var category in categories)
			{
				bool exists = await context.Categories
					.AnyAsync(c => c.Name!.ToLower() == category.Name!.ToLower());

				if (!exists)
				{
					await context.Categories.AddAsync(category);
				}
			}

			await context.SaveChangesAsync();
		}
	}
}
