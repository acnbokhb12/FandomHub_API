using Microsoft.AspNetCore.Identity;

namespace FandomHub.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<IdentityApplicationUser> _identityUserManager;

		public UserRepository(UserManager<IdentityApplicationUser> userManager)
		{
			_identityUserManager = userManager;
		}

		public async Task<AuthResponse> FindByIdAsync(string userId)
		{
			var userIdentity = await _identityUserManager.FindByIdAsync(userId);
			var roles = await _identityUserManager.GetRolesAsync(userIdentity);
			var role = roles.FirstOrDefault() ?? "User";
			var user = new AuthResponse
			{
				UserId = userIdentity.Id,
				UserName = userIdentity.UserName,
				FullName = userIdentity.FullName,
				Role = role,
			};
			return user;

		}
	}
}
