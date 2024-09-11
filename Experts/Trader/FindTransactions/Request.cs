﻿namespace Experts.Trader.FindTransactions;

public class Request
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string UserId { get; set; }
}
