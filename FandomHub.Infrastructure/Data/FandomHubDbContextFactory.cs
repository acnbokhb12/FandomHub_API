using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FandomHub.Infrastructure.Data
{
	class FandomHubDbContextFactory : IDesignTimeDbContextFactory<FandomHubDbContext>
	{
		public FandomHubDbContext CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory()) // Lấy từ folder hiện tại (nơi chạy EF)
				.AddJsonFile("appsettings.json")
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<FandomHubDbContext>();
			var connectionString = config.GetConnectionString("DefaultConnection");

			optionsBuilder.UseSqlServer(connectionString);

			return new FandomHubDbContext(optionsBuilder.Options);
		}
	}
}
