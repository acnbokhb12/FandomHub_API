using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class CommunityResponse
	{
		public int CommunityId { get; set; }

		public string? Name { get; set; }

		public string? SubName { get; set; }

		public string? LogoImage { get; set; }

		public string? CoverImage { get; set; }

		public string? Avatar { get; set; }

		public int ViewCount { get; set; }

		public string? Slug { get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; }

		public int LanguagesId { get; set; }

		public int HubId { get; set; }

		public bool isActive { get; set; }

	}
}
