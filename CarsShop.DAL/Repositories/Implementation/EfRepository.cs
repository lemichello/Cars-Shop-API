using System;
using System.Linq;
using System.Linq.Expressions;
using CarsShop.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.DAL.Repositories.Implementation
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        public EfRepository(EfContext context)
        {
            _context = context;
            _dbSet   = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Edit(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

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

        private readonly EfContext _context;
        private readonly DbSet<T>  _dbSet;
    }
}