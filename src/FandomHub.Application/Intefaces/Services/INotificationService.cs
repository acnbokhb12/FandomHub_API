using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface INotificationService : IBaseService<Notification, int>
	{
		Task<List<Notification>> GetNotificationsByUserIdAsync(string userId);

	}
}
