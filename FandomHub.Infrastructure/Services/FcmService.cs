using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Services
{
	public class FcmService : IFcmService
	{
		public async Task<bool> SendNotificationAsync(string token, string title, string body)
		{
			var message = new FirebaseAdmin.Messaging.Message
			{
				Token = token,
				Notification = new FirebaseAdmin.Messaging.Notification
				{
					Title = title,
					Body = body
				}
			};

			var messaging = FirebaseAdmin.Messaging.FirebaseMessaging.DefaultInstance;

			var response = await messaging.SendAsync(message);

			// response là messageId string — gửi thành công thì không throw exception
			return !string.IsNullOrEmpty(response);
		}
	}
}
