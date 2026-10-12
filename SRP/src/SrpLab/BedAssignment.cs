namespace SrpLab;

public sealed class BedAssignment
{
    public int Bed;
    public string PatientId;
    public int Acuity;

    public BedAssignment(int bed, string patientId, int acuity)
    {
        Bed = bed;
        PatientId = patientId;
        Acuity = acuity;
    }
}
