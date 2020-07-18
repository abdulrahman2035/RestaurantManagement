
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Web.BSL.Interface
{
    public interface IGenericRepository<T> where T : class
    {


        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);

       /// Task<T> DeleteById(int id);

        Task<IEnumerable<T>> GetAllAsync();

        // Task<IQueryable<T>> GetAllIQueryableAsync();
        Task<IQueryable<T>> FindAll();

       

        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

       

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);


       

    }
}
