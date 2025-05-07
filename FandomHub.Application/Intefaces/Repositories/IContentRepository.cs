using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IContentRepository : IBaseRepo<Content, int>
	{
		Task<bool> CheckSlugInContentWithContentType(SlugContentWithTypeRequest request);
	}
}
 