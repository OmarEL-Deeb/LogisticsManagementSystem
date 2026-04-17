using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
<<<<<<< HEAD
        Task<T?> GetAsync( Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
       

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
      // Task SaveChangesAsync();
=======
        // Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6


    }
}
