namespace SrpLab;

public sealed class TicketPriorityClassifier
{
    public TicketPriority Classify(SupportTicket t)
    {
        string blob = (t.GetSubject() + " " + t.GetBody()).ToLower();

        if (blob.Contains("down") || blob.Contains("outage") || blob.Contains("cannot login"))
            return TicketPriority.P1;

        if (blob.Contains("urgent") || blob.Contains("asap") || blob.Contains("blocked"))
            return TicketPriority.P2;

        return TicketPriority.P3;
    }
}
