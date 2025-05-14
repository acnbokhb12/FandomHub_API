using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class CategoryByHubResponse
	{
		public int HubId { get; set; }

		public string Name { get; set; } = string.Empty;

		public string? Slug { get; set; }

		public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
	}
}
