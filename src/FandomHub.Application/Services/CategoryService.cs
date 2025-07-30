using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class CategoryService : BaseService<Category, int>, ICategoryService
	{
		private readonly ICategoryRepository _categoryRepo;
		public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository) 
		{
			_categoryRepo = categoryRepository;
		}

		public async Task<List<Category>> GetCategoriesWithCondition()
		{ 
			return await _categoryRepo.GetCategoriesWithCondition();
		}

		public async Task<Category?> GetCategoryByIdWithCondition(int id)
		{
			var item = await _categoryRepo.GetCategoryByIdWithCondition(id);
			return item;
		}
	}
}
