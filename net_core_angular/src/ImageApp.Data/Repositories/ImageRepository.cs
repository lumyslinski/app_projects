using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;

namespace ImageApp.Data.Repositories
{
    public class ImageRepository: RepositoryBase<ImageModelDb>, IImageRepository
    {
        public ImageRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
    }
}
