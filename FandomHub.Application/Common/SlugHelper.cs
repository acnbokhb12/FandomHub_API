using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FandomHub.Application.Common
{
	public class SlugHelper : ISlugHelper
	{
		public string SlugifyEdit(string slug)
		{
			if (string.IsNullOrWhiteSpace(slug))
				return string.Empty;

			slug = slug.ToLowerInvariant();

			// Remove accents (e.g., é → e)
			slug = RemoveDiacritics(slug);

			// Remove invalid chars
			slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

			// Convert multiple spaces into one underscore
			slug = Regex.Replace(slug, @"\s+", "_").Trim();

			// Remove trailing underscores
			slug = slug.Trim('_');

			return $"{slug}_Wiki"; ;
		}

		private string RemoveDiacritics(string text)
		{
			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in normalized)
			{
				var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}

			return sb.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}
