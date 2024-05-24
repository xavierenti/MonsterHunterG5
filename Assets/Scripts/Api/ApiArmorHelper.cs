using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ApiArmorHelper;
using static ApiArmorHelper.Armor.Crafting;

public class ApiArmorHelper : MonoBehaviour
{
    private HoverBehaviour hover;

    [Header("Api call Steup")]
    public string url = "";
    public Dictionary<string, string> parameters = new();

    [Header("Objects")]
    [Tooltip("Prefab para los botones de la generación de ARMADURAS")]
    [SerializeField] private GameObject armorButtonPrefab;
    [SerializeField] private Transform grid;

    private TextMeshProUGUI resultField;
    private RawImage imageField;

    [Header("Armor Info")]
    [Tooltip("Prefab para el apartado donde se muestra la Info de cada ARMADURA")]
    [SerializeField] private GameObject armorInfoPrefab;
    public RawImage armorResultIcon;
    public TextMeshProUGUI armorResultName;
    public TextMeshProUGUI armorResultType;
    public TextMeshProUGUI armorResultRank;
    public TextMeshProUGUI armorResultRarity;
    public TextMeshProUGUI armorResultBaseDefense;
    public TextMeshProUGUI armorResultMaxDefense;
    public TextMeshProUGUI armorResultAugmentedDefense;
    public TextMeshProUGUI armorResultFireRestistance;
    public TextMeshProUGUI armorResultWaterRestistance;
    public TextMeshProUGUI armorResultIceRestistance;
    public TextMeshProUGUI armorResultThunderRestistance;
    public TextMeshProUGUI armorResultDragonRestistance;
    public TextMeshProUGUI armorResultSkills;

    [Header("Armor Craft")]
    [Tooltip("Prefab para los botones de la generación de MATERIALES")]
    [SerializeField] private GameObject materialButtonPrefab;
    public RawImage armorCraftIcon;
    public Transform gridCrafts;

    [Header("Armor Craft Info")]
    [Tooltip("Prefab para el apartado donde se muestra la Info de cada MATERIAL")]
    [SerializeField] private GameObject armorCraftInfoPrefab;
    public TextMeshProUGUI materialResultName;
    public TextMeshProUGUI materialResultDescription;
    public TextMeshProUGUI materialResultRarity;
    public TextMeshProUGUI materialResultCarryLimit;
    public Button materialHuntButton;

    private List<Armor> armors; // Lista de armaduras cargados
    private List<Armor.Crafting.Materials> armorMaterials; // Lista de armaduras cargados

    private List<ApiMonsterHelper.Monster> monsters;

    [Header("Armor Item Monster")]
    [SerializeField] private GameObject monsterButtonPrefab;
    public Transform gridMonsters;

    private void Awake()
    {
        hover = HoverBehaviour.instance;

        resultField = armorButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();
        imageField = armorButtonPrefab.GetComponentInChildren<RawImage>();

        armorInfoPrefab.SetActive(false);
        armorCraftInfoPrefab.SetActive(false);
    }

    private void Start()
    {
        MakeArmorApiCall();
    }

    #region ARMOR
    [Serializable]
    public class Armor
    {
        public int id;
        public string name;
        public string type;
        public string rank;
        public int rarity;
        public Defense defense;
        public Resistances resistances;
        public List<Skill> skills;
        public Crafting crafting;
        public Assets assets;

        [Serializable]
        public class Defense
        {
            [Unity.Serialization.FormerName("base")]
            public int baseDef;
            public int max;
            public int augmented;
        }

        [Serializable]
        public class Resistances
        {
            public int fire;
            public int water;
            public int ice;
            public int thunder;
            public int dragon;
        }

        [Serializable]
        public class Skill
        {
            public int id;
            public int level;
            public string description;
            public int skill;
            public string skillName;
        }

        [Serializable]
        public class Crafting
        {
            public List<Materials> materials;

            [Serializable]
            public class Materials
            {
                public int quantity;
                public Item item;

                [Serializable]
                public class Item
                {
                    public int id;
                    public string name;
                    public string description;
                    public int rarity;
                    public int carryLimit;
                    public int sellPrice;
                    public int buyPrice;
                }
            }
        }

        [Serializable]
        public class Assets
        {
            public string imageMale;
            public string imageFemale;
        }
    }
    #endregion ARMOR

    #region API_CALL
    public void MakeArmorApiCall()
    {
        IEnumerator apiCall = ApiHelper.Get<List<Armor>>(url, parameters, OnArmorListSuccess, OnArmorFailure);

        StartCoroutine(apiCall);
    }

    private void OnArmorFailure(Exception exception)
    {
        resultField.text = "Call Error:" + "<br>" + exception.Message;
    }

