using BlazorWebAppMovies.Dtos.Genre;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Response;
using System.Linq.Expressions;

namespace BlazorWebAppMovies.Services.GenreService
{
	public interface IGenreService
	{
		Task<Result<IEnumerable<GenreReadDto>>> GetAllAsync();
		Task<Result<GenreReadDto>> GetByIdAsync(int id);
		Task<Result> AddAsync(GenreCreateDto genreDto);
		Task<Result> UpdateAsync(GenreUpdateDto genreDto);
		Task<Result> DeleteAsync(int id);
		Task<Result<IEnumerable<GenreReadDto>>> Search(string SearchString);
	}
}
