namespace SrpLab;

public sealed class GradingOutputs
{
    private GradingPolicy _policy;

    public GradingOutputs(GradingPolicy policy)
    {
        _policy = policy;
    }

    public string WriteTranscript(string studentId, string fullName, decimal avg)
    {
        string letter = _policy.Letter(avg);
        bool honor = _policy.QualifiesForHonorRoll(avg, letter);
        return "TRANSCRIPT\nStudent: " + fullName + " (" + studentId + ")\nAverage: " +
               avg + "\nLetter: " + letter + "\nHonor: " + honor + "\n";
    }

    public string ExportCsv(StudentScores scores)
    {
        List<string> rows = new List<string>();
        rows.Add("studentId,average,letter,honor");

        foreach (var id in scores.StudentIds())
        {
            decimal avg = scores.Average(id);
            string letter = _policy.Letter(avg);
            int honor = _policy.QualifiesForHonorRoll(avg, letter) ? 1 : 0;
            rows.Add(id + "," + avg + "," + letter + "," + honor);
        }

        return string.Join("\n", rows);
    }
}
