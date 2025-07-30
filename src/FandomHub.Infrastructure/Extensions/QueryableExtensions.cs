using FandomHub.Application.Common;
using FandomHub.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Extensions
{
	public static class QueryableExtensions
	{
		public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
			this IQueryable<T> query,
			int pageNumber,
			int pageSize)
		{
			var totalCount = await query.CountAsync();
			var items = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PaginatedList<T>(items, totalCount, pageNumber, pageSize);
		}
	}
}
