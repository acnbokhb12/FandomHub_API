using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly FandomHubDbContext _context;
        public CategoryRepository(FandomHubDbContext context)
        {
            _context = context;
        }
        public Task GetListCategoriesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
