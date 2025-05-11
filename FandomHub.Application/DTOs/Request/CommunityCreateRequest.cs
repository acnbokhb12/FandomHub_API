using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class CommunityCreateRequest
	{
		public string? Title { get; set; }

		public string? LogoImage { get; set; }

		public string? CoverImage { get; set; }

		public string? Slug { get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; }

	}
}
