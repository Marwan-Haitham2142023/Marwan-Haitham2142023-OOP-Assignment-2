namespace SrpLab;

public sealed class CheckoutFulfillment
{
    public string WriteGiftCard(string fromName, List<BasketLine> lines, decimal grandTotal)
    {
        List<string> skus = new List<string>();
        foreach (var line in lines) skus.Add(line.Sku);

        string items = string.Join(", ", skus);
        return "Dear friend,\nA gift from " + fromName + " awaits (" + items +
               ").\nTotal surprise value: " + grandTotal.ToString("C") + "\n";
    }

    public string Authorize(decimal grandTotal, string cardLast4, int lineCount)
    {
        string payload = grandTotal.ToString("0.00") + "|" + cardLast4 + "|" + lineCount;
        int hash = payload.GetHashCode();
        if (hash < 0) hash = -hash;
        return "AUTH-" + hash.ToString("X8");
    }
}
