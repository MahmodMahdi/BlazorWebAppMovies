using BlazorWebAppMovies.Dtos.AuthDto;
using BlazorWebAppMovies.Response;

namespace BlazorWebAppMovies.Services.AuthService
{
	public interface IAuthService
	{
		Task<Result> RegisterAsync (RegisterDto registerDto);
		Task<Result> LoginAsync (LoginDto loginDto);
		Task LogoutAsync();
	}
}
