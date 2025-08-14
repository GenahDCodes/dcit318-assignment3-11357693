using System;

namespace Q5_InventoryLogger
{
    // Immutable inventory item record that implements the marker interface
    public record InventoryItem(
        int Id,
        string Name,
        int Quantity,
        DateTime DateAdded
    ) : IInventoryEntity;
}
