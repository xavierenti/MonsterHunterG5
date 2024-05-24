using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lista que almacena los objetos en el inventario
    private List<ApiItemHelper> items = new List<ApiItemHelper>();

    private void Start()
    {
        GetAllItems();
    }

    // Método para agregar un objeto al inventario
    public void AddItem(ApiItemHelper newItem)
    {
        items.Add(newItem);
    }

    // Método para eliminar un objeto del inventario
    public void RemoveItem(ApiItemHelper itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    // Método para verificar si un objeto está en el inventario
    public bool ContainsItem(ApiItemHelper itemToCheck)
    {
        return items.Contains(itemToCheck);
    }

    // Método para obtener todos los objetos en el inventario
    public List<ApiItemHelper> GetAllItems()
    {
        return new List<ApiItemHelper>(items);
    }
}
