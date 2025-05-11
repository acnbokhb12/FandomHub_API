using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Character")]
	public class Character : AuditableEntity
	{
		[Key]
		public int CharacterId { get; set; }

		public int? CommunityId { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? Avatar { get; set; }

		public bool isActive { get; set; } = true;

		[ForeignKey("CommunityId")]
		public virtual Community Community { get; set; } = null!;

		public virtual ICollection<CharacterAttributeGroup> CharacterAttributeGroups { get; set; } = new List<CharacterAttributeGroup>();
	}
}
