using System.Linq;
using MayLily.Data.OpenAccess;
using MayLily.Infrastructure.Configuration;
using MayLily.Infrastructure.Data;

namespace MayLily.DomainModel
{
    public interface IMayLilyContext : IEntityContext
    {
        IEntitySet<Category> Categories
        {
            get;
        }
    }

    public class MayLilyContext : EntityContext, IMayLilyContext
    {
        private IEntitySet<Category> categories;

        public MayLilyContext(IConfiguration configuration, IMetadataSource source)
            : base(configuration, source)
        {
        }

        public IEntitySet<Category> Categories
        {
            get
            {
                if (this.categories == null)
                {
                    this.categories = this.CreateEntitySet<Category>();
                }

                return this.categories;
            }
        }

        public override void SeedData()
        {
            if (this.Categories.Count() > 0)
            {
                return;
            }

            this.Categories.Add(new Category { Name = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" });
            this.Categories.Add(new Category { Name = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" });
            this.Categories.Add(new Category { Name = "Confections", Description = "Desserts, candies, and sweet breads" });
            this.SubmitChanges();
        }
    }
}
