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

		public string? Title { get; set; }  

		public string? LogoImage { get; set; }

		public string? CoverImage { get; set; }

		public string? Slug {  get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; }

		public bool isActive { get; set; } = true;

		public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

		public virtual ICollection<CommunityCategory> CommunityCategories { get; set; } = new List<CommunityCategory>();
	}
}
