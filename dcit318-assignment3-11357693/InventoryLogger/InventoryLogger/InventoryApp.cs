using System;
using System.Collections.Generic;
using System.IO;

namespace Q5_InventoryLogger
{
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string? filePath = null)
        {
            // Default file path: <app folder>\data\inventory.json
            var defaultPath = Path.Combine(AppContext.BaseDirectory, "data", "inventory.json");
            _logger = new InventoryLogger<InventoryItem>(filePath ?? defaultPath);
        }

        // Add 3–5 items
        public void SeedSampleData()
        {
            var now = DateTime.Now;

            _logger.Add(new InventoryItem(1, "A4 Paper (Ream)", 25, now));
            _logger.Add(new InventoryItem(2, "Black Ink Cartridge", 10, now));
            _logger.Add(new InventoryItem(3, "Stapler", 5, now));
            _logger.Add(new InventoryItem(4, "Desk Lamp", 8, now));
            _logger.Add(new InventoryItem(5, "Notebook", 40, now));
        }

        public void SaveData() => _logger.SaveToFile();

        public void LoadData() => _logger.LoadFromFile();

        public void PrintAllItems()
        {
            List<InventoryItem> items = _logger.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            Console.WriteLine("=== Inventory Items ===");
            foreach (var it in items)
            {
                Console.WriteLine($"{it.Id,-4} | {it.Name,-22} | Qty: {it.Quantity,-4} | Added: {it.DateAdded:g}");
            }
        }

        // Simulate a new session by recreating the logger (clears memory)
        public void ResetSession(string? filePath = null)
        {
            var defaultPath = Path.Combine(AppContext.BaseDirectory, "data", "inventory.json");
            _logger = new InventoryLogger<InventoryItem>(filePath ?? defaultPath);
        }
    }
}
