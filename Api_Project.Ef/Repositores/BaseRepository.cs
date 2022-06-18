using Api_Project.Core.Const;
using Api_Project.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Ef.Repositores
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public LinqServices<T> linq { get; private set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            linq = new LinqServices<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);

            return await query.ToListAsync();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Order(order, By , query);
           
            return await query.ToListAsync();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Include(include, query);

            return await query.ToListAsync();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Include(include, query);
            query = linq.Order(order, By, query);

            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Order(order, By, query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Include(include, query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Find(match, query);
            query = linq.Include(include, query);
            query = linq.Order(order, By, query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> order, string By)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Order(order, By, query);

            return await query.ToListAsync(); 
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query  = linq.Include(include, query);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = linq.Include(include, query);
            query = linq.Order(order, By, query);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void UpDate(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
