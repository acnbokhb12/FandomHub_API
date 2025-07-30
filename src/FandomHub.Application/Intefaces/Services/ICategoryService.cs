using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface ICategoryService : IBaseService<Category, int>
	{
		Task<Category?> GetCategoryByIdWithCondition(int id);
		Task<List<Category>> GetCategoriesWithCondition(); 

	}
}
