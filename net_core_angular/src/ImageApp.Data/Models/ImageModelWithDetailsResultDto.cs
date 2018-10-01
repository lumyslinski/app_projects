namespace ImageApp.Data.Models
{
    public class ImageModelWithDetailsResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSrc { get; set; }
        public string UrlThumbSrc { get; set; }
        public string Error { get; set; }
        public string GoogleVisionApiDetails { get; set; }
    }
}
