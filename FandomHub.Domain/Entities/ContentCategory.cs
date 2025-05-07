using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class ContentCategory
	{
		public int ContentID { get; set; }
		public Content? Content { get; set; }

		public int CategoryID { get; set; }
		public Category? Category { get; set; }
	}
}
