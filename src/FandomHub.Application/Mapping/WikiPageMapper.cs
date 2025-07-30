using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;


namespace FandomHub.Application.Mapping
{
	public class WikiPageMapper : Profile
	{
		public WikiPageMapper() 
		{
			CreateMap<WikiPageCreateRequest, WikiPage>();
			CreateMap<WikiPage, WikiPageResponse>();

		}
	}
}
