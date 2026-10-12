namespace SrpLab;

public sealed class PickList
{
    private List<PickRequirement> _requirements = new List<PickRequirement>();

    public List<PickRequirement> GetRequirements() { return _requirements; }

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _requirements.Add(new PickRequirement(sku, aisle, bin, qtyNeeded, qtyOnHand));
    }
}
