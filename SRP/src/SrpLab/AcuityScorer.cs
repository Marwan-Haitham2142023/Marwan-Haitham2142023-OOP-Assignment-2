namespace SrpLab;

public sealed class AcuityScorer
{
    public int Score(int heartRate, int spo2)
    {
        int score = 0;
        if (heartRate > 120 || heartRate < 45) score += 4;
        else if (heartRate > 100) score += 2;
        if (spo2 < 90) score += 5;
        else if (spo2 < 94) score += 2;
        if (score > 10) score = 10;
        return score;
    }
}
