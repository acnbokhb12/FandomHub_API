﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class FcmToken
	{
		public int FcmTokenId {get; set; }
		[Required]
		public string UserId { get; set; }
		[Required]
		public string Token { get; set; }
		[Required]
		public string DeviceId { get; set; }

		public string? DeviceName { get; set; }

		public string DeviceType { get; set; }

		public string? AppVersion { get; set; }

		public bool IsActive { get; set; } = true;

		public DateTime LastLogin { get; set; } = DateTime.UtcNow;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
