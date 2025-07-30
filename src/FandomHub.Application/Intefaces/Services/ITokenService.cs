using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface ITokenService
	{
			string GenerateToken(string userId, string username, string role);
	}
}
