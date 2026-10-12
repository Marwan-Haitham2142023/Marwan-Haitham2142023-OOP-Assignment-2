namespace SrpLab;

public sealed class GradingPolicy
{
    public string Letter(decimal avg)
    {
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        if (avg >= 60) return "D";
        return "F";
    }

    public bool QualifiesForHonorRoll(decimal avg, string letter)
    {
        if (avg >= 85 && (letter == "A" || letter == "B")) return true;
        return false;
    }
}
