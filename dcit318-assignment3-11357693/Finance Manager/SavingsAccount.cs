using System;

namespace Q1_FinanceManagement;

// Sealed to prevent further inheritance
public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance) { }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction is null) throw new ArgumentNullException(nameof(transaction));
        if (transaction.Amount <= 0)
        {
            Console.WriteLine("[SavingsAccount] Invalid transaction amount. Must be positive.");
            return;
        }

        if (transaction.Amount > Balance)
        {
            Console.WriteLine("[SavingsAccount] Insufficient funds.");
            return;
        }

        Balance -= transaction.Amount;
        Console.WriteLine($"[SavingsAccount] Deducted {transaction.Amount:C}. Updated balance: {Balance:C}");
    }
}
