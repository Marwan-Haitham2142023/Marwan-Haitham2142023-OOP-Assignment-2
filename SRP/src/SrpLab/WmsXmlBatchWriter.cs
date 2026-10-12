namespace SrpLab;

public sealed class WmsXmlBatchWriter
{
    public string Write(string batchId, List<StockAllocation> allocations)
    {
        List<string> parts = new List<string>();
        foreach (var a in allocations)
            parts.Add("<line sku=\"" + a.Sku + "\" qty=\"" + a.Allocated + "\" />");

        return "<batch id=\"" + batchId + "\">" + string.Join("", parts) + "</batch>";
    }
}
