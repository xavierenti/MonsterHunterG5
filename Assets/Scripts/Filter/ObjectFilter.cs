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
        // Agregar un listener para realizar la b�squeda cuando cambia el valor del campo de b�squeda
        searchField.onValueChanged.AddListener(SearchObjects);
    }

    public void SearchObjects(string searchText)
    {
        // Convertir el texto de b�squeda a min�sculas para hacer la comparaci�n sin distinci�n entre may�sculas y min�sculas
        searchText = searchText.ToLower();

        // Recorrer todos los objetos en la grid
        for (int i = 0; i < objectsContainer.transform.childCount; i++)
        {
            // Obtener el bot�n del objeto actual
            GameObject objectButton = objectsContainer.transform.GetChild(i).gameObject;

            // Obtener el componente TextMeshProUGUI que contiene el nombre del objeto
            TextMeshProUGUI objectNameText = objectButton.GetComponentInChildren<TextMeshProUGUI>();

            // Convertir el nombre del objeto a min�sculas para hacer la comparaci�n sin distinci�n entre may�sculas y min�sculas
            string objectName = objectNameText != null ? objectNameText.text.ToLower() : "";

            // Verificar si el nombre del objeto contiene el texto de b�squeda
            if (objectName.Contains(searchText))
            {
                objectButton.SetActive(true);
            }
            else
            {
                objectButton.SetActive(false);
            }

            // Mostrar todos los botones si el campo de b�squeda est� vac�o
            if (string.IsNullOrEmpty(searchText))
            {
                objectButton.SetActive(true);
            }
        }
    }
}
