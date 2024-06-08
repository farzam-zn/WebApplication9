using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		T GetById(Expression<Func<T, bool>> filter);
		void Add(T entity);
		void Delete (T entity);
		void DeleteRange(IEnumerable<T> entity);
        T GetT(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

    }
}
