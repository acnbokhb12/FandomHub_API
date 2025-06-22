namespace FandomHub.Infrastructure.Repositories
{
	public class EditHistoryRepository : BaseRepo<EditHistory, int>, IEditHistoryRepository
	{ 
		public EditHistoryRepository(FandomHubDbContext context) : base(context)
		{  
		}

	}
}
