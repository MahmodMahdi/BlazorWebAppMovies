using System.ComponentModel.DataAnnotations;

namespace BlazorWebAppMovies.Dtos.Movie
{
	public class MovieUpdateDto : MovieCreateDto
	{
		[Required]
		public int Id { get; set; }
	}
}
