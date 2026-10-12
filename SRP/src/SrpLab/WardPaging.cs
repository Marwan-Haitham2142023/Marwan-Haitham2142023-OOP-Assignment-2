namespace SrpLab;

public sealed class WardPaging
{
    private int _yellowThreshold = 8;
    private List<string> _entries = new List<string>();

    public string DecideCode(int acuity)
    {
        if (acuity >= _yellowThreshold) return "CODE-YELLOW";
        return null;
    }

    public void Record(string code, int bed, DateTime atUtc)
    {
        _entries.Add(code + " bed=" + bed + " at " + atUtc.ToString("HH:mm"));
    }

    public List<string> Drain()
    {
        List<string> copy = new List<string>();
        foreach (var e in _entries) copy.Add(e);
        _entries.Clear();
        return copy;
    }

    public string WriteHandoff(int bed, BedAssignment assignment, DateTime nowUtc)
    {
        if (assignment == null) return "Bed " + bed + ": empty";
        int acuity = assignment.Acuity;
        string tone;
        if (acuity >= 8) tone = "ESCALATE";
        else if (acuity >= 4) tone = "WATCH";
        else tone = "STABLE";
        return "[HANDOFF " + nowUtc.ToString("yyyy-MM-dd") + "] Bed " + bed + " · " +
               assignment.PatientId + " · acuity=" + acuity + " · " + tone;
    }

    public string ExportCensus(List<BedAssignment> assignments)
    {
        List<BedAssignment> sorted = new List<BedAssignment>();
        foreach (var a in assignments) sorted.Add(a);
        sorted.Sort(CompareByBed);

        List<string> lines = new List<string>();
        lines.Add("bed,patient,acuity");
        foreach (var a in sorted)
            lines.Add(a.Bed + "," + a.PatientId + "," + a.Acuity);

        return string.Join("\n", lines);
    }

    private static int CompareByBed(BedAssignment a, BedAssignment b)
    {
        return a.Bed.CompareTo(b.Bed);
    }
}
