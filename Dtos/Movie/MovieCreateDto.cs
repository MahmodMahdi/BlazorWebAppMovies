using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Dtos.Movie
{
	public class MovieCreateDto
	{
		[Required]
		[StringLength(60, MinimumLength = 3)]
		public string Title { get; set; }
		[Required]
		[DisplayName("Release Date")]
		public DateOnly ReleaseDate { get; set; }
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,2)")]
		[Required]
		[Range(1, 1000)]
		public decimal Price { get; set; }
		[Required]
		public int GenreId { get; set; }
	}
}
