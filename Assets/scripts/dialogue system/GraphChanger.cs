using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphChanger : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public string buttonAssigner;
    public DialogueGraph dialogueGraph;

    public override string GetString()
    {
        GameObject go = FindObjectOfType<SceneInteractables>(true).gameObject;
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().graph = dialogueGraph;
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().AddListener();
        return "GraphChanger/";
    }
}