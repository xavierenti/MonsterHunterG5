using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ApiArmorHelper;
using static ApiMonsterHelper;
using static ApiMonsterHelper.Monster;

public class ApiMonsterHelper : MonoBehaviour
{
    public static ApiMonsterHelper instance { get; private set; }

    [Header("Api call Steup")]
    public string url = "";
    public Dictionary<string, string> parameters = new();

    [Header("Objects")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform grid;

    private TextMeshProUGUI resultField;

    [Header("Monster Info")]
    public TextMeshProUGUI resultName;
    public TextMeshProUGUI resultDescription;
    [SerializeField] private GameObject buttonResultReward;
    [SerializeField] private Transform gridRewards;

    [Header("Monster Rewards Info")]
    public TextMeshProUGUI resultItemName;
    [SerializeField] private GameObject buttonResultMonsterReward;
    [SerializeField] private Transform gridMonsterRewards;

    private static List<Monster> monsters; // Lista de monstruos cargados
    private static List<Monster.Rewards> monstersRewards; // Lista de monstruos cargados

    public static List<Monster> GetMonsters()
    {
        return monsters;
    }

    private void Awake()
    {
        instance = this;

        resultField = buttonPrefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        MakeMonsterApiCall();
    }

    #region MONSTER
    [Serializable]
    public class Monster
    {
        public int id;
        public string name;
        public string type;
        public string species;
        public string description;
        public List<Elements> elements;
        public List<Aliments> aliments;
        public List<Resistances> resistances;
        public List<Weaknesses> weaknesses;
        public List<Rewards> rewards;

        [Serializable]
        public class Elements
        {
            [Unity.Serialization.FormerName("0")]
            public string element;
        }

        [Serializable]
        public class Aliments
        {
            public int id;
            public string name;
            public string description;
            public Recovery recovery;
            public Protection protection;

            [Serializable]
            public class Recovery
            {
                public List<Actions> actions;
                public List<Items> items;

                [Serializable]
                public class Actions
                {
                    [Unity.Serialization.FormerName("0")]
                    public string action;
                }

                [Serializable]
                public class Items
                {
                    public int id;
                    public int rarity;
                    public int value;
                    public int carryLimit;
                    public string name;
                    public string description;
                }
            }

            [Serializable]
            public class Protection
            {
                public List<Skills> skills;
                public List<Items> items;

                [Serializable]
                public class Skills
                {
                    public int id;
                    public string name;
                    public string description;
                }

                [Serializable]
                public class Items
                {
                    public int id;
                    public int rarity;
                    public int value;
                    public int carryLimit;
                    public string name;
                    public string description;
                }
            }
        }

        [Serializable]
        public class Locations
        {
            public int id;
            public string name;
            public int zoneCount;
        }

        [Serializable]
        public class Resistances
        {
            public string element;
            public string condition;
        }

        [Serializable]
        public class Weaknesses
        {
            public string element;
            public int stars;
            public string condition;
        }

        [Serializable]
        public class Rewards
        {
            public int id;
            public Item item;
            public List<Conditions> conditions;

            [Serializable]
            public class Item
            {
                public int id;
                public string name;
                public string description;
                public int rarity;
                public int carryLimit;
                public int value;
            }

            [Serializable]
            public class Conditions
            {
                public string type;
                public string subtype;
                public string rank;
                public int quantity;
                public int chance;
            }
        }
    }
    #endregion MONSTER

    #region API_CALL
    public void MakeMonsterApiCall()
    {
        IEnumerator apiCall = ApiHelper.Get<List<Monster>>(url, parameters, OnMonsterListSuccess, OnMonsterFailure);

        resultField.text = "In Progress";

        StartCoroutine(apiCall);
    }

    private void OnMonsterFailure(Exception exception)
    {
        resultField.text = "Call Error:" + "<br>" + exception.Message;
    }

    private void OnMonsterSuccess(Monster result)
    {
        GameObject newMonster = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, grid);

        resultField.text = result.name;
    }

    private void OnMonsterListSuccess(List<Monster> result)
    {
        resultField.text = "Monsters Loaded";

        // Limpiar los hijos del grid antes de instanciar nuevos monstruos
        ClearGrid(grid);

        // Almacenar la lista de monstruos cargados
        monsters = result;

        foreach (Monster monster in result)
        {
            // Crear una nueva instancia del prefab de botón
            GameObject newMonsterButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, grid);

            // Configurar el nombre del monstruo en el botón
            newMonsterButton.GetComponentInChildren<TextMeshProUGUI>().text = monster.name;

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button button = newMonsterButton.GetComponent<Button>();
            button.onClick.AddListener(() => MakeMonsterApiInfo(monster));
        }
    }

    // Limpiar los hijos del grid
    private void ClearGrid(Transform grid)
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion API_CALL

    public void MakeMonsterApiInfo(Monster monster)
    {
        IEnumerator apiCall = ApiHelper.Get<List<Monster>>(url, parameters, result => OnMonsterInfo(monster), OnMonsterFailure);
        StartCoroutine(apiCall);
    }

    public void OnMonsterInfo(Monster result)
    {
        resultName.text = result.name;
        resultDescription.text = result.description;

        MakeRewardList(result.rewards);
    }
   
    private void MakeRewardList(List<Monster.Rewards> result)
    {
        // Limpiar los hijos del grid antes de instanciar nuevas rewards
        ClearGrid(gridRewards);

        foreach (Monster.Rewards reward in result)
        {
            // Crear una nueva instancia del prefab de botón
            GameObject newMonsterRewardButton = Instantiate(buttonResultReward, Vector3.zero, Quaternion.identity, gridRewards);

            // Configurar el nombre del ítem de la recompensa en el botón
            newMonsterRewardButton.GetComponentInChildren<TextMeshProUGUI>().text = reward.item.name;

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button button = newMonsterRewardButton.GetComponent<Button>();
            button.onClick.AddListener(() => MakeMonsterRewardApiInfo(reward.item));
        }
    }

    public void MakeMonsterRewardApiInfo(Monster.Rewards.Item monsterRewardItem)
    {
        List<Monster.Rewards.Item> rewardsList = new List<Monster.Rewards.Item> { monsterRewardItem };

        IEnumerator apiCall = ApiHelper.Get<List<Monster.Rewards.Item>>(url, parameters, result => OnMonsterRewardInfo(rewardsList), OnMonsterFailure);
        StartCoroutine(apiCall);
    }

    public void OnMonsterRewardInfo(List<Monster.Rewards.Item> monsterRewardItem)
    {
        // Limpiar los hijos del grid antes de instanciar nuevas recompensas de monstruos
        ClearGrid(gridMonsterRewards);

        if (monsterRewardItem != null && monsterRewardItem.Count > 0)
        {
            Monster.Rewards.Item rewards = monsterRewardItem[0];

            resultItemName.text = rewards.name;

            foreach (Monster monster in monsters)
            {
                if (HasReward(monster, rewards))
                {
                    GameObject newMonsterRewardButton = Instantiate(buttonResultMonsterReward, Vector3.zero, Quaternion.identity, gridMonsterRewards);
                    newMonsterRewardButton.GetComponentInChildren<TextMeshProUGUI>().text = monster.name;

                    Button button = newMonsterRewardButton.GetComponent<Button>();
                    button.onClick.AddListener(() => OnMonsterInfo(monster));
                }
            }
        }
    }

    public bool HasReward(Monster monster, Monster.Rewards.Item rewards)
    {
        foreach (Monster.Rewards reward in monster.rewards)
        {
            if (reward.item.name == rewards.name) { return true; }
        }
        return false;
    }
}