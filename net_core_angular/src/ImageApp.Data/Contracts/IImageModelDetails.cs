namespace ImageApp.Data.Contracts
{
    public interface IImageModelDetails
    {
        int Id { get; set; }
        int ImageId { get; set; }
        string Mid { get; set; }
        string Description { get; set; }
        double Score { get; set; }
        double Topicality { get; set; }
    }
}
