using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MayLily.Infrastructure.Data
{
    public interface IEntitySet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IQueryable, IEnumerable
        where TEntity : class
    {
        void Add(TEntity entity);

        void Attach(TEntity entity);

        void Remove(TEntity entity);

        TEntity Find(object keyValue);

        TEntity Find(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> path = null);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> path = null);
    }
}
