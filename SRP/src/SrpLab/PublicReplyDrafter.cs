namespace SrpLab;

public sealed class PublicReplyDrafter
{
    public string Draft(string ticketId, TicketPriority p, string agentName, DateTimeOffset slaDeadline)
    {
        string apology;
        if (p == TicketPriority.P1)
            apology = "We are treating this as a critical incident.";
        else
            apology = "Thanks for reaching out.";

        return "Hi,\n" + apology + "\nTicket " + ticketId + " is with " + agentName +
               ". Next update before " + slaDeadline.ToString("u") + ".\n";
    }
}
