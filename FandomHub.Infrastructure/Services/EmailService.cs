using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace FandomHub.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IFluentEmail _fluentEmail;
		private readonly EmailConfiguration _emailConfig;
		private readonly ILogger<EmailService> _logger;

		public EmailService(IFluentEmail fluentEmail, EmailConfiguration emailConfig, ILogger<EmailService> logger)
		{
			_fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
			_emailConfig = emailConfig ?? throw new ArgumentNullException(nameof(emailConfig));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			if (string.IsNullOrEmpty(_emailConfig.From) ||
				string.IsNullOrEmpty(_emailConfig.SmtpServer) ||
				string.IsNullOrEmpty(_emailConfig.UserName) ||
				string.IsNullOrEmpty(_emailConfig.Password))
			{
				throw new ArgumentException("Email configuration is incomplete.");
			}
		}

		public async Task SendEmailAsync(Message message)
		{
			if (message == null || message.To == null || !message.To.Any())
			{
				throw new ArgumentException("Message or recipients cannot be null or empty.");
			}

			try
			{
				var email = _fluentEmail
					.To(message.To.Select(x => new FluentEmail.Core.Models.Address(x.Email, x.Name)))
					.Subject(message.Subject)
					.UsingTemplateFromFile($"Templates/{message.TemplateName}", message.TemplateData);

				var result = await email.SendAsync();
				if (result.Successful)
				{
					_logger.LogInformation("Email sent successfully to {Recipients}", string.Join(", ", message.To.Select(x => x.Email)));
				}
				else
				{
					_logger.LogError("Failed to send email to {Recipients}: {Error}", string.Join(", ", message.To.Select(x => x.Email)), string.Join(", ", result.ErrorMessages));
					throw new Exception($"Failed to send email: {string.Join(", ", result.ErrorMessages)}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to send email to {Recipients}", string.Join(", ", message.To.Select(x => x.Email)));
				throw;
			}
		}
	}
}
