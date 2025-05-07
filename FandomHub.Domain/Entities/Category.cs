using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class Category
	{
		public int CategoryID { get; set; }

		public string? Name { get; set; }

		public string? Slug { get; set; }

		public bool isActive { get; set; } = true;

		public virtual ICollection<ContentCategory> ContentCategories { get; set; } = new List<ContentCategory>();
	}
}
