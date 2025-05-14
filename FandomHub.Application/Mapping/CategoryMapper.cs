using AutoMapper;
using FandomHub.Application.DTOs.Response;
using FandomHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Application.Mapping
{
	public class CategoryMapper : Profile
	{
		public CategoryMapper() 
		{
			CreateMap<Category, CategoryResponse>();
		}
	}
}
