using Microsoft.EntityFrameworkCore;
using SimpleProductCatalog.Abstraction.Domain;
using SimpleProductCatalog.Abstraction.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Abstraction.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        public virtual async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            var entityEntry = Context.Entry(entity);
            DbSet.Update(entity);
        }
    }
}
