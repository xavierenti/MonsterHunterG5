using System.Collections.Generic;
using UnityEngine;

public class ElemetsTypes : MonoBehaviour
{
    public static Dictionary<string, Color> MonsterElementTypeDict = new() {
        { "fire", new Color(234f / 255f, 32f / 255f, 25f / 255f)},
        { "water", new Color(22f / 255f, 217f / 255f, 223f / 255f) },
        { "ice", new Color(138f / 255f, 194f / 255f, 106f / 255f) },
        { "thunder", new Color(191f / 255f, 138f / 255f, 9f / 255f) },
        { "dragon", new Color(128f / 255f, 9f / 255f, 191f / 255f) },
        { "blast", new Color(254f / 255f, 45f / 255f, 245f / 255f) },
        { "poison", new Color(29f / 255f, 203f / 255f, 74f / 255f) },
        { "sleep", new Color(189f / 255f, 33f / 255f, 75f / 255f) },
        { "paralysis", new Color(151f / 255f, 159f / 255f, 55f / 255f) },
        { "stun", new Color(147f / 255f, 114f / 255f, 169f / 255f) },
    };

    public static Dictionary<string, string> HexColorMonsterElementTypeDict = new() {
        { "fire", "#EA2019"},
        { "water", "#2B38B2" },
        { "ice", "#16D9DF" },
        { "thunder", "#BF8A09" },
        { "dragon", "#8009BF" },
        { "blast", "#FE2DF5" },
        { "poison", "#1DCB4A" },
        { "sleep", "#BD214B" },
        { "paralysis", "#979F37" },
        { "stun", "#9372A9" },
    };
}
