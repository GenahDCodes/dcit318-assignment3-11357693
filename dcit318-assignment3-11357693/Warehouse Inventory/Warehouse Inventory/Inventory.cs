using System;
using System.Collections.Generic;

namespace Q3_WarehouseInventory
{
    // Generic in-memory inventory keyed by T.Key
    public class Inventory<T> where T : IHasKey
    {
        private readonly Dictionary<string, T> _items = new(StringComparer.OrdinalIgnoreCase);

        public void Add(T item)
        {
            if (_items.ContainsKey(item.Key))
                throw new DuplicateItemException(item.Key);

            _items[item.Key] = item;
        }

        public T Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty.", nameof(key));

            if (!_items.TryGetValue(key, out var item))
                throw new ItemNotFoundException(key);

            return item;
        }

        public bool TryGet(string key, out T? item) => _items.TryGetValue(key, out item!);

        public void Remove(string key)
        {
            if (!_items.Remove(key))
                throw new ItemNotFoundException(key);
        }

        public IReadOnlyCollection<T> GetAll() => _items.Values;

        public void Receive(string key, int qty)
        {
            var item = Get(key);
            (item as InventoryItem)?.Receive(qty); // concrete behavior
        }

        public void Ship(string key, int qty)
        {
            var item = Get(key);
            (item as InventoryItem)?.Ship(qty); // concrete behavior
        }
    }
}
