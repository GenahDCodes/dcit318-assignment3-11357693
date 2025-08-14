using System;

namespace Q3_WarehouseInventory
{
    public class WarehouseApp
    {
        private readonly Inventory<InventoryItem> _inventory = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Warehouse Inventory (Q3) ===");
                Console.WriteLine("1) Add Item");
                Console.WriteLine("2) Receive Stock");
                Console.WriteLine("3) Ship Stock");
                Console.WriteLine("4) View Item");
                Console.WriteLine("5) List All Items");
                Console.WriteLine("6) Remove Item");
                Console.WriteLine("7) Exit");
                Console.Write("Choose an option (1-7): ");

                var choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddItem(); break;
                        case "2": ReceiveStock(); break;
                        case "3": ShipStock(); break;
                        case "4": ViewItem(); break;
                        case "5": ListAll(); break;
                        case "6": RemoveItem(); break;
                        case "7": return;
                        default:
                            Console.WriteLine("Invalid option. Please select 1-7.");
                            break;
                    }
                }
                catch (DuplicateItemException ex) { PrintError(ex.Message); }
                catch (ItemNotFoundException ex) { PrintError(ex.Message); }
                catch (InsufficientStockException ex) { PrintError(ex.Message); }
                catch (InvalidQuantityException ex) { PrintError(ex.Message); }
                catch (ArgumentException ex) { PrintError(ex.Message); }
                catch (OverflowException) { PrintError("Value too large."); }
                catch (Exception ex) { PrintError("Unexpected error: " + ex.Message); }

                Pause();
            }
        }

        private void AddItem()
        {
            var key = ReadNonEmpty("Enter item key (e.g., SKU): ");
            var name = ReadNonEmpty("Enter item name: ");
            var category = ReadOptional("Enter category (optional): ", fallback: "Uncategorized");
            var qty = ReadPositiveInt("Enter initial quantity (>=0): ", allowZero: true);

            var item = new InventoryItem(key, name, qty, category);
            _inventory.Add(item);
            Console.WriteLine("Item added successfully.");
        }

        private void ReceiveStock()
        {
            var key = ReadNonEmpty("Enter item key: ");
            var qty = ReadPositiveInt("Enter quantity to receive: ");
            _inventory.Receive(key, qty);
            Console.WriteLine("Stock received successfully.");
        }

        private void ShipStock()
        {
            var key = ReadNonEmpty("Enter item key: ");
            var qty = ReadPositiveInt("Enter quantity to ship: ");
            _inventory.Ship(key, qty);
            Console.WriteLine("Stock shipped successfully.");
        }

        private void ViewItem()
        {
            var key = ReadNonEmpty("Enter item key: ");
            var item = _inventory.Get(key);
            Console.WriteLine(item);
        }

        private void ListAll()
        {
            var items = _inventory.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("No items in inventory.");
                return;
            }

            Console.WriteLine("Key | Name | Qty | Category");
            Console.WriteLine("-----------------------------------------");
            foreach (var it in items)
            {
                Console.WriteLine(it);
            }
        }

        private void RemoveItem()
        {
            var key = ReadNonEmpty("Enter item key to remove: ");
            _inventory.Remove(key);
            Console.WriteLine("Item removed.");
        }

        // ====== Helpers ======
        private static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input)) return input;
                Console.WriteLine("Value cannot be empty.");
            }
        }

        private static string ReadOptional(string prompt, string fallback)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? fallback : input;
        }

        private static int ReadPositiveInt(string prompt, bool allowZero = false)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim();
                if (int.TryParse(s, out var value))
                {
                    if (allowZero && value == 0) return value;
                    if (value > 0) return value;
                }
                Console.WriteLine(allowZero
                    ? "Invalid quantity. Enter 0 or a positive integer."
                    : "Invalid quantity. Enter a positive integer.");
            }
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void PrintError(string message)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = prev;
        }
    }
}
