using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverShop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public ShopManager SM;
    private void Start()
    {
       SM = FindAnyObjectByType<ShopManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SM.UpdateSprites(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
     
    }
}
