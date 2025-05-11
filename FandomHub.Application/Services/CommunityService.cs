using AutoMapper;
using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class CommunityService : BaseService<Community, int>,ICommunityService
	{
		private readonly ICommunityRepository _communityRepo;
		private readonly IMapper _mapper;
        public CommunityService(
			ICommunityRepository communityRepo,
			IMapper mapper
			) : base(communityRepo) 
        {
            _communityRepo = communityRepo;
			_mapper = mapper;
        }
        public Task<CommunityResponse> CreateCommunity(CommunityCreateRequest request, string userId)
		{
			DateTime now = DateTime.Now;
			DateTime trimmed = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

			var community = _mapper.Map<Community>( request );
			community.CreatedAt = trimmed;
			community.CreatedBy = userId;


			throw new NotImplementedException();
		}
	}
}
