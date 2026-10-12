namespace SrpLab;

public sealed class StudentScores
{
    private Dictionary<string, List<decimal>> _scores =
        new Dictionary<string, List<decimal>>(StringComparer.OrdinalIgnoreCase);

    public void Record(string studentId, decimal score)
    {
        if (score < 0 || score > 100) throw new ArgumentOutOfRangeException("score");

        if (!_scores.ContainsKey(studentId))
            _scores[studentId] = new List<decimal>();

        _scores[studentId].Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.ContainsKey(studentId)) return 0m;
        List<decimal> list = _scores[studentId];
        if (list.Count == 0) return 0m;

        decimal sum = 0m;
        foreach (var s in list) sum += s;
        return Math.Round(sum / list.Count, 2);
    }

    public List<string> StudentIds()
    {
        List<string> ids = new List<string>();
        foreach (var pair in _scores) ids.Add(pair.Key);
        ids.Sort();
        return ids;
    }
}
