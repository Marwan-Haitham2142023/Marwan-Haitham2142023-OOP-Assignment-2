namespace SrpLab;

public sealed class CourseSeating
{
    private HashSet<string> _seated;
    private List<string> _waitlist;
    private int _capacity;

    public CourseSeating(int capacity)
    {
        _capacity = capacity;
        _seated = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _waitlist = new List<string>();
    }

    public int GetCapacity() { return _capacity; }

    public string Register(string studentEmail)
    {
        if (studentEmail == null || studentEmail.Trim() == "")
            throw new ArgumentException("email");

        string email = studentEmail.Trim();

        if (_seated.Contains(email)) return "ALREADY_REGISTERED";
        foreach (var w in _waitlist)
            if (w.Equals(email, StringComparison.OrdinalIgnoreCase)) return "ALREADY_REGISTERED";

        if (_seated.Count < _capacity)
        {
            _seated.Add(email);
            return "SEATED";
        }

        _waitlist.Add(email);
        return "WAITLIST:" + _waitlist.Count;
    }

    public bool IsSeated(string studentEmail)
    {
        return _seated.Contains(studentEmail);
    }

    public int WaitlistPosition(string studentEmail)
    {
        for (int i = 0; i < _waitlist.Count; i++)
        {
            if (_waitlist[i].Equals(studentEmail, StringComparison.OrdinalIgnoreCase))
                return i + 1;
        }
        return -1;
    }

    public void PromoteFromWaitlist(int seats)
    {
        while (seats > 0 && _waitlist.Count > 0 && _seated.Count < _capacity)
        {
            string next = _waitlist[0];
            _waitlist.RemoveAt(0);
            _seated.Add(next);
            seats--;
        }
    }
}
