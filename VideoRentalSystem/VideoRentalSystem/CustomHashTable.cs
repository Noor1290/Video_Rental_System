using System.Collections;


namespace VideoRentalSystem
{
    // CustomHashTable implements a simple hash table using separate chaining for collision handling.
    public class CustomHashTable : IEnumerable<KeyValuePair<string, object>>
    {
        // Private nested class representing a node in the hash table's linked list.
        private class HashNode
        {
            // The key for the hash table entry.
            public string Key { get; }
            // The value associated with the key.
            public object Value { get; set; }
            // Pointer to the next node in the chain (for handling collisions).
            public HashNode? Next { get; set; }

            // Constructor that initializes the node with a key and value.
            public HashNode(string key, object value)
            {
                // Ensure the key is not null.
                Key = key ?? throw new ArgumentNullException(nameof(key));
                // Ensure the value is not null.
                Value = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        // Array of buckets where each bucket is a linked list of HashNode objects.
        private readonly HashNode?[] buckets;
        // The size of the buckets array (capacity of the hash table).
        private readonly int size;

        // Constructor that initializes the hash table with a specified capacity.
        public CustomHashTable(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

            // Initialize buckets with the given capacity.
            buckets = new HashNode[capacity];
            size = capacity;
        }

        // Computes the index for a given key.
        private int GetHash(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            // Use the built-in GetHashCode, mod by size, and take absolute value to ensure index is valid.
            return Math.Abs(key.GetHashCode() % size);
        }

        // Adds a key-value pair to the hash table.
        public void Add(string key, object value)
        {
            int index = GetHash(key);  // Determine which bucket to use.
            HashNode? node = buckets[index];

            // Traverse the linked list in the bucket to check if the key already exists.
            while (node != null)
            {
                if (node.Key == key)
                {
                    node.Value = value; // Update the value if the key already exists.
                    return;
                }
                node = node.Next;
            }

            // Insert new node at the head of the linked list for simplicity.
            buckets[index] = new HashNode(key, value) { Next = buckets[index] };
        }

        // Overloaded Add method to allow adding a KeyValuePair directly.
        public void Add(KeyValuePair<string, object> pair)
        {
            Add(pair.Key, pair.Value);
        }

        // Removes the key-value pair from the hash table using the key.
        public void Remove(string key)
        {
            int index = GetHash(key);  // Identify the correct bucket.
            HashNode? node = buckets[index];
            HashNode? previous = null;

            // Traverse the linked list in the bucket.
            while (node != null)
            {
                if (node.Key == key)
                {
                    if (previous == null)
                        buckets[index] = node.Next; // Remove the first node.
                    else
                        previous.Next = node.Next; // Remove the node by skipping it in the chain.

                    return;
                }
                previous = node;
                node = node.Next;
            }
        }

        // Retrieves the value associated with the given key.
        public object? Get(string key)
        {
            int index = GetHash(key);  // Find the appropriate bucket.
            HashNode? node = buckets[index];

            // Traverse the linked list looking for the key.
            while (node != null)
            {
                if (node.Key == key)
                    return node.Value; // Return the value if found.

                node = node.Next;
            }

            return null; // Return null if the key does not exist.
        }

        // Indexer for convenient access to the hash table using array-like syntax.
        public object? this[string key]
        {
            get => Get(key);  // Retrieve the value.
            set => Add(key, value ?? throw new ArgumentNullException(nameof(value))); // Add or update the key-value pair.
        }

        // Checks if the key exists in the hash table.
        public bool ContainsKey(string key)
        {
            return Get(key) != null;
        }

        // Provides an enumerator for iterating through the hash table's key-value pairs.
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            // Loop through each bucket in the array.
            foreach (var bucket in buckets)
            {
                HashNode? node = bucket;
                // Iterate through each node in the linked list.
                while (node != null)
                {
                    yield return new KeyValuePair<string, object>(node.Key, node.Value);
                    node = node.Next;
                }
            }
        }

        // Explicit non-generic enumerator implementation.
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
