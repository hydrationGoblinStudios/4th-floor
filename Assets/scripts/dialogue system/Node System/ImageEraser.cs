using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEraser : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    private SpriteRenderer spriteRenderer;
    public override string GetString()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Dialogue System Sprite");
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255, 255, 255, 0);
        return "ImageNode/";
    }
}
