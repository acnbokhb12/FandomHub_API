using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IFcmTokenService : IBaseService<FcmToken, int>
	{
		Task<bool> SaveDeviceTokenAsync(FcmTokenRequest request, string userId); 
	}
}
