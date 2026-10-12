namespace SrpLab;

public sealed class SupportTicket
{
    private string _id;
    private string _subject;
    private string _body;
    private DateTimeOffset _openedAt;

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        _id = id;
        _subject = subject;
        _body = body;
        _openedAt = openedAt;
    }

    public string GetId() { return _id; }
    public string GetSubject() { return _subject; }
    public string GetBody() { return _body; }
    public DateTimeOffset GetOpenedAt() { return _openedAt; }

    public void AppendCustomerMessage(string text)
    {
        _body = _body + "\n---\n" + text;
    }
}
