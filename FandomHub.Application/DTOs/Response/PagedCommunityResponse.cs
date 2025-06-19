using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class PagedCommunityResponse
	{
		public List<CommunityResponse> Data { get; set; } = new();
		public PaginationMetadata Metadata { get; set; } = new();
	}

	public class PaginationMetadata
	{
		public int Page { get; set; }
		public int PerPage { get; set; }
		public int PageCount { get; set; }
		public int TotalCount { get; set; }
		public PaginationLinks Links { get; set; } = new();
	}

	public class PaginationLinks
	{
		public string Self { get; set; } = string.Empty;
		public string First { get; set; } = string.Empty;
		public string? Previous { get; set; }
		public string? Next { get; set; }
		public string Last { get; set; } = string.Empty;
	}
}
