namespace SrpLab;

public sealed class CouponDiscountCalculator
{
    public decimal Calculate(string couponText, decimal subTotal)
    {
        if (couponText == null || couponText.Trim() == "") return 0m;
        string t = couponText.Trim().ToUpper();

        if (t.StartsWith("SAVE"))
        {
            string rest = t.Substring(4);
            int pct;
            if (int.TryParse(rest, out pct) && pct > 0 && pct <= 50)
                return Math.Round(subTotal * pct / 100m, 2);
        }

        if (t.Contains("FREESHIP")) return 0m;
        if (t == "WELCOME10")
        {
            if (subTotal < 10m) return subTotal;
            return 10m;
        }
        return 0m;
    }
}
