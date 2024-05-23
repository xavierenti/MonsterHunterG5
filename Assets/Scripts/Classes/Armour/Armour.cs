using System.Collections.Generic;
using System;

[Serializable]
public class Armour
{
    public int id;
    public string name;
    public string type;
    public string rank;
    public int rarity;
    public Defense defense;
    public Resistance resistances;
    public List<Skills> skills;
    public Assets assets;
    public Crafting crafting;


    [Serializable]
    public class Defense
    {
        [Unity.Serialization.FormerName("base")]
        public int baseDefense;
        public int max;
        public int augmented;
    }

    [Serializable]
    public class Resistance
    {
        public int fire;
        public int water;
        public int ice;
        public int thunder;
        public int dragon;
    }

    [Serializable]
    public class Skills
    {
        public int id;
        public int level;
        public int description;
        public int skill;
        public string skillName;
    }

    [Serializable]
    public class Assets
    {
        public string imageMale;
        public string imageFemale;
    }

    [Serializable]
    public class Crafting
    {
        public List<Materials> materials;

        [Serializable]
        public class Materials
        {
            public int quantity;
            public Item item;

            [Serializable]
            public class Item
            {
                public int id;
                public string name;
                public string description;
                public int rarity;
                public int carryLimit;
                public int sellPrice;
                public int buyPrice;
            }
        }
    }
}
