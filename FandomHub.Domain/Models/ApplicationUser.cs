using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Models
{
	public class ApplicationUser
	{
		public string Id { get; set; }
		public string UserName { get; set; } 
		public string FullName { get; set; }
		public string Role {  get; set; }
	}
}
