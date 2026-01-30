using BlazorWebAppMovies.Dtos.AuthDto;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Response;
using BlazorWebAppMovies.Services.AuthService;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppMovies.Services.AuthenticationService
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AuthService(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}
		public async Task<Result> RegisterAsync(RegisterDto registerDto)
		{
			var userExist = await _userManager.FindByEmailAsync(registerDto.Email);
			if (userExist != null)
				 return Result.Failure("User is already exist");
			var user = new ApplicationUser
			{ 
				Name = registerDto.Name,
				UserName = registerDto.Email,
				Email = registerDto.Email
			};
			var result =await _userManager.CreateAsync(user, registerDto.Password);
			if (result.Succeeded)
			{
				if (!await _roleManager.RoleExistsAsync("User"))
					await _roleManager.CreateAsync(new IdentityRole("User"));
				await _userManager.AddToRoleAsync(user, "User");
				return Result.Success("Data created successfully");

			}
			return Result.Failure(string.Join(",", result.Errors.Select(x => x.Description)));
		}
		public async Task<Result> LoginAsync(LoginDto loginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false,lockoutOnFailure: true);
			if (result.Succeeded)
				return Result.Success("Login successful");

			if (result.IsLockedOut)
				return Result.Failure("Account locked. Please try again later.");

			return Result.Failure("Invalid login attempt");
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		
	}
}
