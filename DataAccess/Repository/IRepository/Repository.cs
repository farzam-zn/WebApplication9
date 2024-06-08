using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
	{


		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet =_db.Set<T>();
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);

		}
        public T GetT(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;

            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }
        public void Delete(T entity)
		{

			dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}

		public T GetById(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query= query.Where(filter);
			return query.FirstOrDefault();
		}

		public async Task<IEnumerable<T>> GetAll()
		{

           IQueryable<T> query1 = dbSet;	
			return  await query1.ToListAsync();
		}

    }
}
