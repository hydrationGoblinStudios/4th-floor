using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    [Output] public int exit3;
    [Output] public int exit4;
    public string speaker;
    public string dialogue;
    public Sprite sprite;

    public override string GetString()
    {
        return "QuestionNode/" + speaker + "/" + dialogue;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}