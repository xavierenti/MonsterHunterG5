using UnityEngine;

public class CraftingViewController : Controller
{
    public static CraftingViewController Instance { get => _instance; }
    private static CraftingViewController _instance;

    private CraftingViewController()
    {
        _instance = this;
    }

    private SystemManager _sysInstance;
    private WindowManager _winInstance;

    private GameObject _tmp = null;

    protected override void Awake()
    {
        OnCellClicked.AddListener(OnClicked);
    }

    private void Start()
    {
        LoadExternalManagers();
        _sysInstance.CurrentController = this;
        LoadView(ViewsName.CRAFTINGINVENTORYLIST, false);
    }

    public override void LoadDataById(int id)
    {
        
    }

    public override void LoadView(ViewsName name, bool destroy)
    {
        _tmp = _winInstance.LoadViewReturnView(name, this.gameObject);
    }

    public override void Clicked(Clickable obj)
    {
        OnCellClicked.Invoke(obj);
    }

    protected override void OnClicked(Clickable obj)
    {
        int index = obj.GetComponentInParent<MonsterUICell>().Index;
        LoadDataById(index);
        LoadView(ViewsName.CRAFTINGITEMDESCRIPTION, true);
    }

    protected override void LoadExternalManagers()
    {
        _sysInstance = SystemManager.Instace;
        _winInstance = WindowManager.Instace;
    }
}
