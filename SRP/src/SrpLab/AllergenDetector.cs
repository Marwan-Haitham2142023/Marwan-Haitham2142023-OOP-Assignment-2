namespace SrpLab;

public sealed class AllergenDetector
{
    public List<string> Detect(List<KitchenOrderItem> items)
    {
        HashSet<string> hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var item in items)
        {
            foreach (var ing in item.Ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter"))
                    hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread"))
                    hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew"))
                    hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab"))
                    hits.Add("shellfish");
            }
        }

        List<string> result = new List<string>();
        foreach (var h in hits) result.Add(h);
        result.Sort();
        return result;
    }
}
