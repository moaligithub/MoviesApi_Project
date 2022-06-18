using Api_Project.Core.Interfaces.IRepositories;
using Api_Project.Core.IUnitOfWork;
using Api_Project.Core.Models;
using Api_Project.Ef.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Ef.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Genre> Genres { get; private set; }

        public IBaseRepository<Movie> Movies { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Genres = new BaseRepository<Genre>(_context);
            Movies = new BaseRepository<Movie>(_context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
