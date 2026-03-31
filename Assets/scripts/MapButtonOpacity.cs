using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButtonOpacity : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
   [SerializeField] Color mapButtonColor;
    [SerializeField] bool hover = false;
    public void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    public void Update()
    {
        if (hover)
        {
            if (mapButtonColor.a <= 2)
            {
                mapButtonColor = new Color(1, 1, 1, (float)(mapButtonColor.a + 0.1));
            }
        }
        else if(mapButtonColor.a >= 0)
        {
            mapButtonColor = new Color(1, 1, 1, (float)(mapButtonColor.a - 0.1));
        }
        gameObject.GetComponent<Image>().color = mapButtonColor;
    }
}
