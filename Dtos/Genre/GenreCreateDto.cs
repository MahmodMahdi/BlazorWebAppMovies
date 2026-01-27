using System.ComponentModel.DataAnnotations;

namespace BlazorWebAppMovies.Dtos.Genre
{
	public class GenreCreateDto
	{
		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string Name { get; set; }
		[StringLength(100, MinimumLength = 3)]
		public string Description { get; set; }
	}
}
