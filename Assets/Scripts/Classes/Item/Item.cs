using System;

[Serializable]
public class Item
{
    public int id;
    public string name;
    public string description;
    public int rarity;
    public int carryLimit;
    public int value;

    public static implicit operator Item(Monster.Reward.Item v)
    {
        Item tmp = new();

        tmp.id = v.id;
        tmp.name = v.name;
        tmp.description = v.description;
        tmp.rarity = v.rarity;
        tmp.carryLimit = v.carryLimit;
        tmp.value = v.value;

        return tmp;
    }
}
