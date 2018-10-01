using System;
using System.Collections.Generic;
using ImageApp.Data.Contracts;
using ImageApp.Data.Models;

namespace ImageApp.Data.Services
{
    public class ImageService: IImageService
    {
        private IImageRepository imageRepository;
        private IImageDetailsRepository imageDetailsRepository;
        private IImageDetailsWebRepository imageDetailsWebRepository;
        private IImageWebMatchesRepository imageWebMatchesRepository;

        public ImageService(IImageRepository imageRepository, IImageDetailsRepository imageDetailsRepository, IImageDetailsWebRepository imageDetailsWebRepository, IImageWebMatchesRepository imageWebMatchesRepository)
        {
            this.imageRepository            = imageRepository;
            this.imageDetailsRepository     = imageDetailsRepository;
            this.imageDetailsWebRepository  = imageDetailsWebRepository;
            this.imageWebMatchesRepository  = imageWebMatchesRepository;
        }

        public ImageServiceResultConcreteModel CreateImage(string name, string title, long length, int width, int height, string contentType, string contentImageUrl, string contentImageUrlThumb)
        {
            var result = new ImageServiceResultConcreteModel();
            try
            {
                var imageModelDb = new ImageModelDb()
                {
                    Name = name,
                    Title = title,
                    Length = length,
                    Width = width,
                    Height = height,
                    ContentType = contentType,
                    ContentImageUrl = contentImageUrl,
                    ContentImageUrlThumb = contentImageUrlThumb,
                    CreatedDate = DateTime.Now
                };
                var newImage = this.imageRepository.Create(imageModelDb);
                result.SetData(newImage);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModelDetails CreateImageModelDetails(double score, string mid, double topicality, string description, int imageId)
        {
            var result = new ImageServiceResultConcreteModelDetails();
            try
            {
                var imageModelDetails = new ImageModelDetailsDb()
                {
                    Description = description,
                    ImageId = imageId,
                    Mid = mid,
                    Score = score,
                    Topicality = topicality
                };
                var added = this.imageDetailsRepository.Create(imageModelDetails);
                result.SetData(added);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModelDetailsWeb CreateImageModelDetailsWeb(double score, string description, string entityId, int imageId)
        {
            var result = new ImageServiceResultConcreteModelDetailsWeb();
            try
            {
                var imageModelDetailsWebDb = new ImageModelDetailsWebDb()
                {
                    Score = score,
                    Description = description,
                    EntityId = entityId,
                    ImageId = imageId
                };
                var added = this.imageDetailsWebRepository.Create(imageModelDetailsWebDb);
                result.SetData(added);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModelWebMatches CreateImageWebMatches(string url, string pageTitle, string urlImage, int imageId)
        {
            var result = new ImageServiceResultConcreteModelWebMatches();
            try
            {
                var imageModelWebMatchesDb = new ImageModelWebMatchesDb()
                {
                    Url = url,
                    PageTitle = pageTitle,
                    UrlImage = urlImage,
                    ImageId = imageId
                };
                var added = this.imageWebMatchesRepository.Create(imageModelWebMatchesDb);
                result.SetData(added);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModelList ReadImages(string include)
        {
            var result = new ImageServiceResultConcreteModelList();
            try
            {
                var items = this.imageRepository.Read(include);
                result.SetData(items);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModel GetImage(int id, string include=null)
        {
            var result = new ImageServiceResultConcreteModel();
            try
            {
                var found = this.imageRepository.GetItem(id,include);
                result.SetData(found);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }

            return result;
        }

        public ImageServiceResultConcreteModel Update(int id, ImageModelDb entity)
        {
            var result = new ImageServiceResultConcreteModel();
            try
            {
                var updated = this.imageRepository.Update(id, entity);
                result.SetData(updated);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }
            return result;
        }

        public ImageServiceResultConcreteModel Delete(int id)
        {
            var result = new ImageServiceResultConcreteModel();
            try
            {
                var deleted = this.imageRepository.Delete(id);
                result.SetData(deleted);
            }
            catch (Exception exp)
            {
                result.SetError(exp);
            }
            return result;
        }
    }
}
