using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.DTOs.Response
{
	public class EmailSendResponse
	{
		public bool Successful { get; }
		public string Message { get; }

		public EmailSendResponse(bool successful, string message)
		{
			Successful = successful;
			Message = message;
		}
	}
}
