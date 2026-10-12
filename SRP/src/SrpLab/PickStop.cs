namespace SrpLab;

public sealed class PickStop
{
    public string Aisle;
    public int Bin;
    public string Sku;
    public int Qty;

    public PickStop(string aisle, int bin, string sku, int qty)
    {
        Aisle = aisle;
        Bin = bin;
        Sku = sku;
        Qty = qty;
    }
}
