using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class Content
	{
		public int ContentID { get; set; }

		public string? Title { get; set; }

		public string? Slug { get; set; }

		public string? CoverImage { get; set; }

		public string? ContentText { get; set; }

		public string? Summary { get; set; } 

		public string? CreatedById { get; set; }

		public DateTime CreatedAt { get; set; }  

		public DateTime? UpdatedAt { get; set; }

		public bool IsPublished { get; set; }

		public bool isActive { get; set; } = true;

		public int ContentTypeID { get; set; }

		public virtual ContentType? ContentType { get; set; }

		public virtual ICollection<ContentCategory> ContentCategories { get; set; } = new List<ContentCategory>();

		public virtual ICollection<ContentEditHistory> EditHistories { get; set; } = new List<ContentEditHistory>();
	}
}
