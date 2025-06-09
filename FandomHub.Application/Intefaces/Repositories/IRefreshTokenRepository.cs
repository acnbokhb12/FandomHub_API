using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IRefreshTokenRepository : IBaseRepo<RefreshToken, int>
	{
		Task AddRefreshTokenAsync(RefreshToken refreshToken);
		Task<RefreshToken?> GetRefreshTokenAsync(string token);
	}
}