    private void OnArmorListSuccess(List<Armor> result)
    {
        if (result == null)
        {
            Debug.LogError("Armor list is null.");
            return;
        }

        // Almacenar la lista de armaduras cargadas
        armors = result;

        // Limpiar los hijos del grid antes de instanciar nuevas armaduras
        ClearGrid(grid);

        foreach (Armor armor in result)
        {
            // Crear una nueva instancia del prefab de botón para hombres
            GameObject newMaleArmorButton = Instantiate(armorButtonPrefab, Vector3.zero, Quaternion.identity, grid);

            IEnumerator imageMaleApiCall = ApiHelper.GetTexture(armor.assets.imageMale.Replace("\0", "/"), (texture) =>
            {
                newMaleArmorButton.GetComponentInChildren<RawImage>().texture = texture;
            }, OnImageFailure);
            StartCoroutine(imageMaleApiCall);

            // Configurar el nombre de la armadura en el botón
            newMaleArmorButton.GetComponentInChildren<TextMeshProUGUI>().text = armor.name + " (Male)";

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button maleButton = newMaleArmorButton.GetComponent<Button>();
            maleButton.onClick.AddListener(() => MakeArmorApiInfo(armor, "male"));

            // Crear una nueva instancia del prefab de botón para mujeres
            GameObject newFemaleArmorButton = Instantiate(armorButtonPrefab, Vector3.zero, Quaternion.identity, grid);

            IEnumerator imageFemaleApiCall = ApiHelper.GetTexture(armor.assets.imageFemale.Replace("\0", "/"), (texture) =>
            {
                newFemaleArmorButton.GetComponentInChildren<RawImage>().texture = texture;
            }, OnImageFailure);
            StartCoroutine(imageFemaleApiCall);

            // Configurar el nombre de la armadura en el botón
            newFemaleArmorButton.GetComponentInChildren<TextMeshProUGUI>().text = armor.name + " (Female)";

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button femaleButton = newFemaleArmorButton.GetComponent<Button>();
            femaleButton.onClick.AddListener(() => MakeArmorApiInfo(armor, "female"));
        }
    }

    private void OnImageFailure(Exception exception)
    {
        imageField.texture = null;
        imageField.color = Color.red;
        Debug.LogError("Image loading failed: " + exception.Message);
    }

