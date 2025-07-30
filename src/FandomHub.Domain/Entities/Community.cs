using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Community")]
	public class Community : AuditableEntity
	{
		[Key]
		public int CommunityId { get; set; }

		public string? Name { get; set; }

		public string? SubName { get; set; }

		public string? LogoImage { get; set; }

		public string? CoverImage { get; set; }

		public string? Avatar { get; set; }

		public string? Slug {  get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; }

		public int ViewCount { get; set; } = 0;

		public bool IsActive { get; set; } = true;

		public int LanguagesId { get; set; }

		public int HubId { get; set; } 

		public Languages Languages { get; set; } = null!;

		public Hub Hub { get; set; } = null!;

		public virtual ICollection<WikiPage> WikiPages { get; set; } = new List<WikiPage>();

		public virtual ICollection<CommunityCategory> CommunityCategories { get; set; } = new List<CommunityCategory>();
	}
}
