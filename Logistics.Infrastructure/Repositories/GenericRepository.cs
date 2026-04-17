using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

          
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
<<<<<<< HEAD

        public async Task<T?> GetAsync(  Expression<Func<T, bool>> predicate,
       params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }


=======
        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //   return await _dbSet.ToListAsync();
        //}
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

          
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
<<<<<<< HEAD
        
        public async Task<IEnumerable<T>> FindAsync(
       Expression<Func<T, bool>> predicate,
       params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query
                .Where(predicate)
                .ToListAsync();
        }


=======
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public async  Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public   void Update(T entity)
        {
             _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

     
<<<<<<< HEAD
        //public  async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
            
        //}
=======
        public  async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            
        }
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6

        
    }
}
