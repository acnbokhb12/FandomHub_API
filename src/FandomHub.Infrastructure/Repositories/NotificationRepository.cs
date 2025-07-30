namespace FandomHub.Infrastructure.Repositories
{
	public class NotificationRepository : BaseRepo<Notification, int>, INotificationRepository
	{
		public NotificationRepository(FandomHubDbContext context) : base(context)
		{
		}

		public async Task<List<Notification>> GetNotificationsByUserIdAsync(string userId)
		{
			return await _context.Notifications
				.Where(n => n.UserId == userId)
				.OrderByDescending(n => n.CreatedAt)
				.ToListAsync();
		}
	}
}
