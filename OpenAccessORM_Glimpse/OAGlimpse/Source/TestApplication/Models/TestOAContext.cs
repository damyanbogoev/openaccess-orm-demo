using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace TestApplication.Models
{
    public class TestOAContext : OpenAccessContext
    {
        private readonly static MetadataContainer metadataContainer = new TestMappingSource().GetModel();
        private readonly static BackendConfiguration backendConfiguration = new BackendConfiguration { Backend = "mssql" };

        private const string DbConnection = @"data source=.\sqlexpress;initial catalog=Northwind;integrated security=True";

        public TestOAContext()
            : base(TestOAContext.DbConnection, TestOAContext.backendConfiguration, TestOAContext.metadataContainer)
        {

        }

        protected override void OnDatabaseOpen(BackendConfiguration backendConfiguration, MetadataContainer metadataContainer)
        {
            backendConfiguration.Logging.LogEvents = LoggingLevel.All;
            base.OnDatabaseOpen(backendConfiguration, metadataContainer);
        }

        public IQueryable<Category> Categories
        {
            get
            {
                return this.GetAll<Category>();
            }
        }
    }
}