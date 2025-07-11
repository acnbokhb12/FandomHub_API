using FandomHub.Api.Middlewares;
using FandomHub.Application.Common;
using FandomHub.Application.Intefaces.Common;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Application.Services;
using FandomHub.Infrastructure.Configurations;
using FandomHub.Infrastructure.Data;
using FandomHub.Infrastructure.Identity;
using FandomHub.Infrastructure.Repositories;
using FandomHub.Infrastructure.Services;
using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost5173",
		builder =>
		{
			builder.WithOrigins("http://localhost:5173")
				   .AllowAnyMethod()
				   .AllowAnyHeader()
				   .AllowCredentials();
		});
});


builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "FandomHub_api", Version = "v1" });

	// Configure Swagger for JWT Authentication
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
});

builder.Services.AddAuthentication(options =>
{

	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		var config = builder.Configuration;
		options.RequireHttpsMetadata = false;
		options.UseSecurityTokenValidators = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ClockSkew = TimeSpan.Zero,
			ValidateIssuerSigningKey = true,
			ValidateIssuer = false,
			ValidateAudience = false,
			RequireExpirationTime = true,
			ValidAudience = config["Jwt:Audience"],
			ValidIssuer = config["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
		};
		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				Console.WriteLine("Authentication Failed: " + context.Exception.Message);
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddAuthorization();

// Database
builder.Services.AddDbContext<FandomHubDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<IdentityApplicationUser>(options =>
{

})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<FandomHubDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

// Midlewares
builder.Services.AddSingleton<Stopwatch>();
builder.Services.AddScoped<PerformanceMiddleware>();
builder.Services.AddScoped<GlobalExceptionMiddleware>();

//FluentEmail Configuration
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


//Email Configuration
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

// Firebase Configuration
builder.Services.AddSingleton<FirebaseConfigurationService>();

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
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<ITokenService, TokenService>();
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
builder.Services.AddScoped<IFcmService, FcmService>();



// Mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

using (var scope = app.Services.CreateScope())
{
	var firebaseConfig = scope.ServiceProvider.GetRequiredService<FirebaseConfigurationService>();
	firebaseConfig.InitializeFirebase();
}

if (app.Environment.IsDevelopment())
{
	using var scope = app.Services.CreateScope();

	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var dbContext = scope.ServiceProvider.GetRequiredService<FandomHubDbContext>();

	await ApplicationRoleSeeder.SeedRolesAsync(roleManager);
	await ApplicationCategorySeeder.SeedAsync(dbContext);
	await ApplicationHubSeeder.SeedAsync(dbContext);
	await ApplicationHubCategorySeeder.SeedAsync(dbContext);
	await ApplicationLanguagesSeeder.SeedAsync(dbContext);

}
app.UseCors("AllowLocalhost5173");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
