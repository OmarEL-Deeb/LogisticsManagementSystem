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
        Task<T?> GetAsync( Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
       

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
