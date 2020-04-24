using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Models;

namespace Common.Data.Data
{
    public interface IRepository<TEntity> : IQueryable<TEntity>, IDisposable
        where TEntity : BaseEntity
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity GetById(int id);
        void Delete(int entityId);
    }
}
