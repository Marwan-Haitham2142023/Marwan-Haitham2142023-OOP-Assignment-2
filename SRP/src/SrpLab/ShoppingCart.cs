namespace SrpLab;

public sealed class ShoppingCart
{
    private List<BasketLine> _lines = new List<BasketLine>();
    private string _couponText;
    private bool _giftWrapRequested;

    public List<BasketLine> GetLines()
    {
        return _lines;
    }

    public string GetCouponText() { return _couponText; }
    public bool IsGiftWrapRequested() { return _giftWrapRequested; }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException("qty");
        _lines.Add(new BasketLine(sku, price, qty));
    }

    public void ApplyCouponText(string t) { _couponText = t; }
    public void EnableGiftWrap() { _giftWrapRequested = true; }

    public decimal SubTotal()
    {
        decimal total = 0m;
        foreach (var line in _lines)
            total += line.Price * line.Qty;
        return total;
    }
}
