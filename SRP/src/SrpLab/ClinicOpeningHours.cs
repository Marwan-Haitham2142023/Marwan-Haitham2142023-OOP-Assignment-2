namespace SrpLab;

public sealed class ClinicOpeningHours
{
    private TimeOnly _open;
    private TimeOnly _close;
    private int _slotMinutes;

    public ClinicOpeningHours(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        _open = open;
        _close = close;
        _slotMinutes = slotMinutes;
    }

    public TimeOnly GetOpen() { return _open; }
    public TimeOnly GetClose() { return _close; }
    public int GetSlotMinutes() { return _slotMinutes; }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        if (when.DayOfWeek == DayOfWeek.Friday || when.DayOfWeek == DayOfWeek.Saturday)
            return false;

        TimeOnly t = TimeOnly.FromDateTime(when.DateTime);
        if (t < _open) return false;
        if (t.AddMinutes(_slotMinutes) > _close) return false;
        return true;
    }
}
