using FandomHub.Application.Common;
using FandomHub.Application.Intefaces.Common;
using FandomHub.Application.Intefaces.Repositories;
using FandomHub.Application.Intefaces.Services;
using FandomHub.Application.Services;
using FandomHub.Infrastructure.Data;
using FandomHub.Infrastructure.Identity;
using FandomHub.Infrastructure.Repositories;
using FandomHub.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

// ===== Add DbContext =====
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

// Register repositories
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



// Register service
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

 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

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
