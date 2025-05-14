using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class CategoryResponse
	{
		public int CategoryID { get; set; }

		public string? Name { get; set; }

		public string? Slug { get; set; }
	}
}
