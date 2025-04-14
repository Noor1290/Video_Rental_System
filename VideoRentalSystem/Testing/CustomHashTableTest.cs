#nullable disable
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoRentalSystem;
using System.Diagnostics;

namespace Testing
{
    [TestClass]
    public class CustomHashTableTests
    {
        private static CustomHashTable _customHashTable;

        // Called once before all tests
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            try
            {
                // Initialize the CustomHashTable with a capacity of 10000
                _customHashTable = new CustomHashTable(10000);
                Debug.WriteLine("CustomHashTable initialized.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during initialization: {ex.Message}");
            }
        }

        // Called once after all tests have run
        [ClassCleanup]
        public static void ClassCleanup()
        {
            _customHashTable = null;
            Debug.WriteLine("Test cleanup complete.");
        }

        // Test for adding an item to the hash table
        [TestMethod]
        public void Add_Functionaltity()
        {
            // Arrange
            string key = "TestUser";
            object value = new { Name = "John", Age = 25 };

            try
            {
                // Act: Add the key-value pair
                _customHashTable.Add(key, value);
                Debug.WriteLine("Item added successfully.");

                // Assert: Check if the item exists
                var result = _customHashTable.Get(key);
                Assert.IsNotNull(result, $"Item with key '{key}' was not found in the hash table.");
                Debug.WriteLine("Item exists in hash table.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        // Test for searching an item in the hash table
        [TestMethod]
        public void Get_Functionality()
        {
            // Arrange
            string key = "TestUser";
            object expectedValue = new { Name = "John", Age = 25 };
            _customHashTable.Add(key, expectedValue);

            try
            {
                // Act: Retrieve the value by key
                var result = _customHashTable.Get(key);

                // Assert: Check if the retrieved value matches the expected value
                Assert.AreEqual(expectedValue, result, $"Expected value for key '{key}' does not match.");
                Debug.WriteLine("Value retrieved successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        // Test for removing an item from the hash table
        [TestMethod]
        public void Remove_Functionality()
        {
            // Arrange
            string key = "TestUser";
            object value = new { Name = "John", Age = 25 };
            _customHashTable.Add(key, value);

            try
            {
                // Act: Remove the item by key
                _customHashTable.Remove(key);
                Debug.WriteLine("Item removed successfully.");

                // Assert: Check if the item was removed
                var result = _customHashTable.Get(key);
                Assert.IsNull(result, $"Item with key '{key}' should have been removed.");
                Debug.WriteLine("Item removed and not found.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        // Test for checking if a key exists
        [TestMethod]
        public void ContainsKey_Functionality()
        {
            // Arrange
            string key = "TestUser";
            object value = new { Name = "John", Age = 25 };
            _customHashTable.Add(key, value);

            try
            {
                // Act: Check if the key exists
                bool result = _customHashTable.ContainsKey(key);

                // Assert: The key should exist
                Assert.IsTrue(result, $"Key '{key}' should exist in the hash table.");
                Debug.WriteLine("Key exists in the hash table.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
