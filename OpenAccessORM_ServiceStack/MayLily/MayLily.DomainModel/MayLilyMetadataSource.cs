using System.Collections.Generic;
using MayLily.Data.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace MayLily.DomainModel
{
    public class MayLilyMetadataSource : FluentMetadataSource, IMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            IList<MappingConfiguration> result = new List<MappingConfiguration>();
            result.Add(MayLilyMetadataSource.GetCategoryMapping());

            return result;
        }

        private static MappingConfiguration<Category> GetCategoryMapping()
        {
            MappingConfiguration<Category> result = new MappingConfiguration<Category>();
            result.MapType(x => new
            {
                CategoryID = x.Id,
                CategoryName = x.Name,
                Description = x.Description
            }).
            ToTable("Categories");

            result.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            result.HasProperty(x => x.Name).WithFixedLength(50);
            result.HasProperty(x => x.Description).WithInfiniteLength();
            result.HasIndex(x => x.Name).WithName("IX_Category").IsUnique();

            return result;
        }
    }
}
