using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Application.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int Id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>>predicate);
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
