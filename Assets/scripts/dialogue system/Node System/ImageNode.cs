using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public SpriteRenderer spriteRenderer;
    public Sprite image;
    public override string GetString()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Dialogue System Sprite");
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        spriteRenderer.color = new Color(255, 255, 255, 255);
        return "ImageNode/";
    }
}
