using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IFcmTokenRepository : IBaseRepo<FcmToken, int>
	{
		Task SaveTokenAsync(FcmTokenRequest request, string userId);

		Task<FcmToken?> GetTokenByUniqueIdAsync(string deviceId);

		Task<List<FcmToken>> GetActiveTokensByUserIdAsync(string userId);
	}
}
