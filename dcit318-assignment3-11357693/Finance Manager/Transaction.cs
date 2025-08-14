using System;

namespace Q1_FinanceManagement;

public record Transaction(
    int Id,
    DateTime Date,
    decimal Amount,
    string Category
);
