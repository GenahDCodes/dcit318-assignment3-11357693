using System;

namespace Q1_FinanceManagement;

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[MobileMoney] Processing {transaction.Amount:C} for '{transaction.Category}' on {transaction.Date:d}.");
    }
}
