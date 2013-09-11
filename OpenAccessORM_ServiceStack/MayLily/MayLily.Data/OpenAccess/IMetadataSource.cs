using Telerik.OpenAccess.Metadata;

namespace MayLily.Data.OpenAccess
{
    public interface IMetadataSource
    {
        MetadataContainer GetModel();
    }
}
