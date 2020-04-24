using Common.Data.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Data.Ef
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private bool _disposed;

        public EfRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Expression Expression => ((IQueryable<T>)_dbSet).Expression;

        public Type ElementType => ((IQueryable<T>)_dbSet).ElementType;

        public IQueryProvider Provider => ((IQueryable<T>)_dbSet).Provider;

        public T GetById(int id)
        {
            return _dbSet.Find(id) ?? throw new NullReferenceException(nameof(T));
        }

        public T Create(T entity)
        {
            if (entity == null) throw new ArgumentException(nameof(entity));

            _dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            if (!_dbSet.Any(e => e.Id == entity.Id)) { throw new NullReferenceException($"{typeof(T)} has been deleted"); }


            _context.Update(entity);

            return entity;
        }

        public void Delete(int entityId)
        {
            var entry = _dbSet.FirstOrDefault(e => e.Id == entityId);
            if (entry == null) { throw new NullReferenceException($"{typeof(T)} has been deleted"); }
            _dbSet.Remove(entry);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IQueryable<T>)_dbSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing) _context?.Dispose();
        }

        #endregion
    }
}
