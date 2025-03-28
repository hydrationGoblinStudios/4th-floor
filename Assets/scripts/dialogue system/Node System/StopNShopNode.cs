using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StopNShopNode : BaseNode 
{
    public SpriteRenderer spriteRenderer;
    public GameObject SceneInteractable;

    [Input] public int entry;
    public override string GetString()
    {
        GameObject go = GameObject.Find("Dialogue Sprite");
        if (go != null)
        {
            if (SceneInteractable == null)
            {
                SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
            }
            if (SceneInteractable != null)
            {
                foreach (Transform child in SceneInteractable.transform)
                {
                    if (child.name != "shop")
                    {
                        child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
                    }
                }
            }
            ShopManager shopManager = FindObjectOfType<ShopManager>(true);
            shopManager.gameObject.SetActive(!shopManager.gameObject.activeInHierarchy);
            spriteRenderer = go.GetComponent<SpriteRenderer>();
            shopManager.blackMarketOpen = true;
        spriteRenderer.sprite = null;
        }
        return "Stop";
    }
}