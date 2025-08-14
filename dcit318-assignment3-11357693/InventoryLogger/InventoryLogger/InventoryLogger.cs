using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Q5_InventoryLogger
{
    // Generic logger for any inventory entity
    // NOTE: You can't constrain to "record" in C#, so we enforce the interface.
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new();
        private readonly string _filePath;

        // JSON options (pretty + safe DateTime handling)
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public InventoryLogger(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));

            _filePath = filePath;
        }

        // Add item to the in-memory log
        public void Add(T item) => _log.Add(item);

        // Return a copy of all items
        public List<T> GetAll() => new(_log);

        // Serialize and save to file
        public void SaveToFile()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

                using var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                JsonSerializer.Serialize(fs, _log, _jsonOptions);

                Console.WriteLine($"Saved {_log.Count} item(s) to: {_filePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission error while saving: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error while saving: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving: {ex.Message}");
            }
        }

        // Load from file into the in-memory log
        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("No existing file found. Starting with empty log.");
                    return;
                }

                using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var loaded = JsonSerializer.Deserialize<List<T>>(fs, _jsonOptions) ?? new List<T>();

                _log.Clear();
                _log.AddRange(loaded);

                Console.WriteLine($"Loaded {_log.Count} item(s) from: {_filePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission error while loading: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Data format error while loading: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error while loading: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading: {ex.Message}");
            }
        }
    }
}
