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
	public class ApplicationLanguagesSeeder
	{
		public static async Task SeedAsync(FandomHubDbContext context)
		{
			var languages = new List<Languages>
	{
		new Languages { LanguageCode = "en", LanguageName = "English", IsActive = true },
		new Languages { LanguageCode = "vi", LanguageName = "Vietnamese", IsActive = true },
		new Languages { LanguageCode = "jp", LanguageName = "Japanese", IsActive = true }
	};

			foreach (var lang in languages)
			{
				bool exists = await context.Languages.AnyAsync(l => l.LanguageCode == lang.LanguageCode);
				if (!exists)
				{
					await context.Languages.AddAsync(lang);
				}
			}

			await context.SaveChangesAsync();
		}

	}
}
