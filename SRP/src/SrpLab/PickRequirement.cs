namespace SrpLab;

public sealed class PickRequirement
{
    public string Sku;
    public string Aisle;
    public int Bin;
    public int QtyNeeded;
    public int QtyOnHand;

    public PickRequirement(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        Sku = sku;
        Aisle = aisle;
        Bin = bin;
        QtyNeeded = qtyNeeded;
        QtyOnHand = qtyOnHand;
    }
}
