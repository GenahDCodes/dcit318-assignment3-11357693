using System;

namespace Q3_WarehouseInventory
{
    // Inventory item with a unique Key (e.g., SKU)
    public class InventoryItem : IHasKey
    {
        public string Key { get; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public string Category { get; private set; }

        public InventoryItem(string key, string name, int quantity, string category)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be empty.", nameof(key));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");
            if (string.IsNullOrWhiteSpace(category)) category = "Uncategorized";

            Key = key.Trim();
            Name = name.Trim();
            Quantity = quantity;
            Category = category.Trim();
        }

        public void Receive(int qty)
        {
            if (qty <= 0) throw new InvalidQuantityException(qty);
            checked { Quantity += qty; }
        }

        public void Ship(int qty)
        {
            if (qty <= 0) throw new InvalidQuantityException(qty);
            if (qty > Quantity) throw new InsufficientStockException(Key, qty, Quantity);
            Quantity -= qty;
        }

        public override string ToString()
            => $"{Key} | {Name} | Qty: {Quantity} | Category: {Category}";
    }
}
