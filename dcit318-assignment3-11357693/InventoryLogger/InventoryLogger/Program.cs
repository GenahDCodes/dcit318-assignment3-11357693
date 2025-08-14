namespace Q5_InventoryLogger
{
    public class Program
    {
        public static void Main()
        {
            var app = new InventoryApp();

            // 1) Seed sample data
            app.SeedSampleData();

            // 2) Save to disk
            app.SaveData();

            // 3) Clear memory / simulate a new run
            app.ResetSession();

            // 4) Load from disk
            app.LoadData();

            // 5) Print to confirm it was recovered
            app.PrintAllItems();

            System.Console.WriteLine("\nPress Enter to exit...");
            System.Console.ReadLine();
        }
    }
}
