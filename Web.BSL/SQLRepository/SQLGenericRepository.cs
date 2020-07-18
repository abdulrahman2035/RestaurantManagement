
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.BSL.Interface;
using Web.DAL;

namespace Web.BSL.SQLRepository
{
    public class SQLGenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly ApplicationDbContext Context;

        public SQLGenericRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }
        //public Task<T> GetById(int id) => Context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task Add(T entity)
        {
            // await Context.AddAsync(entity);
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

       

        public async Task<IQueryable<T>> FindAll()
        {
            var lists = await Context.Set<T>().ToListAsync();
            var listss = Context.Set<T>();

            var list=await Context.Set<T>().AsNoTracking().ToListAsync();
            return listss;
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var list= await Context.Set<T>()
                .Where(expression).AsNoTracking().ToListAsync();
            return list.AsQueryable();

        }
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }
       

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().CountAsync(predicate);

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(this.disposed)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

       
    }
}
