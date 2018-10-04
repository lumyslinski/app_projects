using System;
using System.Collections.Generic;
using System.IO;
using ImageApp.Data.Contracts;
using ImageApp.Data.Models;
using ImageApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    public class ImageController: BaseApiController
    {
        public ImageController(IImageService imageService, IHostingEnvironment hostingEnvironment) : base(imageService, hostingEnvironment)
        {

        }

        [HttpGet]
        public IActionResult GetImages()
        {
            var imageModelWithDetailsResultDto = new List<ImageModelWithDetailsResultDto>();
            var images = imageService.ReadImages("ImageModelDetails");
            if (images.IsOk)
            {
                foreach (var image in images.Data)
                {
                    var gv = new GoogleVisionApiDetailsWrapper();
                    gv.Add(image.ImageModelDetails);
                    imageModelWithDetailsResultDto.Add(new ImageModelWithDetailsResultDto()
                    {
                        Id = image.Id,
                        Name = image.Name,
                        UrlSrc = image.ContentImageUrl,
                        UrlThumbSrc = image.ContentImageUrlThumb,
                        GoogleVisionApiDetails = gv.ToString()
                    });
                }
                return new JsonResult(imageModelWithDetailsResultDto, JsonSettings);
            }
            else
            {
                return new BadRequestObjectResult(images.Error);
            }
        }

        // DELETE 
        [HttpDelete("{imageId}")]
        public IActionResult Delete(int imageId)
        {
            string result = "fail";
            try
            {
                var deleted = imageService.Delete(imageId);
                if (deleted.IsOk)
                {
                    string webRootPath  = hostingEnvironment.WebRootPath;
                    string uploadedFilePath = Path.Combine(webRootPath, GetPath(deleted.Data.ContentImageUrl));
                    string uploadedFileThumbPath = Path.Combine(webRootPath, GetPath(deleted.Data.ContentImageUrlThumb));
                    System.IO.File.Delete(uploadedFilePath);
                    System.IO.File.Delete(uploadedFileThumbPath);
                    result = "ok";
                }
                else
                {
                    return new BadRequestObjectResult(deleted.Error);
                }
            }
            catch (Exception exp)
            {
                result = exp.Message;
            }
            return new JsonResult(result, JsonSettings);
        }

        private string GetPath(string path) {
            return path.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
        }
    }
}
