﻿using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface INotificationRepository : IBaseRepo<Notification, int>
	{
		Task<List<Notification>> GetNotificationsByUserIdAsync(string userId);
	}
}
