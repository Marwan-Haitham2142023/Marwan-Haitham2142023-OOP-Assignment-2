namespace SrpLab;

public sealed class CartPricer
{
    private const decimal GiftWrapFee = 4.99m;
    private CouponDiscountCalculator _coupons;

    public CartPricer(CouponDiscountCalculator coupons)
    {
        _coupons = coupons;
    }

    public CartPrice Price(ShoppingCart cart)
    {
        decimal subTotal = cart.SubTotal();
        decimal discount = _coupons.Calculate(cart.GetCouponText(), subTotal);
        decimal giftWrapFee = 0m;
        if (cart.IsGiftWrapRequested()) giftWrapFee = GiftWrapFee;

        decimal grandTotal = subTotal - discount + giftWrapFee;
        if (grandTotal < 0m) grandTotal = 0m;

        return new CartPrice(subTotal, discount, giftWrapFee, grandTotal);
    }
}
