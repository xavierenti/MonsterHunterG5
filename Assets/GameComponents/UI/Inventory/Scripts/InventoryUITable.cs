using System.Collections.Generic;
using UnityEngine;

public class InventoryUITable : UITable<InventoryUICell>
{
    [Header("Setup MonsterUITable")]
    [SerializeField] private List<InventoryItem> _inventory;

    private void Start()
    {
        LoadInventory();
        base.Start();
    }

    public List<InventoryItem> GetInventory()
    {
        return _inventory;
    }

    public void SetInventory(List<InventoryItem> inventory)
    {
        _inventory = inventory;
    }

    public override int TotalCellsCount => _inventory.Count;

    public override void SetupCell(InventoryUICell cell)
    {
        int index = cell.Index;

        cell.ItemNameText.text = _inventory[index].Name;
        cell.ItemDescriptionText.text = _inventory[index].Description;
    }

    private void OnDisable()
    {
        _inventory.Clear();
    }

    private void LoadInventory()
    {
        _inventory = SystemManager.Instace.GetInventory();
    }
}
