namespace SrpLab;

public sealed class BillingOperations
{
    private int _sequence = 1000;
    private int _failedPaymentCount = 0;

    public int GetFailedPaymentCount() { return _failedPaymentCount; }

    public string NextInvoiceNumber(DateOnly periodStart)
    {
        _sequence++;
        string n = _sequence.ToString("D5");
        return "INV-" + periodStart.ToString("yyyyMM") + "-" + n;
    }

    public void RegisterFailedPayment()
    {
        _failedPaymentCount++;
    }

    public string WriteDunningEmail(string customerName, DateOnly asOf, decimal amount, string invoiceNumber)
    {
        string severity;
        if (_failedPaymentCount <= 1) severity = "friendly reminder";
        else if (_failedPaymentCount == 2) severity = "second notice";
        else severity = "final notice before suspension";

        return "Subject: " + severity + " " + invoiceNumber + "\nHi " + customerName +
               ",\nBalance " + amount.ToString("C") + " as of " + asOf.ToString("o") +
               " (" + _failedPaymentCount + " failures).\n";
    }

    public string WriteLedgerLine(string customerId, string invoiceNumber, decimal amount)
    {
        return customerId + "," + invoiceNumber + "," + amount.ToString("0.00") + ",AR-SUB";
    }
}
