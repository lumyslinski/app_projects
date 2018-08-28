namespace ImageApp.Data.Models
{
    public enum UploadStatusCode
    {
        ReadingFromRequest,
        LoadingIntoMemory,
        LoadedIntoMemory,
        LoadingIntoDatabase,
        LoadedIntoDatabase,
        Finished
    }
}
