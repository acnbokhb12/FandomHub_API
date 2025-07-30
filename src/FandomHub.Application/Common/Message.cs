using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Common
{
	public class Message
	{
		public (string Email, string? Name)[] To { get; } // Hỗ trợ email và tên
		public string Subject { get; }
		public string TemplateName { get; }
		public object TemplateData { get; }

		public Message(IEnumerable<(string Email, string? Name)> to, string subject, string templateName, object templateData)
		{
			To = to?.ToArray() ?? throw new ArgumentNullException(nameof(to));
			if (!To.Any() || To.Any(x => string.IsNullOrEmpty(x.Email)))
			{
				throw new ArgumentException("Recipient list cannot be empty or contain invalid emails.");
			}
			Subject = subject ?? throw new ArgumentNullException(nameof(subject));
			TemplateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
			TemplateData = templateData ?? throw new ArgumentNullException(nameof(templateData));
		}
	}
}
