using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Common
{
	public class DatabasePaginationResult<T>
	{
		public List<T> Items { get; set; } = new();
		public int TotalCount { get; set; }

		public DatabasePaginationResult(List<T> items, int totalCount)
		{
			Items = items;
			TotalCount = totalCount;
		}
	}
}
