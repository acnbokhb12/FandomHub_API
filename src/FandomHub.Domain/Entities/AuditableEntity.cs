using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public abstract class AuditableEntity
	{
		public string? CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public string? DeleteBy { get; set; }
		public DateTime? DeleteAt { get; set; }
		 

	}
}
