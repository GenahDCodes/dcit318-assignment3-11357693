using System;

namespace Q3_WarehouseInventory
{
    // Thrown when attempting to add an item that already exists
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string key)
            : base($"An item with key '{key}' already exists.") { }
    }

    // Thrown when an item cannot be found by its key
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string key)
            : base($"No item found with key '{key}'.") { }
    }

    // Thrown when a stock operation would result in negative quantity
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string key, int requested, int available)
            : base($"Insufficient stock for '{key}'. Requested {requested}, available {available}.") { }
    }

    // Thrown when quantity is zero/negative or otherwise invalid
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(int qty)
            : base($"Invalid quantity: {qty}. Quantity must be a positive integer.") { }
    }
}
