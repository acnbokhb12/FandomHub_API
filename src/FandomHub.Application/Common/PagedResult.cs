using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Common
{
	public class PagedResult<T>
	{
		public List<T> Items { get; set; } = new();
		public PaginationMetadata Metadata { get; set; } = new();

		public PagedResult(List<T> items, int totalCount, PaginationRequest request)
		{
			Items = items;
			Metadata = new PaginationMetadata
			{
				Page = request.GetValidPage(),
				PerPage = request.GetValidPerPage(),
				PageCount = items.Count,
				TotalCount = totalCount
			};
		}
	}
}
