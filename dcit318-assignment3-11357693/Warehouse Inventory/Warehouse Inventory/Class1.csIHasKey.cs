namespace Q3_WarehouseInventory
{
    // Minimal contract so the generic Inventory<T> can work with any keyed model
    public interface IHasKey
    {
        string Key { get; }
    }
}
