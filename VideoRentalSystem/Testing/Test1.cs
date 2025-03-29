#nullable disable
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.SqlClient;
using VideoRentalSystem;
using System.Diagnostics;

namespace Testing
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        private static DatabaseConnection _dbConnection;
        private static readonly string _connectionString =
            "Server=NOOR\\SQLEXPRESS01;Database=VideoRentalSystem;Integrated Security=True;TrustServerCertificate=True;";

        // Called once before all tests
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            try
            {
                _dbConnection = new DatabaseConnection("NOOR\\SQLEXPRESS01", "VideoRentalSystem");
                Debug.WriteLine("Database connection initialized.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL connection failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General Error during initialization: {ex.Message}");
            }
        }

        // Called once after all tests have run
        [ClassCleanup]
        public static void ClassCleanup()
        {
            _dbConnection = null;
            Debug.WriteLine("Test cleanup complete.");
        }

        // Data-driven test: run the same test with different sets of user info
        [DataTestMethod]
        [DataRow("TestUser_A", "TestA@test.com", "TestPassword123")]
        [DataRow("TestUser_B", "TestB@test.com", "AnotherSecretXYZ")]
        public void InsertUser_ShouldCreateUser_WhenDataIsValid(
            string username, string email, string password)
        {
            // Arrange
            byte[] profilePicture = new byte[] { 0x01, 0x02, 0x03 };

            try
            {
                // Act: Insert user
                _dbConnection.InsertUser(username, email, password, profilePicture);
                Debug.WriteLine("User inserted successfully.");

                // Assert: Check if user actually exists in the DB
                bool userExists = DoesUserExist(username, email, password);
                Assert.IsTrue(userExists, $"User '{username}' was not found in DB after insertion.");
                Debug.WriteLine("User exists in DB.");
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General Error: {ex.Message}");
            }
        }

        // Check existence by matching username/email/password
        private bool DoesUserExist(string username, string email, string password)
        {
            bool exists = false;
            string sql = @"
                SELECT COUNT(*) 
                FROM Users
                WHERE Username = @Username
                  AND Email = @Email
                  AND Password = @Password
            ";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = (int)cmd.ExecuteScalar();
                        exists = (count > 0);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error during check: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General Error during check: {ex.Message}");
            }

            return exists;
        }
    }
}
