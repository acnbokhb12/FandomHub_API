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
	public class WikiPageRepository : BaseRepo<WikiPage, int>, IWikiPageRepository
	{
		public WikiPageRepository(FandomHubDbContext context) : base(context)
		{
			
		}
	}
}
