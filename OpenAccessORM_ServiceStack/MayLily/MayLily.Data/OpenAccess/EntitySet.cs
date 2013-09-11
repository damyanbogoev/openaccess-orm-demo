using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MayLily.Infrastructure.Data;
using Telerik.OpenAccess;

namespace MayLily.Data.OpenAccess
{
    public sealed class EntitySet<TEntity> : IEntitySet<TEntity>
       where TEntity : class
    {
        private readonly IInternalContext internalContext;

        private readonly IQueryable<TEntity> internalEntitySet;

        internal EntitySet(IInternalContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.internalContext = context;
            this.internalEntitySet = context.GetAll<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this.internalContext.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            this.internalContext.AttachCopy(entity);
        }

        public void Remove(TEntity entity)
        {
            this.internalContext.Delete(entity);
        }

        public TEntity Find(object keyValue)
        {
            ObjectKey key = new ObjectKey(typeof(TEntity).FullName, keyValue);

            TEntity result;
            this.internalContext.TryGetObjectByKey<TEntity>(key, out result);

            return result;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> path = null)
        {
            IQueryable<TEntity> query = this.internalEntitySet;
            if (path != null)
            {
                query = this.internalEntitySet.Include(path);
            }

            if (filter != null)
            {
                return query.SingleOrDefault(filter);
            }

            return query.SingleOrDefault();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> path = null)
        {
            IQueryable<TEntity> query = this.internalEntitySet;
            if (path != null)
            {
                query = this.internalEntitySet.Include(path);
            }

            if (filter != null)
            {
                return query.Where(filter);
            }

            return query;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.internalEntitySet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return this.internalEntitySet.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this.internalEntitySet.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.internalEntitySet.Provider;
            }
        }
    }
}
