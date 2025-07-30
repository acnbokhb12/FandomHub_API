using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class FcmTokenRequest
	{
		public string Token { get; set; }
		public string UniqueId { get; set; }
		public string DeviceId { get; set; }
		public string? DeviceName { get; set; }
		public string DeviceType { get; set; }
		public string? AppVersion { get; set; }

	}
}
