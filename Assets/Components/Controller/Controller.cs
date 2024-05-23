using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public int MaxViews;
    [SerializeField] public int MinViews;

    private SystemManager _sysInst;
    private WindowManager _winInst;

    public UnityEvent<Clickable> OnCellClicked;

    protected virtual void Awake() { }
    protected virtual void OnDisable() { }
    public virtual void LoadDataById(int id) { }
    public virtual void LoadView(ViewsName name, bool destroy) { }
    protected virtual void DestroyChilds() { }
    protected virtual void DestroyVariableViews() { }
    protected virtual void LoadExternalManagers() { }
    public virtual void Clicked(Clickable obj) { }
    protected virtual void OnClicked(Clickable obj) { }
}
