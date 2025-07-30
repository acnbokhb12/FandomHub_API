using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(Message message);

		Task<EmailSendResponse> SendWelcomeEmailAsync(string recipientEmail, string recipientName, string userName, string verificationLink);
	}
}
