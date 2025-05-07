using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class ContentCreateRequest
	{
		public string? Title { get; set; }

		public string? Slug { get; set; } 

		public string? CoverImage { get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; }

		public int ContentTypeID { get; set; }

		public List<int> CategoryIds { get; set; } = new();
	}
}
