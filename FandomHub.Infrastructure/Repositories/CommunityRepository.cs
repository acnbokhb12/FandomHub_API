using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class CommunityRepository : BaseRepo<Community, int>,ICommunityRepository
	{
		private readonly FandomHubDbContext _context;
		public CommunityRepository(FandomHubDbContext context) : base(context) 
		{
			_context = context;
		}
	}
}
