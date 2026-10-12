namespace SrpLab;

public sealed class KitchenOperations
{
    private const int AllergyProtocolDelayMinutes = 3;
    private const int SlowThresholdMinutes = 20;

    public int EstimateReadyMinutes(List<KitchenOrderItem> items, int openStations, bool hasAllergens)
    {
        if (openStations <= 0) openStations = 1;

        int sequential = 0;
        int longest = 0;
        if (items.Count > 0) longest = items[0].PrepMinutes;

        foreach (var item in items)
        {
            sequential += item.PrepMinutes;
            if (item.PrepMinutes > longest) longest = item.PrepMinutes;
        }

        int parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (hasAllergens) parallel += AllergyProtocolDelayMinutes;

        if (parallel > longest) return parallel;
        return longest;
    }

    public string SelectLane(bool hasAllergens, int etaMinutes)
    {
        if (hasAllergens) return "LANE-ALLERGY";
        if (etaMinutes > SlowThresholdMinutes) return "LANE-SLOW";
        return "LANE-FAST";
    }
}
