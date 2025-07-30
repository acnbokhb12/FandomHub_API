using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Category")]
	public class Category
	{
		[Key]
		public int CategoryID { get; set; }

		public string? Name { get; set; }

		public string? Slug { get; set; }

		public bool IsActive { get; set; } = true;

		public virtual ICollection<CommunityCategory> CommunityCategories { get; set; } = new List<CommunityCategory>();
	}
}
