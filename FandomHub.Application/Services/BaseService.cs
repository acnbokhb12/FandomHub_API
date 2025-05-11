using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class BaseService<T, Tkey> : IBaseService<T, Tkey> where T : class
	{
		private readonly IBaseRepo<T, Tkey> _repo;
        public BaseService(IBaseRepo<T, Tkey> baseRepo)
        {
            _repo = baseRepo;
        }
        public async Task<T?> CreateAsync(T entity)
		{
			return await _repo.CreateAsync(entity);
		}

		public async Task<bool> Delete(Tkey id)
		{
			return await _repo.Delete(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repo.GetAllAsync();
		}

		public async Task<T?> GetByIdAsync(Tkey id)
		{
			return await _repo.GetByIdAsync(id);
		}

		public async Task<bool> UpdateAsync(Tkey id, T entity)
		{
			return await (_repo.UpdateAsync(id, entity));
		}

		public async Task<T?> UpdateTAsync(Tkey id, T entity)
		{
			return await (_repo.UpdateTAsync(id, entity));
		}
	}
}
