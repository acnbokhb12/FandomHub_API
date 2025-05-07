using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class ContentType
	{
		public int ContentTypeID { get; set; }

		public string? Name { get; set; }

		public string? Slug { get; set; }

		public string? Description { get; set; }

		public bool isActive { get; set; } = true;

		public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
	}
}
