using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;

namespace ImageApp.Data.Repositories
{
    public class ImageDetailsWebRepository: RepositoryBase<ImageModelDetailsWebDb>, IImageDetailsWebRepository
    {
        public ImageDetailsWebRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
    }
}
