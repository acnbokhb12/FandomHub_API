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
	public class EditHistoryRepository : BaseRepo<EditHistory, int>, IEditHistoryRepository
	{ 
		public EditHistoryRepository(FandomHubDbContext context) : base(context)
		{  
		}
	}
 
}
