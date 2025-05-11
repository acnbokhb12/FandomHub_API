using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("CharacterAttributeGroup")]
	public class CharacterAttributeGroup : AuditableEntity
	{
		[Key]
		public int CharacterAttributeGroupId { get; set; }

		public int CharacterId { get; set; }

		public string? Name { get; set; }

		[ForeignKey("CharacterId")]
		public virtual Character? Character { get; set; }  

		public virtual ICollection<CharacterAttribute> CharacterAttributes {  get; set; } = new List<CharacterAttribute>();
	}
}
