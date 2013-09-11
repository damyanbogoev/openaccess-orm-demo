using System;

namespace MayLily.Infrastructure.Data
{
    public interface IEntityContext : IDisposable
    {
        void RollbackChanges();

        void SubmitChanges();

        IEntitySet<TEntity> CreateEntitySet<TEntity>() where TEntity : class;
    }
}
