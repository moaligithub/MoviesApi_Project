using Api_Project.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Ef.Repositores
{
    public class LinqServices<T> where T : class
    {
        public IQueryable<T> Find(Expression<Func<T, bool>> match, IQueryable<T> Query)
        {
            return Query.Where(match);
        }

        public IQueryable<T> Include(Expression<Func<T, object>> include, IQueryable<T> Query)
        {
            return Query.Include(include);
        }

        public IQueryable<T> Order(Expression<Func<T, object>> order, string By, IQueryable<T> Query)
        {
            if (By == "OrderByDescending")
                Query = Query.OrderByDescending(order);
            else
                Query = Query.OrderBy(order);

            return Query;
        }
        public IQueryable<T> Skip(int Skip, IQueryable<T> Query)
        {
            return Query.Skip(Skip);
        }

        public IQueryable<T> Take(int Take, IQueryable<T> Query)
        {
            return Query.Take(Take);
        }
    }
}
