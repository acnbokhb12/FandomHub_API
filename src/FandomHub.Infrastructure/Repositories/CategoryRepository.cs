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
