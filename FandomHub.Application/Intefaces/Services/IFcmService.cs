using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IFcmService
	{
		Task<bool> SendNotificationAsync(string token, string title, string body);
	}
}
