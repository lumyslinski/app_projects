using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;

namespace ImageApp.Data.Repositories
{
    public class ImageDetailsRepository: RepositoryBase<ImageModelDetailsDb>, IImageDetailsRepository
    {
        public ImageDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
