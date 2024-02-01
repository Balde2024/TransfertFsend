using System;
using ForSendKH.Models;

namespace ForSendKH.Contracts
{
	public interface IGenericRepository<T> where T : class
	{
		public Task<T> GetAsync(int? id);
		public Task<List<T>> GetAllAsync();

		public Task<PageResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);

		public Task<T> AddAsync(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(int id);
		public Task<bool> ExistAsync(int id);
	}
}

