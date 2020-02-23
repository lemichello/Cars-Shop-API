using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarsShop.DAL.Abstraction
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Remove(T entity);
        Task Edit(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}