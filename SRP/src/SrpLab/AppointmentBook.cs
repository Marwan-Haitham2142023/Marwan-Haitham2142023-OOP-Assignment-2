namespace SrpLab;

public sealed class AppointmentBook
{
    private HashSet<DateTimeOffset> _booked = new HashSet<DateTimeOffset>();
    private ClinicOpeningHours _hours;

    public AppointmentBook(ClinicOpeningHours hours)
    {
        _hours = hours;
    }

    public bool IsBooked(DateTimeOffset slot)
    {
        return _booked.Contains(slot);
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_hours.IsWithinBusinessHours(slot)) return false;
        if (_booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }
}
