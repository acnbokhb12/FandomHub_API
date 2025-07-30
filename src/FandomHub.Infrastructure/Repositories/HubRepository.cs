namespace FandomHub.Infrastructure.Repositories
{
	public class HubRepository : BaseRepo<Hub, int>, IHubRepository
	{ 
		public HubRepository(FandomHubDbContext context) : base(context)
		{ 
		}

	} 
}
