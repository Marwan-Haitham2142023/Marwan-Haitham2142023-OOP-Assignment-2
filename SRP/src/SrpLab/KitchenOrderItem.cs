namespace SrpLab;

public sealed class KitchenOrderItem
{
    public string Item;
    public List<string> Ingredients;
    public int PrepMinutes;

    public KitchenOrderItem(string item, List<string> ingredients, int prepMinutes)
    {
        Item = item;
        Ingredients = ingredients;
        PrepMinutes = prepMinutes;
    }
}
