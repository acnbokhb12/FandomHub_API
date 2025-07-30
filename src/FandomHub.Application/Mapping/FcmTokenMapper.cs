using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Mapping
{
	public class FcmTokenMapper: Profile
	{
		public FcmTokenMapper()
		{ 
			CreateMap<FcmTokenRequest, FcmToken>();
		}
	} 
}
