using System;
using System.IO;
using System.Net.Http.Headers;
using ImageApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageApp.Controllers
{
    public class UploadController: BaseApiController
    {
        public UploadController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment) : base(context, hostingEnvironment)
        {
            
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Index()
        {
            UploadResultDto uploadResult = new UploadResultDto();
            UploadStatusCode statusCode = UploadStatusCode.ReadingFromRequest;
            try
            {
                ImageModelDb imageModelDb = null;
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string webRootPath = hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, "uploaded");
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    var fileExtension = System.IO.Path.GetExtension(fullPath);
                    string fullPathThumb = fullPath.Replace(fileExtension, "_thumb" + fileExtension);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    using (var ms = new MemoryStream())
                    {
                        statusCode = UploadStatusCode.LoadingIntoMemory;
                        file.OpenReadStream().CopyTo(ms);
                        uploadResult.Base64 = Convert.ToBase64String(ms.ToArray());
                        ms.Seek(0, SeekOrigin.Begin);
                        statusCode = UploadStatusCode.LoadedIntoMemory;
                        using (var image = Image.Load(ms))
                        {
                            imageModelDb = new ImageModelDb()
                            {
                                Name = fileName,
                                Title = fileName,
                                Length = ms.Length,
                                Width = image.Width,
                                Height = image.Height,
                                ContentType = file.ContentType,
                                ContentImageUrl = fullPath.Replace(webRootPath,"").Replace("\\","/"),
                                ContentImageUrlThumb = fullPathThumb.Replace(webRootPath,"").Replace("\\", "/"),
                                CreatedDate = DateTime.Now
                            };
                            image.Save(fullPath);
                            image.Mutate(x => x.Resize(100,100));
                            image.Save(fullPathThumb);
                        }
                    }
                }
                if (imageModelDb != null)
                {
                    statusCode = UploadStatusCode.LoadingIntoDatabase;
                    dbContext.Images.Add(imageModelDb);
                    dbContext.SaveChanges();
                    uploadResult.Id = imageModelDb.Id;
                }
            }
            catch (Exception ex)
            {
                uploadResult.Error = Convert.ToString(string.Format("Upload Failed: {0}. Status code: {1}",ex.Message, statusCode));
            }
            return Json(uploadResult);
        }
    }
}
