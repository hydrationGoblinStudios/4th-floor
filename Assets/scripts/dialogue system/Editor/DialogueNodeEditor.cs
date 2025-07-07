using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
using System.Linq;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor
{
    private DialogueNode dialogueNode;
    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if(dialogueNode == null)
        {
            dialogueNode = target as DialogueNode;
        }
        dialogueNode.sprite = (Sprite)EditorGUILayout.ObjectField("Mugshot", dialogueNode.sprite, typeof(Sprite), false);
        serializedObject.Update();
    }
}
