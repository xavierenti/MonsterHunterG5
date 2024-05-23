using System.Collections.Generic;
using UnityEngine;

public class ColorArmorTypes : MonoBehaviour
{
    public static Dictionary<string, Color> ArmorTypeDict = new() {
        { "head", new Color(191f / 255f, 183f / 255f, 2f / 255f)},
        { "chest", new Color(195f / 255f, 121f / 255f, 26f / 255f) },
        { "gloves", new Color(138f / 255f, 194f / 255f, 106f / 255f) },
        { "waist", new Color(158f / 255f, 68f / 255f, 178f / 255f) },
        { "legs", new Color(43f / 255f, 56f / 255f, 178f / 255f) },
    };

    public static Dictionary<string, string> HexColorArmorTypeDict = new() {
        { "head", "#BFB702"},
        { "chest", "#C3791A" },
        { "gloves", "#8AC26A" },
        { "waist", "#9E44B2" },
        { "legs", "#2B38B2" },
    };
}
