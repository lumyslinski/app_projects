namespace ImageApp.Data.Contracts
{
    public interface IImageModelWebMatches
    {
        int Id { get; set; }
        int ImageId { get; set; }
        string Url { get; set; }
        string UrlImage { get; set; }
        string PageTitle { get; set; }
    }
}
