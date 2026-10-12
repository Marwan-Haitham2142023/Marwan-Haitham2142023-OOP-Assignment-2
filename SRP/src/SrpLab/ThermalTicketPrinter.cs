namespace SrpLab;

public sealed class ThermalTicketPrinter
{
    private const int Width = 32;

    public string Render(int orderNumber, List<KitchenOrderItem> items, List<string> allergens, int etaMinutes)
    {
        string line = new string('=', Width);

        List<string> itemLines = new List<string>();
        foreach (var item in items)
            itemLines.Add("* " + item.Item.ToUpper() + " (" + item.PrepMinutes + "m)");
        string body = string.Join("\n", itemLines);

        string allergyLine;
        if (allergens.Count == 0) allergyLine = "ALLERGENS: none";
        else allergyLine = "ALLERGENS: " + string.Join(",", allergens);

        return line + "\nORDER #" + orderNumber + "\nETA " + etaMinutes +
               " MIN\n" + body + "\n" + allergyLine + "\n" + line + "\n";
    }
}
