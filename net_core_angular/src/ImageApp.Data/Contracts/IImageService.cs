using ImageApp.Data.Models;
using System.Collections.Generic;

namespace ImageApp.Data.Contracts
{
    public interface IImageService
    {
        ImageServiceResultConcreteModel CreateImage(string name, string title, long length, int width, int height, string contentType, string contentImageUrl, string contentImageUrlThumb);
        ImageServiceResultConcreteModelDetails CreateImageModelDetails(double score, string mid, double topicality, string description, int imageId);
        ImageServiceResultConcreteModelWebMatches CreateImageWebMatches(string url, string pageTitle, string urlImage, int imageId);
        ImageServiceResultConcreteModelDetailsWeb CreateImageModelDetailsWeb(double score, string description, string entityId, int imageId);
        ImageServiceResultConcreteModelList ReadImages(string include);
        ImageServiceResultConcreteModel GetImage(int id, string include=null);
        ImageServiceResultConcreteModel Update(int id, ImageModelDb entity);
        ImageServiceResultConcreteModel Delete(int id);
    }
}
