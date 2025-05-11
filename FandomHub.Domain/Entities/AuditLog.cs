using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
    public class AuditLog
    {
		public int Id { get; set; }
		public string? Action { get; set; } // Create, Update, Delete
		public string? EntityName { get; set; }
		public int? EntityId { get; set; }
		public string? OriginalValue { get; set; }
		public string? NewValue { get; set; }
		public string? PerformedBy { get; set; }
		public DateTime PerformedAt { get; set; }
	}
}
