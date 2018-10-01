using ImageApp.Data.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController: Controller
    {
        #region Private Fields
        protected IImageService imageService;
        protected IHostingEnvironment hostingEnvironment;
        #endregion

        public JsonSerializerSettings JsonSettings { get; private set; }

        public BaseApiController(IImageService imageService, IHostingEnvironment hostingEnvironment)
        {
            this.imageService = imageService;
            this.hostingEnvironment = hostingEnvironment;
            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }
    }
}
