namespace FandomHub.Infrastructure.Repositories
{
	public class RefreshTokenRepository : BaseRepo<RefreshToken, int>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(FandomHubDbContext context) : base(context)
		{
		}

		public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
		{
			_context.RefreshTokens.Add(refreshToken);
			await _context.SaveChangesAsync();
		}

		public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
		{
			 return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
		}

	}
}
