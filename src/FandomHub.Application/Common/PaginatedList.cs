using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Common
{
	public class PaginatedList<T>
	{
		public List<T> Items { get; private set; }
		public int TotalCount { get; private set; }
		public int PageNumber { get; private set; }
		public int PageSize { get; private set; }
		public int TotalPages { get; private set; }

		public PaginatedList(List<T> items, int totalCount, int pageNumber, int pageSize)
		{
			Items = items;
			TotalCount = totalCount;
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		}

		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
	}

}
