using System;
using System.Collections.Generic;

namespace Q1_FinanceManagement
{
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new();

        public void Run()
        {
            Console.WriteLine("=== Finance Management System (Q1) ===");

            // Create account
            var account = new SavingsAccount(accountNumber: "SA-001", initialBalance: 1000m);
            Console.WriteLine($"Account created. Number: {account.AccountNumber}, Balance: {account.Balance:C}\n");

            bool running = true;
            int transactionId = 1;

            while (running)
            {
                Console.WriteLine("\n--- New Transaction ---");

                // Get transaction details with validation
                DateTime date = DateTime.Now;
                decimal amount = GetPositiveDecimal("Enter transaction amount: ");
                string category = GetNonEmptyString("Enter category (e.g., Groceries, Utilities): ");

                // Choose processor type
                Console.WriteLine("\nChoose processor type:");
                Console.WriteLine("1. Mobile Money");
                Console.WriteLine("2. Bank Transfer");
                Console.WriteLine("3. Crypto Wallet");

                ITransactionProcessor processor = null;
                while (processor == null)
                {
                    Console.Write("Enter choice (1-3): ");
                    string procChoice = Console.ReadLine()?.Trim();

                    processor = procChoice switch
                    {
                        "1" => new MobileMoneyProcessor(),
                        "2" => new BankTransferProcessor(),
                        "3" => new CryptoWalletProcessor(),
                        _ => null
                    };

                    if (processor == null)
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                }

                // Create and process transaction
                var transaction = new Transaction(transactionId++, date, amount, category);
                processor.Process(transaction);

                // Apply to account
                account.ApplyTransaction(transaction);

                // Add to log
                _transactions.Add(transaction);

                // Ask if user wants to continue
                Console.Write("\nDo you want to add another transaction? (y/n): ");
                string answer = Console.ReadLine()?.Trim().ToLower();
                if (answer == "n" || answer == "no")
                {
                    running = false;
                }
            }

            // Summary
            PrintSummary(account);
        }

        private void PrintSummary(SavingsAccount account)
        {
            Console.WriteLine("\n=== Summary ===");
            Console.WriteLine($"Account: {account.AccountNumber}");
            Console.WriteLine($"Final Balance: {account.Balance:C}");
            Console.WriteLine("Transactions:");
            foreach (var tx in _transactions)
            {
                Console.WriteLine($"  #{tx.Id} | {tx.Date:d} | {tx.Category} | {tx.Amount:C}");
            }
            Console.WriteLine("=== End ===");
        }

        // Validation helpers
        private decimal GetPositiveDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value) && value > 0)
                    return value;
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private string GetNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                    return input;
                Console.WriteLine("Value cannot be empty.");
            }
        }
    }
}
