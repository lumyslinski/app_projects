namespace ImageApp.Data.Contracts
{
    public interface IImageModelDetailsWeb
    {
        int Id { get; set; }
        int ImageId { get; set; }
        string EntityId { get; set; }
        double Score { get; set; }
        string Description { get; set; }
    }
}
