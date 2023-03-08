using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;
	public string speaker;
	public string dialogue;
	public Sprite sprite;

    public override string GetString()
    {
		return "DialogueNode/" + speaker+ "/" + dialogue;
    }
    public override Sprite GetSprite()
    {
        return sprite;
    }
}