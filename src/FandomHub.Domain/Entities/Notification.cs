using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Notification")]
	public class Notification
	{
		[Key]
		public int NotificationId { get; set; }

		public string UserId { get; set; }  

		public int NotificationTypeId { get; set; }

		public NotificationType NotificationType { get; set; } = null!;
		
		public string? Message { get; set; }

		public bool IsRead { get; set; } = false;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public int? EntityId { get; set; }

		public string EntityType { get; set; } // Lưu loại thực thể (
	}
}
