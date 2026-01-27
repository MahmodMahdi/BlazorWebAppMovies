using BlazorWebAppMovies.Data;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorWebAppMovies.GenericRepo
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly BlazorWebAppMoviesContext _context;
		public GenericRepository(BlazorWebAppMoviesContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,string? includeProperty = null)
		{
			//using var dbContext = _context.CreateDbContext();
			IQueryable<T> query = _context.Set<T>();
			if(predicate  != null)
			{
				query = query.Where(predicate);
			}
			if (includeProperty != null)
			{
				foreach(var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				query = query.Include(item.Trim());
			}
				return await query.AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(Expression<Func<T, bool>>? predicate,string? includeProperty = null,bool tracking = false)
		{
			//using var dbContext = _context.CreateDbContext();
			IQueryable<T> query = _context.Set<T>();

			if(predicate !=null)
				query = query.Where(predicate);

			if (includeProperty != null)
			{
				foreach (var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					query = query.Include(item.Trim());
			}

			if (!tracking)
				query = query.AsNoTracking();

			return await query.FirstOrDefaultAsync();
		}

		public async Task AddAsync(T entity)
		{
			//using var dbContext = _context.CreateDbContext();
		    await _context.Set<T>().AddAsync(entity);

		}
		public async Task UpdateAsync(T entity)
		{
			//using var dbContext = _context.CreateDbContext();
			_context.Set<T>().Update(entity);
		}
		public async Task DeleteAsync(int id)
		{
			//using var dbContext = _context.CreateDbContext();
			var entity = await _context.Set<T>().FindAsync(id);
			if (entity != null)
			{
				_context.Set<T>().Remove(entity);
			}
			else
				throw new ArgumentException($"Entity with id {id} not found.");
		}
		public async Task<PagedResult<T>> GetPagedAsync(
			int pageNumber,
			int pageSize,
			Expression<Func<T,bool>>? filter = null,
			Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null,
			string [] includeProperty = null)
		{
			IQueryable<T> query = _context.Set<T>().AsNoTracking();

			if (filter is not null)
				query = query.Where(filter);

			if (includeProperty is not null && includeProperty.Length > 0)
			{
				query = includeProperty.Aggregate(query, (x, includeProperty) => x.Include(includeProperty));
			}

			//foreach (var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			//	query = query.Include(item.Trim());


			if (orderBy is not null)
				query = orderBy(query);

			return await PagedResult<T>.GetPaginated(query, pageNumber, pageSize);
		}
		public IQueryable<T> Search(Expression<Func<T,bool>> predicate,string searchString)
		{
			var query = _context.Set<T>().AsNoTracking();
			if (!string.IsNullOrWhiteSpace(searchString))
			{
				query = query.Where(predicate);
			}
			return query;
		}
		public IQueryable<T> GetQueryable()
		{
			return _context.Set<T>().AsQueryable();
		}

		public async Task<bool> IsAnyAsync(Expression<Func<T, bool>>? predicate)
		{
			return  await _context.Set<T>().AnyAsync(predicate);
		}
	}
}
