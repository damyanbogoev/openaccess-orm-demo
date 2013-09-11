using System.Linq;
using Telerik.OpenAccess;

namespace MayLily.Data.OpenAccess
{
    public interface IInternalContext
    {
        IQueryable<TEntity> GetAll<TEntity>();

        void Add(object entity);

        void Delete(object entity);

        TEntity AttachCopy<TEntity>(TEntity entity);

        bool TryGetObjectByKey<TEntity>(ObjectKey key, out TEntity result);
    }
}
