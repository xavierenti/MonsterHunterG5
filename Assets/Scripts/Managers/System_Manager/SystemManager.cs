using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Genres { MALE, FEMALE }
public enum Equipe { HEAD, HAND, BODY, PANTS, BOOTS }

public class SystemManager : MonoBehaviour
{
    private static SystemManager _instance;
    public static SystemManager Instace
    {
        get
        {
            return _instance;
        }
    }

    public Controller CurrentController { get => _currentController; set { _currentController = value; } }
    private Controller _currentController;

    private SystemManager()
    {
        _instance = this;
    }

    private Dictionary<int, Armour> _armours = new();
    private Dictionary<int, Item> _items = new();
    private Dictionary<int, Monster> _monsters = new();

    private List<InventoryItem> _invetory = new();

    private Genres _genre = Genres.MALE;

    WindowManager _winInstance;
    ApiMHHelper _instanceAPI;

    private bool[] _loadedData = { false, false, false };

    private void Start()
    {
        _winInstance = WindowManager.Instace;
        _instanceAPI = ApiMHHelper.Instance;
        StartCoroutine(LoadData());
    }

    public void GetAllMonsters(List<Monster> monstersList)
    {
        foreach (Monster monster in _monsters.Values)
        {
            monstersList.Add(monster);
        }
    }

    public Monster GetMonster(int monsterId)
    {
        return _monsters[monsterId];
    }

    public List<InventoryItem> GetInventory()
    {
        return _invetory;
    }

    public void AddToInventory(Item item)
    {
        _invetory.Add(item);
    }

    private void OnArmoursSuccess(List<Armour> result)
    {
        foreach (Armour armour in result)
        {
            _armours.Add(armour.id, armour);
        }

        _loadedData[0] = true;
    }
    private void OnItemsSuccess(List<Item> result)
    {
        foreach (Item item in result)
        {
            _items.Add(item.id, item);
        }

        _loadedData[1] = true;
    }
    private void OnMonstersSuccess(List<Monster> result)
    {
        foreach (Monster monster in result)
        {
            _monsters.Add(monster.id, monster);
        }

        _loadedData[2] = true;
    }

    private IEnumerator LoadData()
    {
        GameObject tmp = _winInstance.LoadViewReturnView(ViewsName.LOADING);

        _instanceAPI.MakeArmoursApiCall(OnArmoursSuccess);
        _instanceAPI.MakeItemsApiCall(OnItemsSuccess);
        _instanceAPI.MakeMonstersApiCall(OnMonstersSuccess);

        bool isLoadingData = true;

        while (isLoadingData)
        {
            if (_loadedData[0] && _loadedData[1] && _loadedData[2])
            {
                isLoadingData = false;
                Debug.Log("[*] Finish Loaded");
                _winInstance.DestroyView(tmp);
                _winInstance.LoadView(ViewsName.CRAFTING);
            }

            yield return null;
        }
    }
}
