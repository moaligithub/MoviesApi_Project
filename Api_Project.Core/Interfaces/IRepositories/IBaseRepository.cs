using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> order, string By);
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> include);
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> include);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include);

        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By);
        Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> include);
        Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> order, string By, Expression<Func<T, object>> include);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        void UpDate(T entity);
        void Delete(T entity);
    }
}
