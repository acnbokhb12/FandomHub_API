using System.Security.Cryptography;

namespace FandomHub.Infrastructure.Services
{
	public class RefreshTokenService : IRefreshTokenService
	{
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
		{
			_refreshTokenRepository = refreshTokenRepository;
		}

		public async Task<string> GenerateRefreshTokenAsync(string userId)
		{
			var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
			var token = new RefreshToken
			{
				UserId = userId,
				Token = refreshToken,
				CreatedAt = DateTime.UtcNow,
				ExpiresAt = DateTime.UtcNow.AddDays(7), 
				Revoked = false
			};

			await _refreshTokenRepository.CreateAsync(token); 
			return refreshToken;
		}

		public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
		{
			return await _refreshTokenRepository.GetRefreshTokenAsync(token);
		}
	}
}
