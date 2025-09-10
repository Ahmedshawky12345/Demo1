using Demo1.Application.Interfaces.IRepository;
using Demo1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> CompleteAsync();
}
}
