using AutoMapper;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class HubService : BaseService<Hub, int>, IHubService
	{
		private readonly IHubRepository _hubRepo;
		private readonly IHubCategoryRepository _categoryRepo;
		private readonly IMapper _mapper;
		public HubService(IHubRepository hubRepository
			, IHubCategoryRepository hubCategoryRepository
			, IMapper mapper
			) : base(hubRepository)
		{
			_hubRepo = hubRepository;
			_categoryRepo = hubCategoryRepository;
			_mapper = mapper;
		}

		 
	}
}
