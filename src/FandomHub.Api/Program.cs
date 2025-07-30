using FandomHub.Api.Middlewares;
using FandomHub.Infrastructure.Data;
using FandomHub.Infrastructure.Identity; 
using FandomHub.Infrastructure.Services;  
using Microsoft.AspNetCore.Identity; 
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//DI Web Services API
builder.AddWebServicesAPI();

//DI Application Services
builder.AddApplicationServices();

//DI Infrastructure Services
builder.AddInfrastructureServices();


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
