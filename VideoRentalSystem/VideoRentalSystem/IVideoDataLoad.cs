using VideoRentalSystem;

public interface IVideoDataLoader
{
    Task<CustomHashTable> LoadVideoDataAsync(string connectionString);
}
