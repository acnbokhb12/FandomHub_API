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
			new Category { Name = "TV", Slug = "tv", isActive = true },
			new Category { Name = "Video Games", Slug = "video_games", isActive = true },
			new Category { Name = "Anime", Slug = "anime", isActive = true },
			new Category { Name = "Movies", Slug = "movies", isActive = true },
			new Category { Name = "Fanon", Slug = "fanon", isActive = true },
			new Category { Name = "Books", Slug = "books", isActive = true },
			new Category { Name = "Comics", Slug = "comics", isActive = true },
			new Category { Name = "Music", Slug = "music", isActive = true },

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
