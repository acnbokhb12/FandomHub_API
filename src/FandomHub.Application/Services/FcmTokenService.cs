using AutoMapper;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class FcmTokenService : BaseService<FcmToken, int>, IFcmTokenService
	{
		private readonly IFcmTokenRepository _fcmTokenRepo;
		private readonly IMapper _mapper;


		public FcmTokenService(
			IFcmTokenRepository fcmTokenRepo,
			IMapper mapper
			) : base(fcmTokenRepo)
		{
			_fcmTokenRepo = fcmTokenRepo;
			_mapper = mapper;
		}

		public async Task<bool> SaveDeviceTokenAsync(FcmTokenRequest request, string userId)
		{
			var existingDevice = await _fcmTokenRepo.GetTokenByUniqueIdAsync(request.DeviceId);
			if(existingDevice != null)
			{
				if(existingDevice.Token != request.Token)
				{
					existingDevice.Token = request.Token; 
				}
				existingDevice.UserId = userId;
				existingDevice.IsActive = true;
				existingDevice.LastLogin = DateTime.Now.TrimToSecond();
				existingDevice.UpdatedAt = DateTime.Now.TrimToSecond();
				existingDevice.AppVersion = request.AppVersion;
				//existingDevice.DeviceName = request.DeviceName;
				//existingDevice.DeviceType = request.DeviceType;
				return await _fcmTokenRepo.SaveChangeAsync();
			}
			else
			{
				var newToken = _mapper.Map<FcmToken>(request);
				newToken.UserId = userId;
				newToken.LastLogin = DateTime.Now.TrimToSecond();
				newToken.CreatedAt = DateTime.Now.TrimToSecond();
				newToken.UpdatedAt = DateTime.Now.TrimToSecond();
				var result = await _fcmTokenRepo.CreateAsync(newToken);
				if (result != null)  return true;
				else return false;
			}
		}

		 
	}
}
