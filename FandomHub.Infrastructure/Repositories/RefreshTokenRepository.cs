using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
