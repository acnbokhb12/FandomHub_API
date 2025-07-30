using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("WikiPage")]
	public class WikiPage : AuditableEntity
	{
		[Key] 
		public int WikiPageId { get; set; }

		public int CommunityId { get; set; }

		public string? Title { get; set; }

		public string? SubTitle { get; set; }

		public string? Avatar { get; set; }

		public string? Content { get; set; }

		public string Slug { get; set; } = null!;

		public int ViewCount { get; set; } = 0;

		public bool IsActive { get; set; } = true;

		public Community Community { get; set; } = null!; 
	}
}
