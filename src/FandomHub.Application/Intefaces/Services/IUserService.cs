using FandomHub.Application.DTOs.Response; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IUserService
	{
		//Task<ApplicationUser> FindByNameAsync(string username);
		//Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
		Task<AuthResponse> FindByIdAsync(string userId);
	}
}
