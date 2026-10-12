namespace SrpLab;

public sealed class WarehousePlanning
{
    public List<StockAllocation> Allocate(List<PickRequirement> reqs)
    {
        List<StockAllocation> result = new List<StockAllocation>();
        foreach (var r in reqs)
        {
            int alloc = r.QtyOnHand;
            if (r.QtyNeeded < alloc) alloc = r.QtyNeeded;
            result.Add(new StockAllocation(r.Sku, r.QtyNeeded, alloc));
        }
        return result;
    }

    public List<PickStop> PlanPath(List<PickRequirement> reqs)
    {
        List<PickStop> stops = new List<PickStop>();

        foreach (var r in reqs)
        {
            int qty = r.QtyOnHand;
            if (r.QtyNeeded < qty) qty = r.QtyNeeded;
            if (qty <= 0) continue;

            PickStop stop = new PickStop(r.Aisle, r.Bin, r.Sku, qty);

            int index = stops.Count;
            while (index > 0 && Compare(stops[index - 1], stop) > 0)
                index--;
            stops.Insert(index, stop);
        }
        return stops;
    }

    private static int Compare(PickStop a, PickStop b)
    {
        int byAisle = string.Compare(a.Aisle, b.Aisle);
        if (byAisle != 0) return byAisle;
        return a.Bin.CompareTo(b.Bin);
    }
}
