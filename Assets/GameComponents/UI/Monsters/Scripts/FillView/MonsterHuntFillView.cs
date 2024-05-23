using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class MonsterHuntFillView : FillView
{
    [Header("Monster FillView Setup")]
    [SerializeField] private SVGImage _monsterSvgImage;
    [SerializeField] private TextMeshProUGUI _monsterNameText;
    [SerializeField] private TextMeshProUGUI _monsterDescriptionText; 

    private Monster _monster;

    public void LoadMonsterHunt(Monster monster)
    {
        _monster = monster;
        Setup();
    }

    private void Setup()
    {
        string specieString = _monster.species;
        specieString = specieString.Replace(" ", "_");
        GameObject specieSvg = Resources.Load<GameObject>("Monsters/Species/" + specieString);

        _monsterSvgImage.sprite = specieSvg.GetComponent<SpriteRenderer>().sprite;
        _monsterSvgImage.color = ColorSpecies.ColorSpeciesDict[specieString];

        _monsterNameText.text = _monster.name;
        _monsterDescriptionText.text = GenerateDescription(specieString);        
    }

    private string GenerateDescription(string specie)
    {
        string description = $"[Name]\t{_monster.name}\n";
        //description += $"[Description]\t<color={ColorSpecies.HexColorSpeciesDict[specie]}>{_monster.description}\n";
        description += $"[Description]\t{_monster.description}\n";
        description += $"[Type]\t{_monster.type}\n";
        description += $"[Specie]\t{_monster.species}\n";

        if (_monster.ailments.Count > 0)
        {
            description += "[Ailments]\t";
            for (int i = 0; i < _monster.ailments.Count; i++)
            {
                description += "\n\t" + _monster.ailments[i].name + " - " + _monster.ailments[i].description;
            }
            description += "\n";
        }

        if (_monster.locations.Count > 0)
        {
            description += "[Locations]\t";
            for (int i = 0; i < _monster.locations.Count; i++)
            {
                description += _monster.locations[i].name + " ";
            }
            description += "\n";
        }

        if (_monster.weaknesses.Count > 0)
        {
            description += "[Weaknesses]\t";
            for (int i = 0; i < _monster.weaknesses.Count; i++)
            {
                description += _monster.weaknesses[i].element + " ";
            }
            description += "\n";
        }

        if (_monster.resistances.Count > 0)
        {
            description += "[Resistances]\t";
            for (int i = 0; i < _monster.resistances.Count; i++)
            {
                description += _monster.resistances[i].element + " ";
            }
            description += "\n";
        }

        return description;
    }
}
