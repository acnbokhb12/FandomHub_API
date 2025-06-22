using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class CommunityRepository : BaseRepo<Community, int>,ICommunityRepository
	{ 
		public CommunityRepository(FandomHubDbContext context) : base(context) 
		{ 
		}

		public Task<List<Community>> GetAllActive()
		{
			return _context.Communities.Where(c => c.IsActive)
				.ToListAsync();
		}

		public async Task<PaginatedList<Community>> GetAllActivePagedAsync(int page, int pageSize)
		{
			return await _context.Communities
				.Where(c => c.IsActive)
				.ToPaginatedListAsync(page, pageSize);
		}

		public Task<Community?> GetByIdActive(int id)
		{
			return _context.Communities.FirstOrDefaultAsync(c => c.CommunityId == id && c.IsActive);
		}
	}
}
