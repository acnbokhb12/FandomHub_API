using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Domain.Entities
{
	[Table("Languages")]
	public class Languages
	{
		[Key]
		public int LanguagesId { get; set; }

		[MaxLength(10)]
		public string LanguageCode { get; set; } = null!;

		[Required]
		[MaxLength(100)]
		public string LanguageName { get; set; } = null!;

		public bool IsActive { get; set; } = true;

	}
}
