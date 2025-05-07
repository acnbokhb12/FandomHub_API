using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	public class ContentEditHistory
	{
		public int HistoryID { get; set; }
		public string? ChangeSummary { get; set; }
		public string? OldContent { get; set; }
		public bool isActive { get; set; } = true;

		[MaxLength(450)]  
		public string? EditedById { get; set; }
		public DateTime EditedAt { get; set; }

		public int ContentID { get; set; }
		public Content? Content { get; set; }
	}
}
