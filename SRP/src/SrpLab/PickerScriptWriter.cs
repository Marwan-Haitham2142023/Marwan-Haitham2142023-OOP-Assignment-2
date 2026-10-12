namespace SrpLab;

public sealed class PickerScriptWriter
{
    public string Write(List<PickStop> stops, List<StockAllocation> allocations)
    {
        List<string> steps = new List<string>();
        for (int i = 0; i < stops.Count; i++)
        {
            PickStop s = stops[i];
            steps.Add((i + 1) + ". Go aisle " + s.Aisle + " bin " + s.Bin +
                      ": pick " + s.Qty + " × " + s.Sku);
        }

        List<string> shortSkus = new List<string>();
        foreach (var a in allocations)
            if (a.IsShort()) shortSkus.Add(a.Sku);

        string warn;
        if (shortSkus.Count > 0) warn = "SHORTAGES: " + string.Join(", ", shortSkus);
        else warn = "SHORTAGES: none";

        return string.Join("\n", steps) + "\n" + warn;
    }
}
