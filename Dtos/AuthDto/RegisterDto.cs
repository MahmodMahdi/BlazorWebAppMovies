using System.ComponentModel.DataAnnotations;

namespace BlazorWebAppMovies.Dtos.AuthDto
{
	public class RegisterDto
	{
		[Required,MinLength(3),MaxLength(60)]
		public string Name { get; set; }
		[Required,EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
