using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("CommunityCategory")]
	public class CommunityCategory
	{ 
		public int CommunityId { get; set; }
		public Community? Community { get; set; } 

		public int CategoryID { get; set; }
		public Category? Category { get; set; } 
	}
}
