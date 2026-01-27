using BlazorWebAppMovies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppMovies.Data
{
	public class BlazorWebAppMoviesContext : IdentityDbContext<ApplicationUser>
	{
		public BlazorWebAppMoviesContext(DbContextOptions<BlazorWebAppMoviesContext> options) : base(options){}
		public BlazorWebAppMoviesContext(){}
		public DbSet<Movie> Movie { get; set; }
		public DbSet<Genre> Genre { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}


	}
}
