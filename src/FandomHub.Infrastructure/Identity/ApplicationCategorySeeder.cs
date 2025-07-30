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
		public static async Task SeedAsync(FandomHubDbContext context)
		{
			var categories = new List<Category>
		{
			new Category { Name = "Music", Slug = "music"},
			new Category { Name = "TV", Slug = "tv" },
			new Category { Name = "Video Games", Slug = "video-games"},
			new Category { Name = "Books", Slug = "book" },
			new Category { Name = "Comics", Slug = "comics" },
			new Category { Name = "Fanon", Slug = "fanon" },
			new Category { Name = "Movies", Slug = "movies" },
			new Category { Name = "Anime", Slug = "anime" },
			new Category { Name = "Humor", Slug = "humor" },
			new Category { Name = "Toys", Slug = "toys" },
			new Category { Name = "Food and Drink", Slug = "food-and-drink" },
			new Category { Name = "Travel", Slug = "travel" },
			new Category { Name = "Education", Slug = "education" },
			new Category { Name = "Finance", Slug = "finance" },
			new Category { Name = "Politics", Slug = "politics" },
			new Category { Name = "Technology", Slug = "technology" },
			new Category { Name = "Science", Slug = "science" },
			new Category { Name = "Philosophy", Slug = "philosophy" },
			new Category { Name = "Sports", Slug = "sports" },
			new Category { Name = "Creative", Slug = "creative" },
			new Category { Name = "Auto", Slug = "auto" },
			new Category { Name = "Home and Garden", Slug = "home-and-garden" },

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
