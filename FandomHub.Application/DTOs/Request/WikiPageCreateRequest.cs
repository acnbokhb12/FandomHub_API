using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class WikiPageCreateRequest
	{
		public string? Title { get; set; }

		public string? SubTitle { get; set; }

		public string? Avatar { get; set; }

		public string? Content { get; set; }

	}
}
