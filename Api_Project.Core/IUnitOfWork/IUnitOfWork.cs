using Api_Project.Core.Interfaces.IRepositories;
using Api_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Genre> Genres { get; }
        IBaseRepository<Movie> Movies { get; }

        void Complete();
    }
}
