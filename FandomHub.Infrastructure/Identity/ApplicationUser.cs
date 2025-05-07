using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string? ImgUrl { get; set; }
	}
}
