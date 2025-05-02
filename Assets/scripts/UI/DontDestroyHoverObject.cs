using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DontDestroyHoverObject : Singleton<DontDestroyHoverObject>
{
    [Header("Hover Object")]
    public TextMeshPro[] texts;
    public GameObject hoverObject;
    public bool HoverOn;
    public void FollowMouse()
    {
        if (hoverObject == null)
        {
            hoverObject = GameObject.FindGameObjectWithTag("Hover Object");
            texts = hoverObject.GetComponentsInChildren<TextMeshPro>();
        }

        if (HoverOn && texts[1].text != "")
        {
            hoverObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hoverObject.transform.position = new Vector3(hoverObject.transform.position.x, hoverObject.transform.position.y, 0);
        }
        else
        {
            hoverObject.transform.position = new Vector3(100, 200, 0);
        }
    }
    public void Update()
    {
        FollowMouse();
    }
}
