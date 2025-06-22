namespace FandomHub.Infrastructure.Repositories
{
	public class CommunityCategoryRepository : ICommunityCategoryRepository
	{
		private readonly FandomHubDbContext _context;
		public CommunityCategoryRepository(FandomHubDbContext dbContext)
		{
			_context = dbContext;
		} 

		public async Task CreateAsync(CommunityCategory entity)
		{
			_context.CommunityCategories.Add(entity);
			await _context.SaveChangesAsync(); 
		} 

		public async Task CreateRangeAsync(List<CommunityCategory> entities)
		{
			_context.CommunityCategories.AddRange(entities);
			await _context.SaveChangesAsync();
		}
	}
}
