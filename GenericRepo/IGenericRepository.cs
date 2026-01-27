using BlazorWebAppMovies.Response;
using System.Linq.Expressions;

namespace BlazorWebAppMovies.GenericRepo
{
	public interface IGenericRepository <T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,string? includeProperty = null);
		Task<T?> GetByIdAsync(Expression<Func<T, bool>>? predicate = null, string? includeProperty = null,bool tracking = false);
		Task AddAsync(T Entity);
		Task UpdateAsync(T Entity);
		Task DeleteAsync(int id);

		IQueryable<T> Search(Expression<Func<T, bool>> predicate, string searchString);
		Task<PagedResult<T>> GetPagedAsync(
			int CurrentPage,
			int PageSize,
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			string[] includeProperty = null);

		Task<bool> IsAnyAsync(Expression<Func<T,bool>>? predicate);

		IQueryable<T> GetQueryable();

	}
}
