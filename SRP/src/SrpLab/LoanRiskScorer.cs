namespace SrpLab;

public sealed class LoanRiskScorer
{
    public decimal Score(LoanApplication a)
    {
        decimal score = 100m;
        int diff = 700 - a.CreditScore;
        if (diff < 0) diff = 0;
        score -= diff * 0.15m;

        if (a.EmploymentMonths < 6) score -= 20m;
        if (a.RequestedAmount > 50_000m && !a.HasCollateral) score -= 25m;
        if (a.RequestedAmount > 150_000m) score -= 10m;

        if (score < 0m) score = 0m;
        if (score > 100m) score = 100m;
        return score;
    }
}
