using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class PaginationRequest
	{
		public int Page { get; set; } = 1;
		public int PerPage { get; set; } = 20;

		public int GetValidPage() => Page < 1 ? 1 : Page;
		public int GetValidPerPage() => PerPage < 1 ? 20 : (PerPage > 100 ? 100 : PerPage);
	}
}
