﻿using FandomHub.Application.DTOs.Request;
using FandomHub.Application.Intefaces.Services.Infrastructure;
using FandomHub.Infrastructure.Data;
using FandomHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly FandomHubDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITokenService _tokenService;
        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, FandomHubDbContext fandomHubDb)
        {
			_context = fandomHubDb;
            _userManager = userManager;
			_tokenService = tokenService;
        }

		public async Task<string> LoginAsync(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);
			if (user == null)
			{
				throw new Exception("Invalid UserName or Password.");
			}
			var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
			if (!isPasswordValid)
			{
				Console.WriteLine("Invalid password.");
				throw new Exception("Invalid UserName or password.");
			}
			var roles = await _userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault() ?? "User";
            Console.WriteLine("UserId v1: "+ user.Id);

			var token = _tokenService.GenerateToken(user.Id, user.UserName, role);
			return token;
		}

		public async Task<string> RegisterAsync(RegisterRequest request)
		{
			var existUserName = await _userManager.FindByNameAsync(request.UserName);
			if (existUserName != null)
			{ 
				throw new Exception("Username is already taken.");
			}
			var existEmail = await _userManager.FindByEmailAsync(request.Email);
			if (existEmail != null)
			{
				throw new Exception("Email is already taken.");
			}

			var user = new ApplicationUser
			{ 
				UserName = request.UserName,
				Email = request.Email,
				BirthDay = request.BirthDay
			};
			var result = await _userManager.CreateAsync(user,request.Password);
			if (!result.Succeeded)
			{  
				throw new Exception("Registration failed");
			}
			var roleResult = await _userManager.AddToRoleAsync(user, "User");
			if (!roleResult.Succeeded)
			{
				Console.WriteLine("Failed to assign role");

				throw new Exception("Failed to assign role.");
			} 
			var token = _tokenService.GenerateToken(user.Id, user.UserName, "User");
			return token;
		}
	}
}
