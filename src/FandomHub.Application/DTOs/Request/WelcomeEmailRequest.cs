using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class WelcomeEmailRequest
	{
		public string UserName { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
	}
}
