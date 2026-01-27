using BlazorWebAppMovies.Data;
using BlazorWebAppMovies.GenericRepo;
using BlazorWebAppMovies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlazorWebAppMovies.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BlazorWebAppMoviesContext _dbcontext;
		private  IDbContextTransaction _transaction;
		public IGenericRepository<Movie> MovieRepository { get; }
		public IGenericRepository<Genre> GenreRepository { get; }

		public UnitOfWork(IDbContextFactory<BlazorWebAppMoviesContext> dbFactory)
		{
			_dbcontext = dbFactory.CreateDbContext();
            MovieRepository = new GenericRepository<Movie>(_dbcontext);
			GenreRepository = new GenericRepository<Genre>(_dbcontext);
		}


		public async Task<int> CompleteAsync() =>
			 await _dbcontext.SaveChangesAsync();

		public async Task BeginTransactionAsync() =>
			_transaction = await _dbcontext.Database.BeginTransactionAsync();
		public async Task CommitTransactionAsync()
		{
			if(_transaction  != null)
			await _transaction.CommitAsync();
		}
		public async Task RollbackTransactionAsync()
		{
			if (_transaction != null)
				await _dbcontext.Database.RollbackTransactionAsync();
		}
		public void Dispose()
		{
			_transaction?.Dispose();
			_dbcontext?.Dispose();
		}
	}
}
