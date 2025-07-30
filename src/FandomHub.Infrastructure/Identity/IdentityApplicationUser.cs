using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Identity
{
	public class IdentityApplicationUser : IdentityUser
	{
		public string? FullName { get; set; }

		public string? ImgUrl { get; set; }

		public DateTime? BirthDay { get; set; }
	}
}
