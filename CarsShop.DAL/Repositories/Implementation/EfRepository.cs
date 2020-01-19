using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarsShop.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.DAL.Repositories.Implementation
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
    }
}
