using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class NotificationType
	{
		public int NotificationTypeId { get; set; }
		public string? Name { get; set; }  
		public string? Description { get; set; } 
	}
}
