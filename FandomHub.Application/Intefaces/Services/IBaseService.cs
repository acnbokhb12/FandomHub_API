using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Services
{
	public interface IBaseService<T, Tkey> where T : class
	{
		Task<T?> CreateAsync(T entity);
		Task<T?> UpdateTAsync(Tkey id, T entity);
		Task<bool> UpdateAsync(Tkey id, T entity);
		Task<bool> Delete(Tkey id);
		Task<T?> GetByIdAsync(Tkey id);
		Task<IEnumerable<T>> GetAllAsync();
	}
}
