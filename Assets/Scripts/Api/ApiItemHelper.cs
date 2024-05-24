using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApiItemHelper : MonoBehaviour
{
    [Header("Api call Steup")]
    public string url = "";
    public Dictionary<string, string> parameters = new();

    [Header("Objects")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform grid;

    private TextMeshProUGUI resultField;

    [Header("Item Info")]
    public TextMeshProUGUI resultName;
    public TextMeshProUGUI resultDescription;
    public TextMeshProUGUI resultRarity;
    public TextMeshProUGUI resultCarryLimit;
    public TextMeshProUGUI resultValue;

    private List<Item> items; // Lista de monstruos cargados

    private void Awake()
    {
        resultField = buttonPrefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        MakeItemApiCall();
    }

    #region ITEM
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
    #endregion ITEM

    #region API_CALL
    public void MakeItemApiCall()
    {
        IEnumerator apiCall = ApiHelper.Get<List<Item>>(url, parameters, OnItemListSuccess, OnItemFailure);

        resultField.text = "In Progress";

        StartCoroutine(apiCall);
    }

    private void OnItemFailure(Exception exception)
    {
        resultField.text = "Call Error:" + "<br>" + exception.Message;
    }

    private void OnItemListSuccess(List<Item> result)
    {
        resultField.text = "Items Loaded";

        // Almacenar la lista de monstruos cargados
        items = result;

        // Limpiar los hijos del grid antes de instanciar nuevos monstruos
        ClearGrid();

        foreach (Item item in result)
        {
            // Crear una nueva instancia del prefab de botón
            GameObject newItemButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, grid);

            // Configurar el nombre del monstruo en el botón
            newItemButton.GetComponentInChildren<TextMeshProUGUI>().text = item.name;

            // Agregar un Listener al botón para mostrar la información cuando se hace clic
            Button button = newItemButton.GetComponent<Button>();
            button.onClick.AddListener(() => MakeItemApiInfo(item));
        }
    }

    // Limpiar los hijos del grid
    private void ClearGrid()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion API_CALL

    public void MakeItemApiInfo(Item item)
    {
        IEnumerator apiCall = ApiHelper.Get<List<Item>>(url, parameters, result => OnItemInfo(item), OnItemFailure);
        StartCoroutine(apiCall);
    }

    private void OnItemInfo(Item result)
    {
        resultName.text = result.name;
        resultDescription.text = result.description;
        resultRarity.text = result.rarity.ToString();
        resultCarryLimit.text = result.carryLimit.ToString();
        resultValue.text = result.value.ToString();
    }
}