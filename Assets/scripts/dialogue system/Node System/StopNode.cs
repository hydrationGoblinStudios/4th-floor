using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StopNode : BaseNode 
{
    public SpriteRenderer spriteRenderer;
    [Input] public int entry;
    public override string GetString()
    {
        GameObject go = GameObject.Find("Dialogue Sprite");
        if (go != null)
        {
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
        }
        return "Stop";
    }
}