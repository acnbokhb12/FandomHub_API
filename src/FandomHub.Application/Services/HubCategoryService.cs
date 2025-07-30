using AutoMapper; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class HubCategoryService : IHubCategoryService
	{
		private readonly IHubCategoryRepository _hubCategoryRepository;
		private readonly IMapper _mapper;
		public HubCategoryService(
			IHubCategoryRepository hubCategoryRepository,
			IMapper mapper)
		{
			_hubCategoryRepository = hubCategoryRepository;
			_mapper = mapper;
		}
		public async Task<List<CategoryResponse>> GetCategoriesByHubId(int hubId)
		{
			var list = await _hubCategoryRepository.GetCategoriesByHubId(hubId);
			return _mapper.Map<List<CategoryResponse>>(list);
		}
	}
}
