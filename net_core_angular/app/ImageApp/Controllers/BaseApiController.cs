using ImageApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController: Controller
    {
        #region Private Fields
        protected ApplicationDbContext dbContext;
        protected IHostingEnvironment hostingEnvironment;
        #endregion

        public JsonSerializerSettings JsonSettings { get; private set; }

        public BaseApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.dbContext = context;
            this.hostingEnvironment = hostingEnvironment;
            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }
    }
}
