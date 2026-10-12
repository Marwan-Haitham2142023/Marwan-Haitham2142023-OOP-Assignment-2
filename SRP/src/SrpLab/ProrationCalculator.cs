namespace SrpLab;

public sealed class ProrationCalculator
{
    public decimal Prorate(Subscription s, DateOnly activeFrom)
    {
        if (activeFrom <= s.PeriodStart) return s.MonthlyPrice;
        if (activeFrom >= s.PeriodEnd) return 0m;

        int totalDays = s.PeriodEnd.DayNumber - s.PeriodStart.DayNumber;
        if (totalDays <= 0) return s.MonthlyPrice;

        int used = s.PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(s.MonthlyPrice * used / totalDays, 2);
    }
}