    private void OnImageSuccess(Texture texture)
    {
        imageField.color = Color.white;
        imageField.texture = texture;
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

    public void MakeArmorApiInfo(Armor armor, string gender)
    {
        // Desactivar el Prefab de la info de los materiales
        if (armorCraftInfoPrefab.activeSelf)
        {
            armorCraftInfoPrefab.SetActive(false);
        }

        // Verificar si el Prefab no está visible
        if (!armorInfoPrefab.activeSelf)
        {
            // Activar el Prefab de la info de las armaduras
            armorInfoPrefab.SetActive(true);
        }

        IEnumerator apiCall = ApiHelper.Get<List<Armor>>(url, parameters, result => OnArmorInfo(armor, gender), OnArmorFailure);
        StartCoroutine(apiCall);
    }

    private void OnArmorInfo(Armor result, string gender)
    {
        armorResultName.text = result.name;
        armorResultType.text = result.type;
        armorResultRank.text = result.rank;
        armorResultRarity.text = result.rarity.ToString();
        armorResultBaseDefense.text = result.defense.baseDef.ToString();
        armorResultMaxDefense.text = result.defense.max.ToString();
        armorResultAugmentedDefense.text = result.defense.augmented.ToString();
        armorResultFireRestistance.text = result.resistances.fire.ToString();
        armorResultWaterRestistance.text = result.resistances.water.ToString();
        armorResultIceRestistance.text = result.resistances.ice.ToString();
        armorResultThunderRestistance.text = result.resistances.thunder.ToString();
        armorResultDragonRestistance.text = result.resistances.dragon.ToString();

        // Construir la cadena de habilidades
        string skillsText = "";
        foreach (Armor.Skill skill in result.skills)
        {
            skillsText += $"{skill.skillName} (Level {skill.level}), ";
        }
        armorResultSkills.text = skillsText.TrimEnd(',', ' '); 

        // Obtener la URL de la imagen según el género
        string imageUrl = GetGenderImageUrl(result, gender);

        // Llama a la función para cargar la textura de la imagen
        IEnumerator imageApiCall = ApiHelper.GetTexture(imageUrl.Replace("\0", "/"), (texture) =>
        {
            armorResultIcon.texture = texture;

            if (armorCraftIcon == null) { return; }

            armorCraftIcon.texture = texture;

        }, OnImageInfoFailure);
        StartCoroutine(imageApiCall);

        if (materialButtonPrefab == null) { return; }

        MakeMaterialsList(result.crafting.materials);
    }

    private void MakeMaterialsList(List<Armor.Crafting.Materials> result)
    {
        ClearGrid(gridCrafts);

        foreach (Armor.Crafting.Materials material in result)
        {
            // Crear una nueva instancia del prefab de botón para los materiales
            GameObject newMaterialArmorButton = Instantiate(materialButtonPrefab, Vector3.zero, Quaternion.identity, gridCrafts);

            // Configurar el nombre de la armadura en el botón
            newMaterialArmorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = material.item.name;
            newMaterialArmorButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = material.quantity.ToString();

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button maleButton = newMaterialArmorButton.GetComponent<Button>();
            maleButton.onClick.AddListener(() => MakeMaterialInfo(material));
        }
    }

    private void MakeMaterialInfo(Armor.Crafting.Materials materials)
    {
        // Desactivar el Prefab de la info de las armaduras
        if (armorInfoPrefab.activeSelf)
        {
            armorInfoPrefab.SetActive(false);
        }

        // Verificar si el Prefab no está visible
        if (!armorCraftInfoPrefab.activeSelf)
        {
            // Activar el Prefab de la info de los materiales
            armorCraftInfoPrefab.SetActive(true);
        }

        IEnumerator apiCall = ApiHelper.Get<List<Armor>>(url, parameters, result => OnMaterialInfo(materials), OnArmorFailure);
        StartCoroutine(apiCall);
    }

    private void OnMaterialInfo(Armor.Crafting.Materials result)
    {
        materialResultName.text = result.item.name;
        materialResultDescription.text = result.item.description;
        materialResultRarity.text = result.item.rarity.ToString();
        materialResultCarryLimit.text = result.item.carryLimit.ToString();

        // Obtener la lista completa de monstruos
        List<ApiMonsterHelper.Monster> monsters = ApiMonsterHelper.GetMonsters();
        Debug.Log("Total de monstruos: " + monsters.Count);

        // Filtrar la lista de monstruos que dan la recompensa del material seleccionado
        List<ApiMonsterHelper.Monster> monstersFiltered = MonstersReward(result.item.id);
        Debug.Log("Monstruos filtrados que dropean el material: " + monstersFiltered.Count);
        
        ClearGrid(gridMonsters);

        // Verificar si hay monstruos que dropean el material
        if (monstersFiltered.Count > 0)
        {
            // Mostrar información sobre los monstruos filtrados
            foreach (ApiMonsterHelper.Monster monster in monstersFiltered)
            {

                Debug.Log("Nombre del monstruo: " + monster.name);
                // Agrega aquí código para mostrar más información sobre el monstruo en tu interfaz de usuario

                GameObject newMonsterButton = Instantiate(monsterButtonPrefab, Vector3.zero, Quaternion.identity, gridMonsters);

                newMonsterButton.GetComponentInChildren<TextMeshProUGUI>().text = monster.name;

                Button button = newMonsterButton.GetComponent<Button>();
                button.onClick.AddListener(() => SelectMonsterCombat(monster));
            
                // Llevar al usuario a la pantalla de combate con los monstruos filtrados
                Debug.Log("Monstruos encontrados. Ir a la pantalla de combate.");

                materialHuntButton.onClick.AddListener(() =>
                {
                    hover.SelectCombat();

                    ApiMonsterHelper.instance.OnMonsterInfo(monster);
                    // Filtro
                });
            }
        }
        else
        {
            // Si ningún monstruo coincide, agregar el material directamente al inventario
            Debug.Log("Ningún monstruo encontrado. Agregar material al inventario.");
            //Inventory.AddItem();

            materialHuntButton.onClick.AddListener(() =>
            {
                hover.SelectInventory();
            });
        }

        // Comparar los items que dropean los monstruos y los que coincidan llevar al usuario a la pantalla de combate
        // con los monstruos filtrados de los que dropean el material, si ningun monstruo coincide,
        // que obtenga el material directamente al inventario
    }

    private List<ApiMonsterHelper.Monster> MonstersReward(int materialId)
    {
        List<ApiMonsterHelper.Monster> monstersFiltered = new List<ApiMonsterHelper.Monster>();

        // Iterar sobre cada monstruo
        foreach (ApiMonsterHelper.Monster monster in ApiMonsterHelper.GetMonsters())
        {
            // Verificar si el monstruo tiene el material con el 'id' especificado en sus recompensas
            bool hasMaterial = false;

            foreach (ApiMonsterHelper.Monster.Rewards reward in monster.rewards)
            {
                if (reward.item.id == materialId)
                {
                    hasMaterial = true;
                    break;
                }
            }

            // Agregar el monstruo a la lista si tiene el material
            if (hasMaterial)
            {
                monstersFiltered.Add(monster);
            }
        }

        // Devolver la lista de monstruos filtrados
        return monstersFiltered;
    }

    private void OnImageInfoFailure(Exception exception)
    {
        armorResultIcon.texture = null;
        armorResultIcon.color = Color.red;
        Debug.LogError("Image loading failed: " + exception.Message);
    }

    private void OnImageInfoSuccess(Texture texture)
    {
        armorResultIcon.color = Color.white;
        armorResultIcon.texture = texture;
    }

    private string GetGenderImageUrl(Armor armor, string buttonName)
    {
        // Determinar el género según el nombre del botón
        string gender = buttonName.ToLower().Contains("female") ? "imageFemale" : "imageMale";

        // Obtener la URL de la imagen según el género
        return armor.assets.GetType().GetField(gender)?.GetValue(armor.assets).ToString().Replace("\0", "/");
    }

    private void SelectMonsterCombat(ApiMonsterHelper.Monster monster)
    {
        hover.SelectCombat();

        ApiMonsterHelper.instance.OnMonsterInfo(monster);
    }
}