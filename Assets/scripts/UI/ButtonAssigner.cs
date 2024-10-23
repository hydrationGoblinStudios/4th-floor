using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAssigner : MonoBehaviour
{
    public DialogueGraph graph;
    public NodeParser nodeParser;
    public Button button;
    void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<NodeParser>();
        button.onClick.AddListener( delegate { nodeParser.StartDialogue(graph); });
    }
}
