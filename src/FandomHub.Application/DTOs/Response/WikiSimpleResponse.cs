using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class WikiSimpleResponse
	{
		public int WikiId { get; set; }

		public string? Title { get; set; }

		public string? SubTitle { get; set; }

		public string? Avatar { get; set; }

		public string? Slug { get; set; }
	}
}
