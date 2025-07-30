using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		//public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
		//{
		//	throw new NotImplementedException();
		//}

		public Task<AuthResponse> FindByIdAsync(string userId)
		{
			return _userRepository.FindByIdAsync(userId);
		}

		//public Task<ApplicationUser> FindByNameAsync(string username)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
