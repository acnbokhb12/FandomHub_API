using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Token { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public bool Revoked { get; set; }
		public bool IsActive => !Revoked && DateTime.UtcNow < ExpiresAt;
	}
}
