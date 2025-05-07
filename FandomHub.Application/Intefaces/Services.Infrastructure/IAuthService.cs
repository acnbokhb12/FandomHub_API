using FandomHub.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services.Infrastructure
{
	public interface IAuthService
	{ 
		Task<string> RegisterAsync(RegisterRequest request);

		Task<string> LoginAsync(LoginRequest request);
	}
}
