namespace ImageApp.Data.Models
{
    public static class GoogleVisionApiJsonRequestFabric
    {
        public static GoogleVisionApiJsonRequest GetImageModelJsonRequest(string base64)
        {
            var    imageModelJsonRequestContainer = new GoogleVisionApiJsonRequest();
                   imageModelJsonRequestContainer.requests.Add(new ImageJsonRequest(base64));
            return imageModelJsonRequestContainer;
        }
    }
}
