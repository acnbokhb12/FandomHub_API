using FandomHub.Application.DTOs.Request;
using FandomHub.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IContentService
	{
		Task<ContentResponse> CreateContent(ContentCreateRequest request, string userId);

		Task<bool> CheckSlugInContentWithContentType(SlugContentWithTypeRequest request);
	}
}
