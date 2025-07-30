using Microsoft.Extensions.Logging;   
using FluentEmail.Core; 
using System.Text.RegularExpressions;

namespace FandomHub.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IFluentEmail _fluentEmail;
		private readonly EmailConfiguration _emailConfig;
		private readonly ILogger<EmailService> _logger;
		private const string WelcomeTemplateName = "WelcomeEmail.cshtml";

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

		public async Task<EmailSendResponse> SendWelcomeEmailAsync(string recipientEmail, string recipientName, string userName, string verificationLink)
		{
			if (string.IsNullOrEmpty(recipientEmail) || !IsValidEmail(recipientEmail))
			{
				return new EmailSendResponse(false, "Invalid or empty recipient email address.");
			}

			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(verificationLink))
			{
				return new EmailSendResponse(false, "UserName or VerificationLink cannot be empty.");
			}

			try
			{
				var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", WelcomeTemplateName);
				if (!File.Exists(templatePath))
				{
					_logger.LogError("Template file not found: {Path}", templatePath);
					return new EmailSendResponse(false, $"Template file not found: {templatePath}");
				}
				var templateData = new WelcomeEmailRequest
				{
					UserName = userName,
					Name = recipientName,
				};

				var email = _fluentEmail
					.To(recipientEmail)
					.Subject("Welcome to FandomHub!")
					.UsingTemplateFromFile(templatePath, templateData, isHtml: true);

				var result = await email.SendAsync();
				if (result.Successful)
				{
					_logger.LogInformation("Welcome email sent successfully to {Recipient}", recipientEmail);
					return new EmailSendResponse(true, "Email sent successfully.");
				}
				else
				{
					var errorMessage = $"Failed to send welcome email to {recipientEmail}: {string.Join(", ", result.ErrorMessages)}";
					_logger.LogError(errorMessage);
					return new EmailSendResponse(false, errorMessage);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to send welcome email to {Recipient}", recipientEmail);
				return new EmailSendResponse(false, $"Failed to send email: {ex.Message}");
			}
		}

		private bool IsValidEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				return false;

			try
			{
				var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
				return regex.IsMatch(email);
			}
			catch
			{
				return false;
			}
		}
	}
	 
}
