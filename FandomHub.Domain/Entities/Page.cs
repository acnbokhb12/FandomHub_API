using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Page")]
	public class Page :AuditableEntity
	{
		[Key] 
		public int PageId { get; set; }

		public string? Title { get; set; }

		public string? Content { get; set; }

		public string Slug { get; set; } = null!;

		public int CommunityId { get; set; }

		public bool IsActive { get; set; } = true;

		public Community Community { get; set; } = null!;
	}
}
