using BlazorWebAppMovies.Dtos.Genre;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Response;
using BlazorWebAppMovies.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorWebAppMovies.Services.GenreService
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;
		public GenreService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Result<IEnumerable<GenreReadDto>>> GetAllAsync()
		{
			try
			{
				var genres = await _unitOfWork.GenreRepository.GetAllAsync();
				var dtos = genres.Select(g => new GenreReadDto
				{
					Id= g.Id,
					Name = g.Name,
					Description = g.Description
				});
				return Result<IEnumerable<GenreReadDto>>.Success(dtos);
			}
			catch
			{
				return Result<IEnumerable<GenreReadDto>>.Failure("Failed to retreive genres");
			}
		}

		public async Task<Result<GenreReadDto>> GetByIdAsync(int id)
		{
			try
			{
				if (id <= 0) return Result<GenreReadDto>.Failure("Invalid id");

				var genre = await _unitOfWork.GenreRepository.GetByIdAsync(x => x.Id == id);

				if (genre is null) return Result<GenreReadDto>.Failure("Genre is not exist");

				var dto = new GenreReadDto
				{
					Id = genre.Id,
					Name = genre.Name,
					Description = genre.Description
				};

				return Result<GenreReadDto>.Success(dto);
			}
			catch
			{
				return Result<GenreReadDto>.Failure("Failed to retreive genre");
			}
		}
		public async Task<Result> AddAsync(GenreCreateDto genreDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(genreDto.Name)) return Result.Failure("Genre Name is Required");

				var existGenre = await _unitOfWork.GenreRepository.IsAnyAsync(g=>g.Name.ToLower()==genreDto.Name.ToLower());
				if (existGenre) return Result.Failure("Genre is already exist");

				var genre = new Genre
				{
					Name = genreDto.Name,
					Description = genreDto.Description,
				};

				await _unitOfWork.GenreRepository.AddAsync(genre);
				var saved = await _unitOfWork.CompleteAsync();

				return Result.Success("Genre created successfully");
			}
			catch
			{
				return Result.Failure("An error occurred while creating the genre");
			}
		}
		public async Task<Result> UpdateAsync(GenreUpdateDto genreDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(genreDto.Name)) return Result.Failure("Genre Name is Required");

				var existingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(x => x.Id == genreDto.Id);
				if (existingGenre is null)
					return Result.Failure("Genre is not found");

				var existGenre = await _unitOfWork.GenreRepository.IsAnyAsync(g => g.Name.ToLower() == genreDto.Name.ToLower()&& g.Id !=genreDto.Id);
				if (existGenre) return Result.Failure("Genre is already exist");

				var genre = new Genre
				{
					Id = genreDto.Id,
					Name = genreDto.Name,
					Description = genreDto.Description,
				};

				await _unitOfWork.GenreRepository.UpdateAsync(genre);
				var updated = await _unitOfWork.CompleteAsync();

				return Result.Success("Genre updated successfully");
			}
			catch
			{
				return Result.Failure("An error occurred while updating the genre");
			}
		}


		public async Task<Result> DeleteAsync(int id)
		{
			try
			{
				var genre = await _unitOfWork.GenreRepository.GetByIdAsync(x => x.Id == id);
				if (genre is null)
					return Result.Failure("Genre not found");

				await _unitOfWork.GenreRepository.DeleteAsync(id);
				var deleted = await _unitOfWork.CompleteAsync();

				return Result.Success("Genre deleted successfully");
			}
			catch
			{
				return Result.Failure("An error occurred while deleting the genre");

			}
		}
		public async Task<Result<IEnumerable<GenreReadDto>>> Search(string SearchString)
		{
			try
			{
				var query = _unitOfWork.GenreRepository.Search(x=>x.Name.ToLower().Contains(SearchString.ToLower()), SearchString);

				var dto =await query.Select(g => new GenreReadDto
				{
					Id = g.Id,
					Name = g.Name,
					Description = g.Description
				}).ToListAsync();

				return Result<IEnumerable<GenreReadDto>>.Success(dto);
			}
			catch
			{
				return Result<IEnumerable<GenreReadDto>>.Failure("An error occured");
			}
		}
	}
}
