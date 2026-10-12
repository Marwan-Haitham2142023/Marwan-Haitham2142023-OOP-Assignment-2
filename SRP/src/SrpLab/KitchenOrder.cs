namespace SrpLab;

public sealed class KitchenOrder
{
    private List<KitchenOrderItem> _items = new List<KitchenOrderItem>();

    public List<KitchenOrderItem> GetItems() { return _items; }

    public void AddItem(string item, List<string> ingredients, int prepMinutes)
    {
        List<string> normalized = new List<string>();
        foreach (var i in ingredients)
            normalized.Add(i.Trim().ToLower());

        _items.Add(new KitchenOrderItem(item, normalized, prepMinutes));
    }
}
