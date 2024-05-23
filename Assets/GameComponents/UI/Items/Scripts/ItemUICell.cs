using TMPro;
using UnityEngine;

public class ItemUICell : UICell
{
    [Header("ItemUICell Setup")]
    [SerializeField] public TextMeshProUGUI ItemNameText;
    [SerializeField] public TextMeshProUGUI ItemDescriptionText;
    [SerializeField] public TextMeshProUGUI ItemValueText;
    [SerializeField] public TextMeshProUGUI ItemRarityText;
}
