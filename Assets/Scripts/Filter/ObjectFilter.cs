using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFilter : MonoBehaviour
{
    public TMP_InputField searchField;
    public GameObject objectsContainer;

    private void Start()
    {
        // Agregar un listener para realizar la búsqueda cuando cambia el valor del campo de búsqueda
        searchField.onValueChanged.AddListener(SearchObjects);
    }

    public void SearchObjects(string searchText)
    {
        // Convertir el texto de búsqueda a minúsculas para hacer la comparación sin distinción entre mayúsculas y minúsculas
        searchText = searchText.ToLower();

        // Recorrer todos los objetos en la grid
        for (int i = 0; i < objectsContainer.transform.childCount; i++)
        {
            // Obtener el botón del objeto actual
            GameObject objectButton = objectsContainer.transform.GetChild(i).gameObject;

            // Obtener el componente TextMeshProUGUI que contiene el nombre del objeto
            TextMeshProUGUI objectNameText = objectButton.GetComponentInChildren<TextMeshProUGUI>();

            // Convertir el nombre del objeto a minúsculas para hacer la comparación sin distinción entre mayúsculas y minúsculas
            string objectName = objectNameText != null ? objectNameText.text.ToLower() : "";

            // Verificar si el nombre del objeto contiene el texto de búsqueda
            if (objectName.Contains(searchText))
            {
                objectButton.SetActive(true);
            }
            else
            {
                objectButton.SetActive(false);
            }

            // Mostrar todos los botones si el campo de búsqueda está vacío
            if (string.IsNullOrEmpty(searchText))
            {
                objectButton.SetActive(true);
            }
        }
    }
}
