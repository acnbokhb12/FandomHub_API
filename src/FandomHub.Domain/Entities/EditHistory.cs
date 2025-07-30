using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("EditHistory")]
	public class EditHistory : AuditableEntity
	{
		[Key]
		public int Id { get; set; }	

		public string? TargetEntityType { get; set; }

		public int TargetEntityId {  get; set; }

		public string? PreviousContent { get; set; }

		public string? ChangeSummary { get; set; }

		public bool IsActive { get; set; } = true;


	}
}
