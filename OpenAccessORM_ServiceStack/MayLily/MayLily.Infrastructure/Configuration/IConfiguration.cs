using Telerik.OpenAccess;

namespace MayLily.Infrastructure.Configuration
{
    public interface IConfiguration
    {
        string ConnectionString { get; }
        BackendConfiguration BackendConfiguration { get; }
    }
}
