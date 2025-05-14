using FandomHub.Application.Intefaces.Services;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IHubCategoryRepository 
	{
		public Task<List<Category>> GetCategoriesByHubId(int hubId);
	}
}
