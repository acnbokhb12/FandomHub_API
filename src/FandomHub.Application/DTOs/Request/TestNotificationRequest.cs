using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class TestNotificationRequest
	{
		public string Token { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
	}
}
