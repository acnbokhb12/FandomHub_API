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
