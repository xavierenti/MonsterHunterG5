using System.Collections.Generic;
using UnityEngine;

public enum ViewsName { LOADING, INVENTORY, CRAFTING, CRAFTINGINVENTORYLIST, CRAFTINGITEMDESCRIPTION, CRAFTINGRECIPE, HUNTING, MONSTERTOHUNT, MONSTERSEARCHER, MONSTERREWARDS, MONSTERREAWRDPOPUP }

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instace { get => _instance; }
    private static WindowManager _instance;

    [Header("Setup")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<GameObject> _views;

    private WindowManager()
    {
        _instance = this;
    }

    public void LoadView(ViewsName name)
    {
        GameObject tmp = Instantiate(_views[(int)name]);
        tmp.transform.SetParent(_canvas.transform);
        CenterView(tmp);
    }

    public void LoadView(ViewsName name, GameObject parent)

    {
        GameObject tmp = Instantiate(_views[(int)name]);
        tmp.transform.SetParent(parent.transform);
        CenterView(tmp);
    }

    public GameObject LoadViewReturnView(ViewsName name)
    {
        GameObject tmp = Instantiate(_views[(int)name]);
        tmp.transform.SetParent(_canvas.transform);
        CenterView(tmp);
        return tmp;
    }

    public GameObject LoadViewReturnView(ViewsName name, GameObject parent)
    {
        GameObject tmp = Instantiate(_views[(int)name]);
        tmp.transform.SetParent(parent.transform);
        CenterView(tmp);
        return tmp;
    }

    public void DestroyView(GameObject view)
    {
        Destroy(view);
    }

    private void CenterView(GameObject view)
    {
        RectTransform tmpRect = view.GetComponent<RectTransform>();
        tmpRect.anchoredPosition = Vector2.zero;
        tmpRect.localScale = Vector2.one;
        tmpRect.offsetMax = Vector2.zero;
        tmpRect.offsetMin = Vector2.zero;
    }
}
