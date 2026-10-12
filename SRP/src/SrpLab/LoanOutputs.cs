namespace SrpLab;

public sealed class LoanOutputs
{
    public List<string> BuildChecklist(LoanApplication a, bool isEligible)
    {
        List<string> docs = new List<string>();
        docs.Add("National ID");
        docs.Add("Proof of income (3 months)");

        if (a.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (a.HasCollateral) docs.Add("Collateral ownership deed");
        if (a.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!isEligible) docs.Add("Manual underwriter referral form");

        return docs;
    }

    public string WriteDecisionLetter(string name, decimal amount, decimal risk, bool eligible, List<string> docs)
    {
        if (eligible)
        {
            return "Dear " + name + ",\nYour request for " + amount.ToString("C") +
                   " is pre-approved (risk " + risk.ToString("0") + ").\n" +
                   "Please upload: " + string.Join("; ", docs) + ".\n";
        }

        return "Dear " + name + ",\nWe are unable to approve " + amount.ToString("C") +
               " at this time.\nReference risk=" + risk.ToString("0") +
               ". You may reapply after improving documentation.\n";
    }

    public string WriteCsvRow(string appId, LoanApplication a, decimal risk, bool eligible)
    {
        string collateral = a.HasCollateral ? "1" : "0";
        string elig = eligible ? "Y" : "N";
        return appId + "," + a.CreditScore + "," + a.EmploymentMonths + "," +
               collateral + "," + risk.ToString("0.00") + "," + elig;
    }
}
