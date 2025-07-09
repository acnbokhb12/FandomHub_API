using FandomHub.Application.Intefaces.Common;
using FandomHub.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class FcmTokenRepository : BaseRepo<FcmToken, int>, IFcmTokenRepository
	{
		private readonly ILogger<FcmTokenRepository> _logger;

		public FcmTokenRepository(FandomHubDbContext context, ILogger<FcmTokenRepository> logger) : base(context)
		{
			_logger = logger;
		}

		public async Task<List<FcmToken>> GetActiveTokensByUserIdAsync(string userId)
		{
			return await _context.FcmTokens.Where(fcm => fcm.UserId == userId && fcm.IsActive)
				.OrderByDescending(fcm => fcm.LastLogin)
				.ToListAsync();
		}

		public async Task<FcmToken?> GetTokenByUniqueIdAsync(string uniqueId)
		{
			return await _context.FcmTokens
				.FirstOrDefaultAsync(fcm => fcm.UniqueId == uniqueId);
		}

		public async Task SaveTokenAsync(FcmTokenRequest request, string userId)
		{
			var existingDevice = await _context.FcmTokens
				.FirstOrDefaultAsync(fcm => fcm.DeviceId == request.DeviceId);
			if(existingDevice != null)
			{
				if(existingDevice.Token != request.Token)
				{
					_logger.LogInformation($"Token refreshed for device {request.DeviceId}");
					existingDevice.Token = request.Token;
					existingDevice.UpdatedAt = DateTime.UtcNow.TrimToSecond();
					_context.FcmTokens.Update(existingDevice); 
				}
			}
		}
	}
}
