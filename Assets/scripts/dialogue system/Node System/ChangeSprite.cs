using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public string spriteObject;
    public int targetSprite;
    public override string GetString()
    {
        GameObject go = GameObject.Find(spriteObject);
        go.GetComponent<SpriteChanger>().ChangeSprite(targetSprite);
        return "ChangeSprite/";
    }
}