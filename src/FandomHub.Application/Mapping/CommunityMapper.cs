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
	public class CommunityMapper : Profile
	{
		public CommunityMapper()
		{
			CreateMap<CommunityUpdateRequest, Community>()
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
			srcMember != null && !(srcMember is string str && string.IsNullOrWhiteSpace(str))));

			CreateMap<CommunityCreateRequest, Community>();
			CreateMap<Community, CommunityResponse>();
		}
	}
}
