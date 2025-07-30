using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Identity
{
	public static class ApplicationRoleSeeder
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			var roles = new List<IdentityRole>
		{
			new IdentityRole { Name = "User", NormalizedName = "USER" },
			new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" }
		};

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role.Name))
				{
					await roleManager.CreateAsync(role);
				}
			}
		}
	}
}
