namespace SrpLab;

public sealed class WardAdmissionDesk
{
    private AcuityScorer _scorer;
    private WardBedRegistry _registry;
    private WardPaging _paging;

    public WardAdmissionDesk(AcuityScorer scorer, WardBedRegistry registry, WardPaging paging)
    {
        _scorer = scorer;
        _registry = registry;
        _paging = paging;
    }

    public BedAssignment Admit(int bed, string patientId, int heartRate, int spo2, DateTime nowUtc)
    {
        int acuity = _scorer.Score(heartRate, spo2);
        _registry.Assign(bed, patientId, acuity);
        string code = _paging.DecideCode(acuity);
        if (code != null) _paging.Record(code, bed, nowUtc);
        return _registry.Find(bed);
    }
}
