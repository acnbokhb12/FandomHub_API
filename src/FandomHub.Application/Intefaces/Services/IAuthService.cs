using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IAuthService
	{
		Task<(string Token, AuthResponse UserInfo)> RegisterAsync(RegisterRequest request);

		Task<(string Token, AuthResponse UserInfo)> LoginAsync(LoginRequest request);
	}
}
