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
        GameObject go = GameObject.Find("Dialogue Sprite");
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.position = Vector3.zero;
        spriteRenderer.color = new Color(255, 255, 255, 255);
        return "ImageNode/";
    }
}
