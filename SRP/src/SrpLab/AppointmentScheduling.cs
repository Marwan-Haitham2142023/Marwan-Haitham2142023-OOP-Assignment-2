namespace SrpLab;

public sealed class AppointmentScheduling
{
    private ClinicOpeningHours _hours;
    private AppointmentBook _book;

    public AppointmentScheduling(ClinicOpeningHours hours, AppointmentBook book)
    {
        _hours = hours;
        _book = book;
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        DateTimeOffset cursor = Align(from);
        DateTimeOffset end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_hours.IsWithinBusinessHours(cursor) && !_book.IsBooked(cursor))
                return cursor;
            cursor = cursor.AddMinutes(_hours.GetSlotMinutes());
        }
        return null;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        int slot = _hours.GetSlotMinutes();
        int minutes = from.Minute - (from.Minute % slot);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }

    public string WriteIcs(DateTimeOffset slot, string patientName, string clinician, Guid uid)
    {
        DateTimeOffset end = slot.AddMinutes(_hours.GetSlotMinutes());
        return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
               "UID:" + uid + "\n" +
               "DTSTART:" + slot.ToString("yyyyMMdd'T'HHmmss'Z'") + "\n" +
               "DTEND:" + end.ToString("yyyyMMdd'T'HHmmss'Z'") + "\n" +
               "SUMMARY:Visit " + patientName + " / " + clinician + "\n" +
               "END:VEVENT\nEND:VCALENDAR\n";
    }

    public string WriteSms(DateTimeOffset slot, string clinicPhone)
    {
        return "Reminder: appointment " + slot.ToString("MMM dd HH:mm") +
               ". Call " + clinicPhone + " to reschedule.";
    }
}
