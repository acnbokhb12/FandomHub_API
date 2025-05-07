using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Intefaces.Repositories
{
	public interface ICategoryRepository
	{
		Task GetListCategoriesAsync();
	}
}
