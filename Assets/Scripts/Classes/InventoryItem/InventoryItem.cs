public class InventoryItem
{
    public enum Types { ITEM, ARMOR }

    public int Id;
    public string Name;
    public string Description;
    public Types Type;

    public static implicit operator InventoryItem(Item v)
    {
        InventoryItem tmp = new();

        tmp.Id = v.id;
        tmp.Name = v.name;
        tmp.Description = v.description;
        tmp.Type = Types.ITEM;

        return tmp;
    }

    public static implicit operator InventoryItem(Armour v)
    {
        InventoryItem tmp = new();

        tmp.Id = v.id;
        tmp.Name = v.name;
        tmp.Description = $"{v.type.ToUpper()} armor. Rank {v.rank.ToUpper()}. Rarity {v.rarity}";
        tmp.Type = Types.ARMOR;

        return tmp;
    }
}
