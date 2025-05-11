using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Mapping
{
	public class CommunityMapper : Profile
	{
		public CommunityMapper()
		{
			CreateMap<CommunityCreateRequest, Community>();
		}
	}
}
