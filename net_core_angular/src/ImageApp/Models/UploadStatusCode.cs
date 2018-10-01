namespace ImageApp.Models
{
    public enum UploadStatusCode
    {
        ReadingFromRequest,
        LoadingIntoMemory,
        LoadedIntoMemory,
        LoadingIntoDatabase,
        LoadedIntoDatabase,
        SavingIntoDisk,
        SavedIntoDisk,
        Finished
    }
}
