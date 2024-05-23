using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    private Controller _controller;

    private void Start()
    {
        _controller = SystemManager.Instace.CurrentController;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _controller.Clicked(this);
    }
}
