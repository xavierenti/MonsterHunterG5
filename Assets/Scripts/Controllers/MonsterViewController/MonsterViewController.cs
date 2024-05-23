using System.Collections.Generic;
using UnityEngine;

public class MonsterViewController : Controller
{
    public static MonsterViewController Instance { get => _instance; }
    private static MonsterViewController _instance;

    private MonsterViewController()
    {
        _instance = this;
    }

    public Monster Monster { get => _monster; }
    private Monster _monster = null;

    private SystemManager _sysInstance;
    private WindowManager _winInstance;

    private GameObject _tmp = null;

    public List<Item> ItemsRewards { get => _itemRewards; set => _itemRewards = value; }
    private List<Item> _itemRewards = new();

    protected override void Awake()
    {
        OnCellClicked.AddListener(OnClicked);
    }

    private void Start()
    {
        LoadExternalManagers();
        _sysInstance.CurrentController = this;
        LoadView(ViewsName.MONSTERSEARCHER, false);
    }

    public override void LoadDataById(int id)
    {
        _monster = _sysInstance.GetMonster(id);
    }

    public override void LoadView(ViewsName name, bool destroy)
    {
        if (destroy)
        {
            DestroyVariableViews();
        }
        _tmp = _winInstance.LoadViewReturnView(name, this.gameObject);
    }

    protected override void DestroyChilds()
    {
        for (int i = MinViews; i <= this.gameObject.transform.childCount - 2; i++)
        {
            GameObject tmp = this.gameObject.transform.GetChild(i).gameObject;
            Destroy(tmp);
        }
    }

    protected override void DestroyVariableViews()
    {
        if (this.transform.childCount < MaxViews) return;

        DestroyChilds();
    }

    public override void Clicked(Clickable obj)
    {
        OnCellClicked.Invoke(obj);
    }

    protected override void OnClicked(Clickable obj)
    {
        int index = obj.GetComponentInParent<MonsterUICell>().Index;
        LoadDataById(index);
        LoadView(ViewsName.MONSTERTOHUNT, true);
        _tmp.GetComponent<MonsterHuntFillView>().LoadMonsterHunt(_monster);
        LoadView(ViewsName.MONSTERREWARDS, false);
    }

    protected override void LoadExternalManagers()
    {
        _sysInstance = SystemManager.Instace;
        _winInstance = WindowManager.Instace;
    }
}