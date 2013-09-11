namespace MayLily.Data.OpenAccess
{
    public interface IDbMigrator
    {
        void MigrateSchema();

        void SeedData();
    }
}
