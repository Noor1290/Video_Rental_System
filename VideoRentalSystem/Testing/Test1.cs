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
            try
            {
                // Clean up users after tests
                DeleteUser("TestUser_A", "TestA@test.com");
                DeleteUser("TestUser_B", "TestB@test.com");
                Debug.WriteLine("Test cleanup complete.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during cleanup: {ex.Message}");
            }
            finally
            {
                _dbConnection = null;
            }
        }

        // Data-driven test: run the same test with different sets of user info
        [DataTestMethod]
        [DataRow("TestUser_B", "TestB@test.com", "AnotherSecretXYZ")]
        [DataRow("TestUser_A", "TestA@test.com", "TestPassword123")]
        [DataRow("TestUser_A", "TestA@test.com", "NewPassword123")]  // Attempt to insert duplicate username
        public void InsertUser(string username, string email, string password)
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

                // Check for duplicate user insertion
                if (username == "TestUser_A")
                {
                    // Assert that an exception is thrown when trying to insert the duplicate
                    bool exceptionThrown = false;
                    try
                    {
                        _dbConnection.InsertUser(username, email, password, profilePicture);
                    }
                    catch (SqlException sqlEx)
                    {
                        if (sqlEx.Message.Contains("UNIQUE constraint"))
                        {
                            exceptionThrown = true;  // Expected exception, the test should pass
                            Debug.WriteLine("Expected UNIQUE constraint violation occurred.");
                        }
                        else
                        {
                            Debug.WriteLine($"Unexpected SQL exception: {sqlEx.Message}");
                        }
                    }

                    // Assert that the exception was thrown
                    Assert.IsTrue(exceptionThrown, "Expected a UNIQUE constraint violation but no exception was thrown.");
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                Assert.IsTrue(sqlEx.Message.Contains("Violated UNIQUE constraint"), "Expected unique constraint violation.");
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
        // Delete user from the database
        private static void DeleteUser(string username, string email)
        {
            string sql = @"
                DELETE FROM Users
                WHERE Username = @Username
                  AND Email = @Email
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

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Debug.WriteLine($"User '{username}' with email '{email}' deleted successfully.");
                        }
                        else
                        {
                            Debug.WriteLine($"User '{username}' with email '{email}' not found for deletion.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error during deletion: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General Error during deletion: {ex.Message}");
            }
        }
    }
}
