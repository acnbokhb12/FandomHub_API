using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IWikiPageRepository : IBaseRepo<WikiPage, int>
	{
		Task<WikiPage?> GetWikiPageByIdAsync(int id);
	}
}
