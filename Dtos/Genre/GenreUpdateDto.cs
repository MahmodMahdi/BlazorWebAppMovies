using System.ComponentModel.DataAnnotations;

namespace BlazorWebAppMovies.Dtos.Genre
{
	public class GenreUpdateDto : GenreCreateDto
	{
		[Required]
		public int Id { get; set; }
	}
}
