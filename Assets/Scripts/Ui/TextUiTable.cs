using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextUiTable : UiTable<TextUiCell>, IDropHandler
{
    [SerializeField] public List<string> _allTexts = new();

    public override int TotalCellsCount => _allTexts.Count;

    public override void SetupCell(TextUiCell cell)
    {
        cell.Label.text = cell.Index.ToString() + " " + _allTexts[cell.Index];
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Object dropped over: " + name);

        GameObject dropped = eventData.pointerDrag;

        if (dropped.TryGetComponent(out TextUiCell cell))
        {
            cell._targetParent = _scrollRect.content;
        }
    }
}