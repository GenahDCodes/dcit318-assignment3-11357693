using System;

namespace Q1_FinanceManagement;

public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");

        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    // Deducts the amount by default (can be overridden)
    public virtual void ApplyTransaction(Transaction transaction)
    {
        if (transaction is null) throw new ArgumentNullException(nameof(transaction));
        if (transaction.Amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(transaction.Amount), "Transaction amount must be positive.");

        Balance -= transaction.Amount;
        Console.WriteLine($"[Account] New balance: {Balance:C}");
    }
}
