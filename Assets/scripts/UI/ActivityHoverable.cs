using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ActivityHoverable : MonoBehaviour
{
    public string hoverName;
    public string description;

    public GameObject hoverObject;
    public TextMeshPro[] texts;
    public bool hoverOn;
    public void Start()
    {
        hoverObject = GameObject.FindGameObjectWithTag("Hover Object");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        texts = hoverObject.GetComponentsInChildren<TextMeshPro>();
        texts[0].text = description;
        texts[1].text = hoverName;
        hoverOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOn = false;
    }
    public void Update()
    {
        TextMeshPro[] texts = hoverObject.GetComponentsInChildren<TextMeshPro>();

        if (hoverOn && texts[1].text != "")
        {
            hoverObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hoverObject.transform.position = new Vector3(hoverObject.transform.position.x, hoverObject.transform.position.y, 0);
        }
        else
        {
            hoverObject.transform.position = new Vector3(100, 200, 0);
        }
    }
}
