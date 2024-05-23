using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterFilter : Filter
{
    [Header("Setup")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private MonsterUITable _table;

    private List<Monster> _originalMonsterList;

    private void Start()
    {
        _originalMonsterList = _table.GetMonsters();
    }
    public override void FilterData(string filter)
    {
        List<Monster> filteredList = _originalMonsterList.FindAll((monster) => {
            return monster.name.ToLower().Contains(filter.ToLower());
        });

        _table.SetMonsters(filteredList);
        _table.ReloadTable();
    }
}
