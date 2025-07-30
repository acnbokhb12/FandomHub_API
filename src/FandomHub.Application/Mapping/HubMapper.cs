using AutoMapper;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Mapping
{
	public class HubMapper : Profile
	{
		public HubMapper() 
		{
			CreateMap<Hub, CategoryByHubResponse>()
			.ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
				src.HubCategories
					.Where(hc => hc.Category != null && hc.Category.IsActive)
					.Select(hc => hc.Category)
			));
		}
	}
}
