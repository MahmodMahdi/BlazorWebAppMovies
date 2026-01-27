using BlazorWebAppMovies.GenericRepo;
using BlazorWebAppMovies.Models;

namespace BlazorWebAppMovies.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<Movie> MovieRepository { get; }
		IGenericRepository<Genre> GenreRepository { get; }
		Task<int> CompleteAsync();
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();

	}
}
