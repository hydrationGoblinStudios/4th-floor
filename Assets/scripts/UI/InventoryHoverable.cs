using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class InventoryHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string hoverName;
    public string description;

    public GameObject HoverObject;
    public TextMeshPro[] texts;
    public GameObject inventoryManager;
    public void Start()
    {
        HoverObject = GameObject.FindGameObjectWithTag("Hover Object");
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        texts = HoverObject.GetComponentsInChildren<TextMeshPro>();
        texts[0].text = description;
        texts[1].text = hoverName;
        inventoryManager.GetComponent<InventoryManager>().HoverOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryManager.GetComponent<InventoryManager>().HoverOn = false;
    }
}