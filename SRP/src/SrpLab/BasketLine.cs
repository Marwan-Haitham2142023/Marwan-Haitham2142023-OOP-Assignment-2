namespace SrpLab;

public sealed class BasketLine
{
    public string Sku;
    public decimal Price;
    public int Qty;

    public BasketLine(string sku, decimal price, int qty)
    {
        Sku = sku;
        Price = price;
        Qty = qty;
    }
}
