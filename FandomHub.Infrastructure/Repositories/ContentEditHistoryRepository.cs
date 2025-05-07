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
	public class ContentEditHistoryRepository : BaseRepo<ContentEditHistory, int>, IContentEditHistoryRepository
	{
		private readonly FandomHubDbContext _context;
        public ContentEditHistoryRepository(FandomHubDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
