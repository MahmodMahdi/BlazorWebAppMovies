using BlazorWebAppMovies.Dtos.Movie;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Response;
using BlazorWebAppMovies.UnitOfWork;
using System.Linq.Expressions;

namespace BlazorWebAppMovies.Services.MovieService
{
	public class MovieService : IMovieService
	{
		private readonly IUnitOfWork _unitOfWork;
		public MovieService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Result<IEnumerable<MovieReadDto>>> GetAllAsync()
		{
			try
			{
				var movies = await _unitOfWork.MovieRepository.GetAllAsync(includeProperty: "Genre");
				var dto = movies.Select(m => new MovieReadDto
				{
					Id = m.Id,
					Title = m.Title,
					ReleaseDate = m.ReleaseDate,
					Price = m.Price,
					GenreName = m.Genre?.Name ?? "N/A"
				});
				return Result<IEnumerable<MovieReadDto>>.Success(dto);
			}
			catch {
				return Result<IEnumerable<MovieReadDto>>.Failure("Failed to retreive movies");
			}
		}

		public async Task<Result<MovieReadDto>> GetByIdAsync(int id)
		{
			try
			{
				if (id <= 0) return Result<MovieReadDto>.Failure("Invalid id");

				var movie = await _unitOfWork.MovieRepository.GetByIdAsync(x => x.Id == id, includeProperty: "Genre");

				if (movie is null) return Result<MovieReadDto>.Failure("Movie is not exist");
				var dto = new MovieReadDto
				{
					Id = movie.Id,
					Title = movie.Title,
					ReleaseDate = movie.ReleaseDate,
					Price = movie.Price,
					GenreName = movie.Genre?.Name ?? "N/A"
				};
				return Result<MovieReadDto>.Success(dto);
			}
			catch
			{
				return Result<MovieReadDto>.Failure("Failed to retreive movie");
			}
		}
		public async Task<Result> AddAsync(MovieCreateDto movieDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(movieDto.Title)) return  Result.Failure("Movie Title is Required");

				if (movieDto.GenreId <= 0) 
					return Result.Failure("GenreId is required");

				var isExist = await _unitOfWork.MovieRepository.IsAnyAsync(m => m.Title.ToLower()==movieDto.Title.ToLower());
				if (isExist)
					return Result.Failure("Title is already exist");

				var movie = new Movie
				{
					Title = movieDto.Title.Trim(),
					ReleaseDate = movieDto.ReleaseDate,
					Price = movieDto.Price,
					GenreId = movieDto.GenreId
				};
				await _unitOfWork.MovieRepository.AddAsync(movie);
				var saved = await _unitOfWork.CompleteAsync();

				return Result.Success("Movie created successfully");
			}
			catch
			{
				return Result.Failure("An error occurred while creating the movie");
			}
		}
		public async Task<Result> UpdateAsync(MovieUpdateDto movieDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(movieDto.Title)) return Result.Failure("Movie Title is Required");

				var existingMovie = await _unitOfWork.MovieRepository.GetByIdAsync(x => x.Id == movieDto.Id, tracking: true);
				if (existingMovie is null)
					return Result.Failure("Movie is not found");

				var isExist = await _unitOfWork.MovieRepository.IsAnyAsync(m => m.Title.ToLower() == movieDto.Title.ToLower() && m.Id != movieDto.Id);
				if (isExist)
					return Result.Failure("Title is already exist");

				existingMovie.Id = movieDto.Id;
				existingMovie.Title = movieDto.Title;
				existingMovie.ReleaseDate = movieDto.ReleaseDate;
				existingMovie.Price = movieDto.Price;
				existingMovie.GenreId = movieDto.GenreId;

				await _unitOfWork.MovieRepository.UpdateAsync(existingMovie);
				var updated = await _unitOfWork.CompleteAsync();

				return Result.Success("Movie updated successfully");
			}
			catch
			{
				return Result.Failure("An error occurred while updating the movie");
			}
		}


		public async Task<Result> DeleteAsync(int id)
		{
			try
			{
				var movie = await _unitOfWork.MovieRepository.GetByIdAsync(x => x.Id == id);
				if (movie is null)
					return Result.Failure("Movie not found");

				await _unitOfWork.MovieRepository.DeleteAsync(id);
				var deleted = await _unitOfWork.CompleteAsync();

				if (deleted > 0)
					return Result.Success("Movie deleted successfully");

				return Result.Failure("Failed to delete this movie");
			}
			catch
			{
				return Result.Failure("An error occurred while deleting the movie");

			}
		}

		public async Task<Result<PagedResult<MovieReadDto>>> GetFilteredMoviesAsync(int CurrentPage, int pageSize, string? SearchString = null)
		{
			try
			{
				if (CurrentPage < 1)
					return  Result<PagedResult<MovieReadDto>>.Failure("Current Page must be greater than 0");

				if (pageSize < 1 || pageSize > 100)
					return Result<PagedResult<MovieReadDto>>.Failure("Page size must be between 1 and 100");

				Expression<Func<Movie,bool>>? filter = null;
				if (!string.IsNullOrWhiteSpace(SearchString))
					filter = s => s.Title.ToLower().Contains(SearchString.ToLower());

				var pagedMovie = await _unitOfWork.MovieRepository.GetPagedAsync(CurrentPage, pageSize, filter, includeProperty: new[] {"Genre"});

				var dtos = pagedMovie.Items.Select(m => new MovieReadDto
				{
					Id = m.Id,
					Title = m.Title,
					Price = m.Price,
					ReleaseDate = m.ReleaseDate,
					GenreName = m.Genre?.Name ?? "N/A"
				}).ToList();
				var pagedResult = new PagedResult<MovieReadDto>(dtos,pagedMovie.TotalCount,pagedMovie.PageNumber,pagedMovie.PageSize);
					return Result<PagedResult<MovieReadDto>>.Success(pagedResult,"Movies retrieved successully");
			}
			catch
			{
				return Result<PagedResult<MovieReadDto>>.Failure("Failed to retrieve movie");
			}
		}

	}
}
