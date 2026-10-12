namespace SrpLab;

public sealed class LoanApplication
{
    public decimal RequestedAmount;
    public int CreditScore;
    public int EmploymentMonths;
    public bool HasCollateral;

    public LoanApplication(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }
}
