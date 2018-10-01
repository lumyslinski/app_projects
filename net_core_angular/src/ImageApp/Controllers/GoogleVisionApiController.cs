using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;
using ImageApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageApp.Controllers
{
    public class GoogleVisionApiController: BaseApiController
    {
        public GoogleVisionApiController(IImageService imageService, IHostingEnvironment hostingEnvironment) : base(imageService, hostingEnvironment)
        {

        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody]UploadResultDto model)
        {
            ImageModelWithDetailsResultDto result = new ImageModelWithDetailsResultDto() {Id = model.Id};
            try
            {
                var image = imageService.GetImage(model.Id);
                if (image != null && image.Data != null)
                {
                    result.Name   = image.Data.Name;
                    result.UrlSrc = image.Data.ContentImageUrl;
                    result.UrlThumbSrc = image.Data.ContentImageUrlThumb;
                    if (image.Data.ImageModelDetails != null)
                    {
                        var gv = new GoogleVisionApiDetailsWrapper();
                            gv.Add(image.Data.ImageModelDetails);
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
                                   var added = imageService.CreateImageModelDetails(imageJsonResultLabelAnnotation.score,
                                        imageJsonResultLabelAnnotation.mid, imageJsonResultLabelAnnotation.topicality,
                                        imageJsonResultLabelAnnotation.description, image.Id);
                                    if(added.IsOk)
                                        gv.Add(added.Data);
                                }
                                result.GoogleVisionApiDetails = gv.ToString();

                                if (itemResponse.ImageJsonResultWebDetection != null &&
                                    itemResponse.ImageJsonResultWebDetection.pagesWithMatchingImages != null)
                                {
                                    foreach (var imageJsonResultPagesWithMatchingImages in itemResponse.ImageJsonResultWebDetection.pagesWithMatchingImages)
                                    {
                                        var foundUrlImageObject = imageJsonResultPagesWithMatchingImages
                                            .fullMatchingImages.FirstOrDefault();
                                        var foundUrlImageString = "";
                                        if (foundUrlImageObject != null)
                                            foundUrlImageString = foundUrlImageObject.url;
                                        imageService.CreateImageWebMatches(imageJsonResultPagesWithMatchingImages.url, imageJsonResultPagesWithMatchingImages.pageTitle, foundUrlImageString, image.Id);
                                    }
                                }

                                if (itemResponse.ImageJsonResultWebDetection != null &&
                                    itemResponse.ImageJsonResultWebDetection.webEntities != null)
                                {
                                    foreach (var imageJsonResultWebEntities in itemResponse.ImageJsonResultWebDetection
                                        .webEntities)
                                    {
                                        imageService.CreateImageModelDetailsWeb(imageJsonResultWebEntities.score,imageJsonResultWebEntities.description,imageJsonResultWebEntities.entityId,image.Id);
                                    }
                                }
                            }
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
