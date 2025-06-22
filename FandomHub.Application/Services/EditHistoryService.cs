using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Services
{
	public class EditHistoryService : BaseService<EditHistory, int>, IEditHistoryService
	{
		private readonly IEditHistoryRepository _editHistoryRepo;
		public EditHistoryService(
			IEditHistoryRepository editHistoryRepository) : base(editHistoryRepository)
		{
			_editHistoryRepo = editHistoryRepository;
		}
	}
	 
}
