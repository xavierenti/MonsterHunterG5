using System.Collections.Generic;
using UnityEngine;

public class ColorSpecies
{
    public static Dictionary<string, Color> ColorSpeciesDict = new() {
        { "bird_wyvern", new Color(147f / 255f, 243f / 255f, 112f / 255f)},
        { "brute_wyvern", new Color(142f / 255f, 100f / 255f, 59f / 255f) },
        { "elder_dragon", new Color(210f / 255f, 207f / 255f, 191f / 255f) },
        { "fanged_beast", new Color(104f / 255f, 164f / 255f, 108f / 255f) },
        { "fanged_wyvern", new Color(185f / 255f, 163f / 255f, 2f / 255f) },
        { "fish", new Color(35f / 255f, 150f / 255f, 235f / 255f) },
        { "flying_wyvern", new Color(173f / 255f, 213f / 255f, 214f / 255f) },
        { "herbivore", new Color(147f / 255f, 243f / 255f, 112f / 255f) },
        { "lynian", new Color(184f / 255f, 91f / 255f, 91f / 255f) },
        { "neopteron", new Color(224f / 255f, 119f / 255f, 229f / 255f) },
        { "piscine_wyvern", new Color(127f / 255f, 127f / 255f, 170f / 255f) },
        { "relict", new Color(35f / 255f, 250f / 255f, 215f / 255f) },
        { "wingdrake", new Color(234f / 255f, 214f / 255f, 161f / 255f) }
    };

    public static Dictionary<string, string> HexColorSpeciesDict = new() {
        { "bird_wyvern", "#93F370"},
        { "brute_wyvern", "#8E643B" },
        { "elder_dragon", "#D2CFBF" },
        { "fanged_beast", "#680002" },
        { "fanged_wyvern", "#B9A402" },
        { "fish", "#2396EB" },
        { "flying_wyvern", "#ADD5D6" },
        { "herbivore", "#ADD570" },
        { "lynian", "#B85B5B" },
        { "neopteron", "#E077E5" },
        { "piscine_wyvern", "#7F7FAA" },
        { "relict", "#23FAD7" },
        { "wingdrake", "#EAD6A1" }
    };
}
