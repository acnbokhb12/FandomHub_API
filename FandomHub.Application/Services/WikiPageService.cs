using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class WikiPageService : BaseService<WikiPage, int>, IWikiPageService
	{
		private readonly IWikiPageRepository _wikiPageRepository;
		public WikiPageService(IWikiPageRepository wikiPageRepository) : base(wikiPageRepository)
		{
			_wikiPageRepository = wikiPageRepository;
		}
	}
}
