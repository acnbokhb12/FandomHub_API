using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Infrastructure.Data;
using FandomHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class CommunityCategoryRepository : ICommunityCategoryRepository
	{
		private readonly FandomHubDbContext _context;
		public CommunityCategoryRepository(FandomHubDbContext dbContext)
		{
			_context = dbContext;
		} 

		public async Task CreateAsync(CommunityCategory entity)
		{
			_context.CommunityCategories.Add(entity);
			await _context.SaveChangesAsync(); 
		} 

		public async Task CreateRangeAsync(List<CommunityCategory> entities)
		{
			_context.CommunityCategories.AddRange(entities);
			await _context.SaveChangesAsync();
		}
	}
}
