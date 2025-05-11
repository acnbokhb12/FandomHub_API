using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("CharacterAttribute")]
	public class CharacterAttribute : AuditableEntity
	{
		[Key]
		public int CharacterAttributeId { get; set; }

		public int CharacterAttributeGroupId { get; set; }

		public string? Key { get; set; }

		public string? Value { get; set; }

		[ForeignKey("CharacterAttributeGroupId")]
		public virtual CharacterAttributeGroup? CharacterAttributeGroup { get; set; } 
	}
}
