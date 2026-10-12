namespace SrpLab;

public sealed class LoanEligibilityPolicy
{
    private LoanRiskScorer _scorer;

    public LoanEligibilityPolicy(LoanRiskScorer scorer)
    {
        _scorer = scorer;
    }

    public bool IsEligible(LoanApplication a)
    {
        return _scorer.Score(a) >= 55m && a.CreditScore >= 580;
    }
}
