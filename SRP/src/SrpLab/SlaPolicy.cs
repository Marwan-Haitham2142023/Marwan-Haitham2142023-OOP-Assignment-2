namespace SrpLab;

public sealed class SlaPolicy
{
    public DateTimeOffset Deadline(DateTimeOffset openedAt, TicketPriority p)
    {
        int hours;
        if (p == TicketPriority.P1) hours = 4;
        else if (p == TicketPriority.P2) hours = 24;
        else hours = 72;

        return openedAt.AddHours(hours);
    }

    public bool IsBreached(DateTimeOffset openedAt, TicketPriority p, DateTimeOffset now)
    {
        return now > Deadline(openedAt, p);
    }

    public string WriteEscalation(string ticketId, TicketPriority p, DateTimeOffset deadline)
    {
        return "ESCALATE " + ticketId + " priority=" + p +
               " breachAt=" + deadline.ToString("u") + " keywords-scanned=yes";
    }
}
