using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUICell : UICell
{
    [Header("Setup Monster UICell")]
    [SerializeField] public Image MonsterBackgroundImage;
    [SerializeField] public SVGImage MonsterSvgImage;
    [SerializeField] public TextMeshProUGUI MonsterName;
    [SerializeField] public TextMeshProUGUI MonsterType;
    [SerializeField] public TextMeshProUGUI MonsterSpecie;

    public int MonsterId { get => _monsterId; set => _monsterId = value; }
    private int _monsterId;
}
