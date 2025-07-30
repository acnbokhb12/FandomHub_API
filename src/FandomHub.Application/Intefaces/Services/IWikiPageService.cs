using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IWikiPageService : IBaseService<WikiPage, int>
	{
		Task<WikiPageResponse?> GetWikiPageByIdAsync(int id);
		Task<WikiPageResponse?> CreateWikiPage(WikiPageCreateRequest request, string userId);
	}
}
