namespace SrpLab;

public sealed class Subscription
{
    public string CustomerId;
    public decimal MonthlyPrice;
    public DateOnly PeriodStart;
    public DateOnly PeriodEnd;

    public Subscription(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }
}
