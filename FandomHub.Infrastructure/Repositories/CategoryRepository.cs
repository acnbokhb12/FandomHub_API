using FandomHub.Application.DTOs.Response;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class CategoryRepository : BaseRepo<Category, int>, ICategoryRepository
	{ 
        public CategoryRepository(FandomHubDbContext context) : base(context)
		{
        }

		public async Task<List<Category>> GetCategoriesWithCondition()
		{
			return await _context.Categories.Where(c => c.IsActive == true)
				.ToListAsync();
		}

		public async Task<Category?> GetCategoryByIdWithCondition(int hubId)
		{
			return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == hubId && c.IsActive == true);
		}

	 
	}
}
