using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FandomHub.Application.Services;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddApplicationServices(this IHostApplicationBuilder builder)
	{ 
		// Mapping
		builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


		// Services
		builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
		builder.Services.AddScoped<ISlugHelper, SlugHelper>();
		builder.Services.AddScoped<ICommunityService, CommunityService>();
		builder.Services.AddScoped<IEditHistoryService, EditHistoryService>();
		builder.Services.AddScoped<IHubService, HubService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IHubCategoryService, HubCategoryService>();
		builder.Services.AddScoped<ICategoryService, CategoryService>();
		builder.Services.AddScoped<INotificationService, NotificationService>();
		builder.Services.AddScoped<IWikiPageService, WikiPageService>();
		builder.Services.AddScoped<IFcmTokenService, FcmTokenService>();
	}
}
