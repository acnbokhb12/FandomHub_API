using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("HubCategory")]
	public class HubCategory
	{
		public int HubId { get; set; }
		public Hub Hub { get; set; } = null!;

		public int CategoryID { get; set; }
		public Category Category { get; set; } = null!;
	}
}
