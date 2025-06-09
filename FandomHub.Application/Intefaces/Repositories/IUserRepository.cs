using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IUserRepository
	{
		Task<AuthResponse> FindByIdAsync(string userId);

	}
}
