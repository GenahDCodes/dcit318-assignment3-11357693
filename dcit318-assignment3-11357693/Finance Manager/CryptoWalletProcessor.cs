using System;

namespace Q1_FinanceManagement;

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[CryptoWallet] Processing {transaction.Amount:C} for '{transaction.Category}' on {transaction.Date:d}.");
    }
}
 