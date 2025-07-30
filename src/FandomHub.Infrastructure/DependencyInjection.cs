using FluentEmail.MailKitSmtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
	public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
	{
		// Database
		builder.Services.AddDbContext<FandomHubDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		//FluentEmail Configuration
		builder.Services.Configure<EmailConfiguration>(
			builder.Configuration.GetSection("EmailConfiguration"));

		builder.Services.AddFluentEmail(builder.Configuration["EmailConfiguration:From"])
			.AddRazorRenderer()
			.AddMailKitSender(new SmtpClientOptions
			{
				Server = builder.Configuration["EmailConfiguration:SmtpServer"],
				Port = int.Parse(builder.Configuration["EmailConfiguration:Port"]),
				User = builder.Configuration["EmailConfiguration:UserName"],
				Password = builder.Configuration["EmailConfiguration:Password"],
				UseSsl = false,
				RequiresAuthentication = true,
				SocketOptions = builder.Configuration["EmailConfiguration:SecureSocketOptions"] switch
				{
					"StartTls" => MailKit.Security.SecureSocketOptions.StartTls,
					"SslOnConnect" => MailKit.Security.SecureSocketOptions.SslOnConnect,
					_ => MailKit.Security.SecureSocketOptions.Auto
				}
			}); 
		builder.Services.AddSingleton(sp =>
			sp.GetRequiredService<IOptions<EmailConfiguration>>().Value);

		// Firebase Configuration
		builder.Services.AddSingleton<FirebaseConfigurationService>();

		//Email Configuration
		builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

		// Repositories
		builder.Services.AddScoped(typeof(IBaseRepo<,>), typeof(BaseRepo<,>));
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
		builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
		builder.Services.AddScoped<IEditHistoryRepository, EditHistoryRepository>();
		builder.Services.AddScoped<IHubRepository, HubRepository>();
		builder.Services.AddScoped<IHubCategoryRepository, HubCategoryRepository>();
		builder.Services.AddScoped<ICommunityCategoryRepository, CommunityCategoryRepository>();
		builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
		builder.Services.AddScoped<IWikiPageRepository, WikiPageRepository>();
		builder.Services.AddScoped<IFcmTokenRepository, FcmTokenRepository>();

		// Services
		builder.Services.AddScoped<ITokenService, TokenService>();
		builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
		builder.Services.AddScoped<IFcmService, FcmService>();
		builder.Services.AddScoped<IEmailService, EmailService>();
		builder.Services.AddScoped<IAuthService, AuthService>();
	}
}