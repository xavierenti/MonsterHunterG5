using System;
using System.Collections.Generic;

[Serializable]
public class Monster
{
    public int id;
    public string name;
    public string type;
    public string species;
    public string description;
    public List<Ailment> ailments;
    public List<Location> locations;
    public List<Resistance> resistances;
    public List<Weakness> weaknesses;
    public List<Reward> rewards;

    [Serializable]
    public class Ailment
    {
        public int id;
        public string name;
        public string description;
        public Protection protection;

        [Serializable]
        public class Protection
        {

            [Serializable]
            public class Skill
            {
                public int id;
                public string name;
                public string description;
            }
        }
    }

    [Serializable]
    public class Location
    {
        public int id;
        public int zoneCount;
        public string name;
    }

    [Serializable]
    public class Resistance
    {
        public string element;
    }

    [Serializable]
    public class Weakness
    {
        public string element;
        public int stars;
    }

    [Serializable]
    public class Reward
    {
        public int id;
        public Item item;
        public List<Condition> conditions;

        [Serializable]
        public class Item
        {
            public int id;
            public string name;
            public string description;
            public int rarity;
            public int carryLimit;
            public int value;
        }

        [Serializable]
        public class Condition
        {
            public string type;
            public string subtype;
            public string rank;
            public int quantity;
            public int chance;
        }
    }
}
