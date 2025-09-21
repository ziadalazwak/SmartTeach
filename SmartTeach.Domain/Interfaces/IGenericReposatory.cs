using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Interfaces
{
    public interface IGenericReposatory<T>where T : class
    {
        public  Task<IEnumerable<T>> GetAllAsync(
    Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    params Expression<Func<T, object>>[] includes);
       public Task<T> GetByIdAsync(object id,
    params Expression<Func<T, object>>[] includes);
        public Task AddAsync(T entity);
      public  void Update(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public  void Delete(int id );
        public IQueryable<T> Query(
    Expression<Func<T, bool>> filter = null,
    params Expression<Func<T, object>>[] includes);
        public Task SaveChangesAsync();
      
    }
    
}
