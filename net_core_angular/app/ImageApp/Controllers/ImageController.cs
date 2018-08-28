using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.Controllers
{
    public class ImageController: BaseApiController
    {
        public ImageController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment) : base(context, hostingEnvironment)
        {

        }

        [HttpGet]
        public IActionResult GetImages()
        {
            var imageModelWithDetailsResultDto = new List<ImageModelWithDetailsResultDto>();
            var images = dbContext.Images.Include(image => image.ImageModelDetails);
            foreach (var image in images)
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

        // DELETE 
        [HttpDelete("{imageId}")]
        public IActionResult Delete(int imageId)
        {
            string result = "fail";
            try
            {
                var found = dbContext.Images.FirstOrDefault(i => i.Id == imageId);
                if (found != null)
                {
                    dbContext.Remove(found);
                    dbContext.SaveChanges();
                    string webRootPath  = hostingEnvironment.WebRootPath;
                    string uploadedFilePath = Path.Combine(webRootPath, found.ContentImageUrl.Replace('/', '\\').TrimStart('\\'));
                    string uploadedFileThumbPath = Path.Combine(webRootPath, found.ContentImageUrlThumb.Replace('/', '\\').TrimStart('\\'));
                    System.IO.File.Delete(uploadedFilePath);
                    System.IO.File.Delete(uploadedFileThumbPath);
                    result = "ok";
                }
            }
            catch (Exception exp)
            {
                result = exp.Message;
            }
            return new JsonResult(result, JsonSettings);
        }
    }
}
