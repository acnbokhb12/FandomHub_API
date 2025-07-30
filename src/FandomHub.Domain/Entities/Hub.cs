using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Hub")]
	public class Hub
	{
		[Key]
		public int HubId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Slug { get; set; }
		public bool IsActive { get; set; } = true;

		public virtual ICollection<HubCategory> HubCategories { get; set; } = new List<HubCategory>();
	}
}
