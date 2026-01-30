using BlazorWebAppMovies.Dtos.Movie;
using BlazorWebAppMovies.Response;

namespace BlazorWebAppMovies.Services.MovieService
{
	public interface IMovieService
	{
		Task<Result<IEnumerable<MovieReadDto>>> GetAllAsync();
		Task<Result<MovieReadDto>> GetByIdAsync(int id);
		Task<Result> AddAsync(MovieCreateDto movie);
		Task<Result> UpdateAsync(MovieUpdateDto movie);
		Task<Result> DeleteAsync(int id);

		//Task<Result<(IEnumerable<Movie> movies, int totalCount)>> GetFilteredMoviesAsync(int CurrentPage,int pageSize,string? SearchString = null);
		Task<Result<PagedResult<MovieReadDto>>> GetFilteredMoviesAsync(int CurrentPage, int pageSize, string? SearchString = null);

	}
}

