using MayLily.DomainModel;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace MayLily.Services
{
    [Route("/get-category/{CategoryID}", Verbs = "GET")]
    public class GetCategory
    {
        public int CategoryID
        {
            get;
            set;
        }
    }

    public class GetCategoryResponse
    {
        public CategoryDto Result
        {
            get;
            set;
        }

        public ResponseStatus ResponseStatus
        {
            get;
            set;
        }
    }

    public class CategoryDto
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }

    public class CategoryService : Service
    {
        private readonly IMayLilyContext context;

        public CategoryService(IMayLilyContext context)
        {
            this.context = context;
        }

        public object Get(GetCategory request)
        {
            Category result = this.context.Categories.Find(request.CategoryID);
            if (result == null)
            {
                throw HttpError.NotFound(string.Format("Unable to find category with id {0}.", request.CategoryID));
            }

            return new GetCategoryResponse
            {
                Result = new CategoryDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description
                }
            };
        }

        public override void Dispose()
        {
            this.context.Dispose();
        }
    }
}