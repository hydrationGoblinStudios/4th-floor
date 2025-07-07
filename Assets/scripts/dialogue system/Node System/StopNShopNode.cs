using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using XNode;
[NodeTint("#F53E07")]

public class StopNShopNode : BaseNode 
{
    public SpriteRenderer spriteRenderer;
    public GameObject SceneInteractable;
    public GameObject calendario;

    [Input] public int entry;
    public override string GetString()
    {
        GameObject go = GameObject.Find("Dialogue Sprite");
        if (go != null)
        {
            if (calendario == null)
            {
                calendario = FindObjectOfType<CalendarioUI>(true).gameObject;
            }
            if (calendario != null)
            {
                calendario.SetActive(false);
            }
            ShopManager shopManager = FindObjectOfType<ShopManager>(true);
            shopManager.Toggle();
            shopManager.open = true;
            spriteRenderer = go.GetComponent<SpriteRenderer>();
            shopManager.blackMarketOpen = true;
        spriteRenderer.sprite = null;
        }
        return "Stop";
    }
}