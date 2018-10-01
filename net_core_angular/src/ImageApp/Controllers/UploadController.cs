using System;
using System.IO;
using System.Net.Http.Headers;
using ImageApp.Data.Contracts;
using ImageApp.Data.Models;
using ImageApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageApp.Controllers
{
    public class UploadController: BaseApiController
    {
        public UploadController(IImageService imageService, IHostingEnvironment hostingEnvironment) : base(imageService, hostingEnvironment)
        {
            
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Index()
        {
            UploadResultDto uploadResult = new UploadResultDto();
            UploadStatusCode statusCode = UploadStatusCode.ReadingFromRequest;
            try
            {
                string fileName = null;
                string fullPath = null;
                string fullPathThumb = null;
                string webRootPath = null;
                IFormFile file = Request.Form.Files[0];
                int width = 0;
                int height = 0;
                long length = 0;
                if (file.Length > 0)
                {
                    webRootPath = hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, "uploaded");
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    var fileExtension = System.IO.Path.GetExtension(fullPath);
                    fullPathThumb = fullPath.Replace(fileExtension, "_thumb" + fileExtension);
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
                            length = ms.Length;
                            width = image.Width;
                            height = image.Height;
                            statusCode = UploadStatusCode.SavingIntoDisk;
                            image.Save(fullPath);
                            image.Mutate(x => x.Resize(100,100));
                            image.Save(fullPathThumb);
                            statusCode = UploadStatusCode.SavedIntoDisk;
                        }
                    }
                }
                if (statusCode == UploadStatusCode.SavedIntoDisk)
                {
                    statusCode = UploadStatusCode.LoadingIntoDatabase;
                    var newImageResult = imageService.CreateImage(fileName, fileName, length, width, height,
                        file.ContentType, GetValidPath(webRootPath, fullPath),
                        GetValidPath(webRootPath, fullPathThumb));
                    if (String.IsNullOrEmpty(newImageResult.Error))
                    {
                        statusCode = UploadStatusCode.LoadedIntoDatabase;
                        uploadResult.Id = newImageResult.Id;
                    }
                    else
                    {
                        throw new Exception(newImageResult.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                uploadResult.Error = Convert.ToString(string.Format("Upload Failed: {0}. Status code: {1}",ex.Message, statusCode));
            }
            return Json(uploadResult);
        }

        string GetValidPath(string webRootPath, string path)
        {
            return path.Replace(webRootPath, "").Replace("\\", "/");
        }
    }
}
