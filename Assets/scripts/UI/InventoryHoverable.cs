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
    public bool activate = false;
    public void Start()
    {
        HoverObject = GameObject.FindGameObjectWithTag("Hover Object");
        inventoryManager = FindObjectOfType<InventoryManager>(true).gameObject;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        activate = true;
        StartCoroutine(ShowDelay());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        activate = false;

        HoverObject.GetComponent<DontDestroyHoverObject>().HoverOn = false;
    }
    IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(1);
        if(activate)
        {
        texts = HoverObject.GetComponentsInChildren<TextMeshPro>();
        texts[0].text = description;
        texts[1].text = hoverName;
        HoverObject.GetComponent<DontDestroyHoverObject>().HoverOn = true;
        }
    }
    public void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            activate = false;
        }   
    }
}
