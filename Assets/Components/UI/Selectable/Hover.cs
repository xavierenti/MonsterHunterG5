using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Setup")]
    [SerializeField] private Color _outlineColor;
    [SerializeField, Range(0f, 6f)] private float _outlineEffect;
    [SerializeField] private Image _image;

    private GameObject _currentObject;

    private Color _normalColor = new Color(1, 1, 1, 245f / 255f);
    private Color _hoveredColor = new Color(1, 1, 1, 1);

    private void Awake()
    {
        _currentObject = this.gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentObject.AddComponent<Outline>();
        Outline outlineComponent = _currentObject.GetComponent<Outline>();

        outlineComponent.effectColor = _outlineColor;
        outlineComponent.effectDistance = new Vector2(_outlineEffect, _outlineEffect);

        _image.color = _hoveredColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject.GetComponent<Outline>());
        _image.color = _normalColor;
    }
}
