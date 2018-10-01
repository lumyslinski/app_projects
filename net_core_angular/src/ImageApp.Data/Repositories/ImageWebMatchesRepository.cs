using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;

namespace ImageApp.Data.Repositories
{
    public class ImageWebMatchesRepository: RepositoryBase<ImageModelWebMatchesDb>, IImageWebMatchesRepository
    {
        public ImageWebMatchesRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
    }
}
