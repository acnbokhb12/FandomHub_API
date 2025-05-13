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
	public class ApplicationHubSeeder
	{
		public static async Task SeedAsync(FandomHubDbContext context)
		{
			var hubs = new List<Hub>
		{
			new Hub { Name = "Games", Slug = "games"},
			new Hub { Name = "Movies", Slug = "movies" },
			new Hub { Name = "Anime", Slug = "anime" },
			new Hub { Name = "Comics", Slug = "comics" },
			new Hub { Name = "Books", Slug = "books" },
			new Hub { Name = "TV", Slug = "tv" },
			new Hub { Name = "Lifestyle", Slug = "lifestyle" },
			new Hub { Name = "Orther", Slug = "orther"  },
		};

			foreach (var hub in hubs)
			{
				if (!await context.Hubs.AnyAsync(h => h.Name.ToLower() == hub.Name.ToLower()))
				{
					await context.Hubs.AddAsync(hub);
				}
			}
			await context.SaveChangesAsync();
		}
	}
}
