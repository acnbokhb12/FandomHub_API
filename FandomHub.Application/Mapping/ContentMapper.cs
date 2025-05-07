using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Mapping
{
	public class ContentMapper : Profile
	{
		public ContentMapper()
		{
			CreateMap<ContentCreateRequest, Content>();
			CreateMap<Content, ContentResponse>();
		}
	}
}
