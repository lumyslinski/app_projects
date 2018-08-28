using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ImageApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageApp.Controllers
{
    public class GoogleVisionApiController: BaseApiController
    {
        public GoogleVisionApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment) : base(context, hostingEnvironment)
        {

        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody]UploadResultDto model)
        {
            ImageModelWithDetailsResultDto result = new ImageModelWithDetailsResultDto() {Id = model.Id};
            try
            {
                var image = dbContext.Images.FirstOrDefault(i => i.Id == model.Id);
                if (image != null)
                {
                    result.Name   = image.Name;
                    result.UrlSrc = image.ContentImageUrl;
                    result.UrlThumbSrc = image.ContentImageUrlThumb;
                    if (image.ImageModelDetails != null)
                    {
                        var gv = new GoogleVisionApiDetailsWrapper();
                            gv.Add(image.ImageModelDetails);
                        result.GoogleVisionApiDetails = gv.ToString();
                    }
                    else
                    {
                        var googleVisionApiJsonResponseDto = await GetVisionApiResult(model.Base64);
                        if (googleVisionApiJsonResponseDto != null && String.IsNullOrEmpty(googleVisionApiJsonResponseDto.Error) &&
                            googleVisionApiJsonResponseDto.GoogleVisionApiJsonResponse != null &&
                            googleVisionApiJsonResponseDto.GoogleVisionApiJsonResponse.responses != null)
                        {
                            foreach (var itemResponse in googleVisionApiJsonResponseDto.GoogleVisionApiJsonResponse.responses)
                            {
                                var gv = new GoogleVisionApiDetailsWrapper();
                                foreach (var imageJsonResultLabelAnnotation in itemResponse.labelAnnotations)
                                {
                                    var imageModelDetail = new ImageModelDetailsDb()
                                    {
                                        Score = imageJsonResultLabelAnnotation.score,
                                        Mid = imageJsonResultLabelAnnotation.mid,
                                        Topicality = imageJsonResultLabelAnnotation.topicality,
                                        Description = imageJsonResultLabelAnnotation.description,
                                        ImageId = image.Id,
                                        Image = image
                                    };
                                    gv.Add(imageModelDetail);
                                    dbContext.ImageModelDetails.Add(imageModelDetail);
                                }
                                result.GoogleVisionApiDetails = gv.ToString();

                                if (itemResponse.ImageJsonResultWebDetection != null &&
                                    itemResponse.ImageJsonResultWebDetection.pagesWithMatchingImages != null)
                                {
                                    foreach (var imageJsonResultPagesWithMatchingImages in itemResponse
                                        .ImageJsonResultWebDetection.pagesWithMatchingImages)
                                    {
                                        var foundUrlImageObject = imageJsonResultPagesWithMatchingImages
                                            .fullMatchingImages.FirstOrDefault();
                                        var foundUrlImageString = "";
                                        if (foundUrlImageObject != null)
                                            foundUrlImageString = foundUrlImageObject.url;
                                        var imageModelWebMatches = new ImageModelWebMatchesDb()
                                        {
                                            Url = imageJsonResultPagesWithMatchingImages.url,
                                            PageTitle = imageJsonResultPagesWithMatchingImages.pageTitle,
                                            UrlImage = foundUrlImageString,
                                            ImageId = image.Id,
                                            Image = image
                                        };
                                        dbContext.ImageModelWebMatches.Add(imageModelWebMatches);
                                    }
                                }

                                if (itemResponse.ImageJsonResultWebDetection != null &&
                                    itemResponse.ImageJsonResultWebDetection.webEntities != null)
                                {
                                    foreach (var imageJsonResultWebEntities in itemResponse.ImageJsonResultWebDetection
                                        .webEntities)
                                    {
                                        var imageModelDetailsWebDb = new ImageModelDetailsWebDb()
                                        {
                                            Score = imageJsonResultWebEntities.score,
                                            Description = imageJsonResultWebEntities.description,
                                            EntityId = imageJsonResultWebEntities.entityId,
                                            ImageId = image.Id,
                                            Image = image
                                        };
                                        dbContext.ImageModelDetailsWeb.Add(imageModelDetailsWebDb);
                                    }
                                }
                            }
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            if (googleVisionApiJsonResponseDto != null)
                            {
                                result.Error = string.Format("Result from google vision api is wrong! Error: {0}",googleVisionApiJsonResponseDto.Error);
                            }
                            else
                            {
                                result.Error = "Result from google vision api is wrong! Error: null response";
                            }
                        }
                    }
                }
                else
                {
                    result.Error = "Failed! Can not get ImageJsonRequest from database";
                    
                }
            }
            catch (Exception exp)
            {
                result.Error = exp.Message;
            }
            return new JsonResult(result, this.JsonSettings);
        }

        public async Task<GoogleVisionApiJsonResponseDto> GetVisionApiResult(string base64)
        {
            string response = "";
            if (!String.IsNullOrEmpty(Startup.GoogleVisionApiUrl))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ConnectionClose = true;
                    var val = GoogleVisionApiJsonRequestFabric.GetImageModelJsonRequest(base64);
                    using (var resultApi = await client.PostAsJsonAsync(Startup.GoogleVisionApiUrl, val))
                    {
                        response = await resultApi.Content.ReadAsStringAsync();
                    }
                }

                var result = JsonConvert.DeserializeObject<GoogleVisionApiJsonResponse>(response);
                if (result != null)
                    return new GoogleVisionApiJsonResponseDto() {GoogleVisionApiJsonResponse = result};
                else
                    return new GoogleVisionApiJsonResponseDto()
                        {Error = "Failed! Can not deserialize json response to GoogleVisionApiJsonResponse"};
            }
            else
                return new GoogleVisionApiJsonResponseDto()
                    { Error = "Failed! Set up google vision api url and secret token." };
        }
    }
}
