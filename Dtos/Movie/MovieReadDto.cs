using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Dtos.Movie
{
	public class MovieReadDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateOnly ReleaseDate { get; set; }
		public decimal Price { get; set; }
		public int GenreId { get; set; }
		[DisplayName("Genre")]
		public string GenreName { get; set; }

	}
}
