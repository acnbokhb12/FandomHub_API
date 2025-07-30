using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FandomHub.Application.DTOs.Request
{
	public class CommunityCreateRequest
	{
		public string? Name { get; set; } 

		public string? CoverImage { get; set; }

		public string? Slug { get; set; } 

		public string? Summary { get; set; }

		public int LanguagesId { get; set; }

		public int HubId { get; set; }

		public List<int> ListCategories { get; set; } = new List<int>();

	}
}
