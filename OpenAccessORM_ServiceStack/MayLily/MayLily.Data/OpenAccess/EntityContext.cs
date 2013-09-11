using MayLily.Infrastructure.Configuration;
using MayLily.Infrastructure.Data;
using Telerik.OpenAccess;

namespace MayLily.Data.OpenAccess
{
    public class EntityContext : OpenAccessContext, IEntityContext, IInternalContext, IDbMigrator
    {
        public EntityContext(IConfiguration configuration, IMetadataSource source)
            : base(configuration.ConnectionString, configuration.BackendConfiguration, source.GetModel())
        {
        }

        public IEntitySet<TEntity> CreateEntitySet<TEntity>()
            where TEntity: class
        {
            return new EntitySet<TEntity>(this);
        }

        public void RollbackChanges()
        {
            this.ClearChanges();
        }

        public void SubmitChanges()
        {
            this.SaveChanges();
        }

        public void MigrateSchema()
        {
            ISchemaHandler handler = this.GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }

                if (throwException)
                {
                    throw;
                }
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }

        public virtual void SeedData()
        {
        }
    }
}
