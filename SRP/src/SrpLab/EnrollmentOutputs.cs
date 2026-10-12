namespace SrpLab;

public sealed class EnrollmentOutputs
{
    private const decimal VatRate = 0.14m;

    public string WriteWelcomePacket(string courseCode, string studentName, bool isSeated, int waitlistPosition)
    {
        string status;
        if (isSeated) status = "confirmed seat";
        else status = "waitlist #" + waitlistPosition;

        return "# Welcome to " + courseCode + "\nHi " + studentName +
               ",\nYour status: **" + status + "**.\n" +
               "Bring a laptop. Discord onboarding link: https://example.invalid/" +
               courseCode.ToLower() + "\n";
    }

    public string FormatInvoiceLine(string courseCode, decimal tuition, bool isSeated)
    {
        if (!isSeated) return courseCode + ",WAITLIST,0.00";

        decimal vat = Math.Round(tuition * VatRate, 2);
        decimal total = tuition + vat;
        return courseCode + ",TUITION," + tuition.ToString("0.00") +
               ",VAT," + vat.ToString("0.00") +
               ",TOTAL," + total.ToString("0.00");
    }
}
