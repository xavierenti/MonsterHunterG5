using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFilter : Filter
{
    [Header("Setup")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Toggle _itemToogle;
    [SerializeField] private Toggle _armorToogle;
    [SerializeField] private InventoryUITable _table;

    private List<InventoryItem> _originalInventoryList;

    private void Start()
    {
        _originalInventoryList = _table.GetInventory();
    }

    public override void FilterData(string filter)
    {
        List<InventoryItem> filteredList = _originalInventoryList.FindAll((item) => {
            return CheckItem(item, filter);
        });

        _table.SetInventory(filteredList);
        _table.ReloadTable();
    }

    private bool CheckItem(InventoryItem item, string filter)
    {
        if (item.Type == InventoryItem.Types.ITEM && !_itemToogle.isOn)
        {
            return false;
        }

        if (item.Type == InventoryItem.Types.ARMOR && !_armorToogle.isOn)
        {
            return false;
        }

        return item.Name.ToLower().Contains(filter.ToLower());
    }
}
