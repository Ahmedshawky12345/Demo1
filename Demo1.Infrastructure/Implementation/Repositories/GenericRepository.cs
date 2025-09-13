using Demo1.Application.Interfaces.IRepository;
using Demo1.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Infrastructure.Implementation.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> DbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();


        }

        public async Task AddAsync(T Entity)
        {
            await DbSet.AddAsync(Entity);
        }

        public void Delete(T Entity)
        {
            DbSet.Remove(Entity);
        }

        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public  IQueryable<T> GetAllAsync()
        {
            return  DbSet.Where(x=>!x.IsDeleted).AsQueryable();
        }

        public async Task<T> GetById(int Id) 
        {
            return await DbSet.FirstOrDefaultAsync(x=>x.Id==Id && !x.IsDeleted);
        }

        public void Update(T Entity)
        {
            DbSet.Update(Entity);
        }
    }
}
