namespace FandomHub.Infrastructure.Repositories
{
	public class WikiPageRepository : BaseRepo<WikiPage, int>, IWikiPageRepository
	{
		public WikiPageRepository(FandomHubDbContext context) : base(context)
		{
			
		}

		public async Task<WikiPage?> GetWikiPageByIdAsync(int id)
		{
			try
			{
				return await _context.WikiPages
					.FirstOrDefaultAsync(wp => wp.WikiPageId == id && wp.IsActive == true);
			}
			catch
			{
				throw;
			}
		}
	}
}
