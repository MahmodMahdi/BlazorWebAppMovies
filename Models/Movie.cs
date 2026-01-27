using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateOnly ReleaseDate { get; set; }	
		public decimal Price { get; set; }
		[DisplayName("Genre")]
		public int GenreId { get; set; }
		[ForeignKey(nameof(GenreId))]
		public Genre Genre { get; set; } = default!;
	}
}
