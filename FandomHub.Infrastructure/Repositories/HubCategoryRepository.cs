namespace FandomHub.Infrastructure.Repositories
{
	public class HubCategoryRepository : IHubCategoryRepository
	{
		private readonly FandomHubDbContext _context;
		public HubCategoryRepository(FandomHubDbContext context)
		{
			_context = context;
		}
		public async Task<List<Category>> GetCategoriesByHubId(int hubId)
		{
			return await _context.HubCategories
				.Where(hc => hc.HubId == hubId)
				.Include(hc => hc.Category)
				.Select(hc => hc.Category)
				.ToListAsync(); 
		}
	}
}
