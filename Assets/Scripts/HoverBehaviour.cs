using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBehaviour : MonoBehaviour
{
    [Header("Hover Animation")]
    [SerializeField] private GameObject bgPanel;
    [SerializeField] private float distancePercentage = 0.75f;
    [SerializeField] private float moveSpeed = 5f;
    private bool isHoverHide = true;

    private Vector3 initialPosition;
    private Vector3 actualPosition;
    private Vector3 targetPosition;

    [Header("Game Screens")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftPanel;
    [SerializeField] private GameObject combatPanel;

    public static HoverBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        initialPosition = transform.position;
        actualPosition = initialPosition;
        
        CalculateTargetPosition();
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, actualPosition, moveSpeed * Time.deltaTime);
    }

    public void ShowHideHover()
    {
        if (actualPosition != initialPosition)
        {
            actualPosition = initialPosition;
            isHoverHide = true;
            bgPanel.SetActive(false);
        }
        else
        {
            actualPosition = targetPosition;
            isHoverHide = false;
            bgPanel.SetActive(true);
        }
    }

    public bool GetHoverHide()
    {
        return isHoverHide;
    }

    private void CalculateTargetPosition()
    {
        float screenWidth = Screen.width;
        float distanceInPixels = screenWidth * distancePercentage;
        targetPosition = new Vector3(initialPosition.x, initialPosition.y + distanceInPixels, initialPosition.z);
    }

    public void SelectInventory()
    {
        inventoryPanel.SetActive(true);
        combatPanel.SetActive(false);
        craftPanel.SetActive(false);
    }

    public void SelectCombat()
    {
        combatPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        craftPanel.SetActive(false);
    }

    public void SelectCraft()
    {
        craftPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        combatPanel.SetActive(false);
    }
}
