using Demo1.Application.Interfaces.IRepository;
using Demo1.Application.Interfaces.IUnitOfWork;
using Demo1.Domain.Entity;
using Demo1.Infrastructure.Implementation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Infrastructure.Implementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IGenericRepository<Product> Products { get; }

        public IGenericRepository<Category> Categories { get; }
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            Products=new GenericRepository<Product> (context);
            Categories=new GenericRepository<Category>(context);
        }

        public async Task<int> CompleteAsync()
        {
         return   await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
