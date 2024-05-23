using System.Collections.Generic;
using UnityEngine;

public class ColorResistancesTypes : MonoBehaviour
{
    public static Dictionary<string, Color> ArmorResistanceDict = new() {
        { "fire", new Color(234f / 255f, 32f / 255f, 25f / 255f)},
        { "water", new Color(22f / 255f, 217f / 255f, 223f / 255f) },
        { "ice", new Color(138f / 255f, 194f / 255f, 106f / 255f) },
        { "thunder", new Color(191f / 255f, 138f / 255f, 9f / 255f) },
        { "dragon", new Color(128f / 255f, 9f / 255f, 191f / 255f) },
    };

    public static Dictionary<string, string> HexColorArmorResistanceDict = new() {
        { "fire", "#EA2019"},
        { "water", "#2B38B2" },
        { "ice", "#16D9DF" },
        { "thunder", "#BF8A09" },
        { "dragon", "#8009BF" },
    };
}
