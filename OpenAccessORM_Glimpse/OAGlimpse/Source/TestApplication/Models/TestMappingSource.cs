using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace TestApplication.Models
{
    public class TestMappingSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            IList<MappingConfiguration> result = new List<MappingConfiguration>();

            MappingConfiguration<Category> productConfiguration = new MappingConfiguration<Category>();
            productConfiguration.MapType(x => new
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                Description = x.Description
            }).ToTable("Categories");

            productConfiguration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            result.Add(productConfiguration);

            return result;
        }
    }
}