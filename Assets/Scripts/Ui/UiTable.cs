using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UiTable<T> : MonoBehaviour where T : UiCell
{
    [SerializeField] protected ScrollRect _scrollRect;
    [SerializeField] private T _baseCell;

    protected virtual void Start()
    {
        if (_scrollRect == null)
        {
            Debug.LogWarning("No have scroll rect in UiTable named: " + name);
            return;
        }

        ReloadTable();
    }

    public void ReloadTable()
    {
        RectTransform parent = _scrollRect.content;

        int childCount = parent.childCount;

        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject gO = parent.GetChild(i).gameObject;

            // Limpiar el parent
            if (gO != _baseCell.gameObject)
            {
                Destroy(gO);
            }
        }

        int cellsCount = TotalCellsCount;

        _baseCell.gameObject.SetActive(true);

        for (int i = 0; i < cellsCount; i++)
        {
            // Rellenar el parent
            T cell = Instantiate(_baseCell, parent);
            cell.Index = i;

            SetupCell(cell);
        }

        _baseCell.gameObject.SetActive(false);
    }

    public abstract int TotalCellsCount { get; }

    public abstract void SetupCell(T cell);
}