namespace SrpLab;

public sealed class WardBedRegistry
{
    private Dictionary<int, BedAssignment> _beds = new Dictionary<int, BedAssignment>();

    public void Assign(int bed, string patientId, int acuity)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException("bed");
        if (patientId == null || patientId.Trim() == "") throw new ArgumentException("patient required");
        _beds[bed] = new BedAssignment(bed, patientId.Trim().ToUpper(), acuity);
    }

    public BedAssignment Find(int bed)
    {
        if (_beds.ContainsKey(bed)) return _beds[bed];
        return null;
    }

    public List<BedAssignment> All()
    {
        List<BedAssignment> all = new List<BedAssignment>();
        foreach (var pair in _beds) all.Add(pair.Value);
        all.Sort(CompareByBed);
        return all;
    }

    private static int CompareByBed(BedAssignment a, BedAssignment b)
    {
        return a.Bed.CompareTo(b.Bed);
    }
}
