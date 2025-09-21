using Microsoft.EntityFrameworkCore;
using SmartTeach.App.Interfaces;
using SmartTeach.Persistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Reposatory
{
    public class GenericReposatory<T> : IGenericReposatory<T> where T : class
    {
        private readonly SmartTeachDbcontext _context;
        private readonly DbSet<T> _dbSet;
        public GenericReposatory(SmartTeachDbcontext context)
        {
            _context=context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public  void Delete(int id )
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
            {

                _dbSet.Remove(entity);
            }
            else {return ; }    

        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public async Task<IEnumerable<T>> GetAllAsync(
    Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query =_dbSet;
            if(filter != null)
            query = query.Where(filter);

            if (includes!=null)
            {
                foreach(var include in includes)
                {
                   query= query.Include(include);
                }

            }
            if (orderBy!=null) query=orderBy(query);

                    return await query.ToListAsync();
        }
        public  IQueryable<T> Query(
    Expression<Func<T, bool>> filter = null,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query;
        }

        public async Task<T> GetByIdAsync(
       object id,
       params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Assuming Id is the primary key
            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id));
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
    
}
