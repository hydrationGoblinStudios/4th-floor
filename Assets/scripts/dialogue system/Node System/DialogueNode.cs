using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
using static XNodeEditor.NodeEditor;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;
    public string speaker;
    [TextArea(5,10)]
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