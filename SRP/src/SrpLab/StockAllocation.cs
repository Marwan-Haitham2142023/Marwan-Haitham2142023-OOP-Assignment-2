namespace SrpLab;

public sealed class StockAllocation
{
    public string Sku;
    public int QtyNeeded;
    public int Allocated;

    public StockAllocation(string sku, int qtyNeeded, int allocated)
    {
        Sku = sku;
        QtyNeeded = qtyNeeded;
        Allocated = allocated;
    }

    public bool IsShort()
    {
        return Allocated < QtyNeeded;
    }
}
