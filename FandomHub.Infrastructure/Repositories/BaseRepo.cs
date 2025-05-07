using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Repositories
{
	public class BaseRepo<T, Tkey> : IBaseRepo<T, Tkey> where T : class
	{
		private readonly FandomHubDbContext _context;
        public BaseRepo(FandomHubDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
		{
			_context.Set<T>().Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> Delete(Tkey id)
		{
			var entity = await _context.Set<T>().FindAsync(id);
			if (entity == null) return false;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<T?> GetByIdAsync(Tkey id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<bool> UpdateAsync(Tkey id, T entity)
		{
			var existing = await _context.Set<T>().FindAsync(id);
			if (existing == null) return false;

			_context.Entry(existing).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<T?> UpdateTAsync(Tkey id, T entity)
		{
			var existing = await _context.Set<T>().FindAsync(id);
			if (existing == null) return null;

			_context.Entry(existing).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();
			return existing;
		}
	}
}
