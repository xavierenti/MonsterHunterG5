using System.Collections.Generic;
using UnityEngine;

public class ItemUITable : UITable<ItemUICell>
{
    [Header("Setup MonsterUITable")]
    [SerializeField] private List<Monster.Reward.Item> _items;

    new private void Start()
    {
        LoadItems();
        base.Start();
    }
    public override int TotalCellsCount => _items.Count;

    public override void SetupCell(ItemUICell cell)
    {
        int index = cell.Index;

        cell.ItemNameText.text = _items[index].name;
        cell.ItemDescriptionText.text = _items[index].description;
        cell.ItemValueText.text = _items[index].value + " Coins";
        cell.ItemRarityText.text = _items[index].rarity + " Rarity";
    }

    private void OnDisable()
    {
        _items.Clear();
    }

    private void LoadItems()
    {
        Monster monster = MonsterViewController.Instance.Monster;
        for (int i = 0; i < monster.rewards.Count; i++)
        {
            _items.Add(monster.rewards[i].item);
        }
    }
}
