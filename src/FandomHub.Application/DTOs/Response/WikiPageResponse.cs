using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class WikiPageResponse
	{
		public int WikiPageId { get; set; }

		public int CommunityId { get; set; }

		public string? Title { get; set; }

		public string? SubTitle { get; set; }

		public string? Avatar { get; set; }

		public string? Content { get; set; }

		public string Slug { get; set; } = null!;

		public int ViewCount { get; set; } = 0;

		public bool IsActive { get; set; } = true;

	}
}
