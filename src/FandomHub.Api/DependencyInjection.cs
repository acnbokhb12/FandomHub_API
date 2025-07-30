
using FandomHub.Api.Middlewares;
using FandomHub.Infrastructure.Data;
using FandomHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
	public static void AddWebServicesAPI(this IHostApplicationBuilder builder)
	{
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

		 
	}
}
