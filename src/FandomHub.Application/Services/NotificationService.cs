using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class NotificationService : BaseService<Notification, int> , INotificationService
	{
		private readonly INotificationRepository _notificationRepo;
		public NotificationService(INotificationRepository notificationRepository) : base(notificationRepository)
		{
			_notificationRepo = notificationRepository;
		}

		public async Task<List<Notification>> GetNotificationsByUserIdAsync(string userId)
		{
			return await _notificationRepo.GetNotificationsByUserIdAsync(userId);
		}
	}
}
