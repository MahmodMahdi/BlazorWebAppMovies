using BlazorWebAppMovies.Data;
using BlazorWebAppMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppMovies
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using var context = new BlazorWebAppMoviesContext(
				serviceProvider.GetRequiredService<
					DbContextOptions<BlazorWebAppMoviesContext>>());

			if (!context.Genre.Any())
			{
				context.Genre.AddRange(
					new Genre { Name = "Action",Description="Amazing Category" },
					new Genre { Name = "Adventure",Description= "Fantastic Category" },
					new Genre { Name = "Comedy", Description="Nice Category" },
					new Genre { Name = "Drama",Description="Good Category" },
					new Genre { Name = "Horror" ,Description="Gergious Category"},
					new Genre { Name = "Romance",Description="Bad Category" },
					new Genre { Name = "Sci-Fi",Description="Nice Category" }
				);
				context.SaveChanges();
			}

			if (context == null || context.Movie == null)
			{
				throw new NullReferenceException(
					"Null BlazorWebAppMoviesContext or Movie DbSet");
			}

			if (context.Movie.Any())
			{
				return;
			}

			context.Movie.AddRange(
				new Movie
				{
					Title = "Mad Max",
					ReleaseDate = new DateOnly(1979, 4, 12),
					GenreId = context.Genre.First(g => g.Name == "Action").Id,
					Price = 2.51M,
				},
				new Movie
				{
					Title = "The Road Warrior",
					ReleaseDate = new DateOnly(1981, 12, 24),
					GenreId = context.Genre.First(g=>g.Name == "Action").Id,
					Price = 2.78M,
				},
				new Movie
				{
					Title = "Mad Max: Beyond Thunderdome",
					ReleaseDate = new DateOnly(1985, 7, 10),
					GenreId = context.Genre.First(g => g.Name == "Adventure").Id,
					Price = 3.55M,
				},
				new Movie
				{
					Title = "Mad Max: Fury Road",
					ReleaseDate = new DateOnly(2015, 5, 15),
					GenreId = context.Genre.First(g => g.Name == "Adventure").Id,
					Price = 8.43M,
				},
				new Movie
				{
					Title = "Furiosa: A Mad Max Saga",
					ReleaseDate = new DateOnly(2024, 5, 24),
					GenreId = context.Genre.First(g => g.Name == "Adventure").Id,
					Price = 13.49M,
				});

			context.SaveChanges();
		}
	}
}
