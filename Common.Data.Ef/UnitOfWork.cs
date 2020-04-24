using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Data;
using Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Data.Ef
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;

        protected readonly DbContext Context;

        public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
        {
            Context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public IQueryable<T> GetCollection<T>() where T : class
        {
            return Context.Set<T>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing) Context?.Dispose();
        }
    }
}
