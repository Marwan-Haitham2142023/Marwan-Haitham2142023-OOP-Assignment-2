namespace SrpLab;

public sealed class CartPrice
{
    public decimal SubTotal;
    public decimal Discount;
    public decimal GiftWrapFee;
    public decimal GrandTotal;

    public CartPrice(decimal subTotal, decimal discount, decimal giftWrapFee, decimal grandTotal)
    {
        SubTotal = subTotal;
        Discount = discount;
        GiftWrapFee = giftWrapFee;
        GrandTotal = grandTotal;
    }
}
