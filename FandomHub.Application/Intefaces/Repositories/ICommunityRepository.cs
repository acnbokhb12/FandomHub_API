using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

using FandomHub.Application.Common;

namespace FandomHub.Application.Intefaces.Repositories
{
    public interface ICommunityRepository : IBaseRepo<Community, int>
    {
        Task<List<Community>> GetAllActive();

        Task<Community?> GetByIdActive(int id);

		Task<PaginatedList<Community>> GetAllActivePagedAsync(int page, int pageSize);
	}
}
