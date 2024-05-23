using System.Collections.Generic;
using UnityEngine;

public class MonsterUITable : UITable<MonsterUICell>
{
    [Header("Setup MonsterUITable")]
    [SerializeField] private List<Monster> _monsters;

    new private void Start()
    {
        SystemManager.Instace.GetAllMonsters(_monsters);
        base.Start();
    }
    public override int TotalCellsCount => _monsters.Count;

    public List<Monster> GetMonsters()
    {
        return _monsters;
    }

    public void SetMonsters(List<Monster> monsters)
    {
        _monsters = monsters;
    }

    public override void SetupCell(MonsterUICell cell)
    {
        int index = cell.Index;
        string specieString = _monsters[index].species;

        cell.MonsterId = _monsters[index].id;
        cell.MonsterName.text = _monsters[index].name;
        cell.MonsterType.text = _monsters[index].type;
        cell.MonsterSpecie.text = specieString;

        specieString = specieString.Replace(" ", "_");
        GameObject specieSvg = Resources.Load<GameObject>("Monsters/Species/" + specieString);
        cell.MonsterSvgImage.sprite = specieSvg.GetComponent<SpriteRenderer>().sprite;
        cell.MonsterSvgImage.color = ColorSpecies.ColorSpeciesDict[specieString];

        cell.Index = _monsters[index].id;
    }

    private void OnDisable()
    {
        _monsters.Clear();
    }
}
