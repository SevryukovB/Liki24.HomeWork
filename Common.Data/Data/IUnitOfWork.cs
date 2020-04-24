using System;
using System.Collections.Generic;
using System.Text;
using Common.Data.Models;

namespace Common.Data.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        void SaveChanges();
    }
}
