using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite bgImage;
    [SerializeField] private Sprite bgImageHover;
    [SerializeField] private Sprite bgImageOnClick;

    private Image imageComponent;

    private static ButtonsBehaviour selectedButton;  // Botón seleccionado actualmente

    void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = bgImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedButton != this)
        {
            imageComponent.sprite = bgImageHover;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedButton != this)
        {
            imageComponent.sprite = bgImage;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Desseleccionar el botón previamente seleccionado
        if (selectedButton != null && selectedButton != this)
        {
            selectedButton.imageComponent.sprite = bgImage;
        }

        // Seleccionar este botón
        selectedButton = this;
        imageComponent.sprite = bgImageOnClick;
    }
}