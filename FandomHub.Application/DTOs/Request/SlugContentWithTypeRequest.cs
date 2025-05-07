using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Request
{
	public class SlugContentWithTypeRequest
	{
		public string Slug {  get; set; }
		
		public int ContentTypeID { get; set; }
	}
}
