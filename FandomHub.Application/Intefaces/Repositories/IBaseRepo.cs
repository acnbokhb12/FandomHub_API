using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface IBaseRepo<T, Tkey> where T : class
	{
		Task<T?> GetByIdAsync(Tkey id);
		Task<T> CreateAsync(T entity); 
		Task<bool> Delete(Tkey id);
		Task<bool> UpdateAsync(Tkey id, T entity);
		Task<T?> UpdateTAsync(Tkey id, T entity);
		Task<IEnumerable<T>> GetAllAsync();
		Task<bool> SaveChangeAsync();
	}
}
