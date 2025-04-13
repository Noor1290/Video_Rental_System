using Microsoft.Data.SqlClient;
using System.Diagnostics;
using VideoRentalSystem;

public class VideoDataLoader : IVideoDataLoader
{
    public async Task<CustomHashTable> LoadVideoDataAsync(string connectionString)
    {
        CustomHashTable videoData = new CustomHashTable(10000);

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                string videoQuery = "SELECT * FROM VideoDatabase";

                using (SqlCommand videoCmd = new SqlCommand(videoQuery, conn))
                {
                    using (SqlDataReader videoReader = await videoCmd.ExecuteReaderAsync())
                    {
                        while (await videoReader.ReadAsync())
                        {
                            var videoDetails = new CustomHashTable(10000);
                            videoDetails.Add("VideoID", videoReader["VideoID"].ToString());
                            videoDetails.Add("UserID", videoReader["UserID"].ToString());
                            videoDetails.Add("VideoTitle", videoReader["VideoTitle"].ToString());
                            videoDetails.Add("UploadDate", videoReader["UploadDate"].ToString());
                            videoDetails.Add("TimeLimit", videoReader["TimeLimit"].ToString());
                            videoDetails.Add("Duration", videoReader["Duration"].ToString());
                            videoDetails.Add("VideoPath", videoReader["VideoPath"].ToString());
                            videoDetails.Add("Price", videoReader["Price"].ToString());
                            videoDetails.Add("Genre", videoReader["Genre"].ToString());

                            videoData.Add(videoReader["VideoID"].ToString(), videoDetails);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading video data: {ex.Message}");
        }

        return videoData;
    }
}
