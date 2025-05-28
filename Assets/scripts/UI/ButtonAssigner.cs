using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAssigner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DialogueGraph graph;
    public NodeParser nodeParser;
    public Button button;
    void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<NodeParser>();
        if(graph != null)
        {
            Debug.Log(this.name +" button assigned");
        button.onClick.AddListener( delegate { nodeParser.StartDialogue(graph); });
        }
    }
    public void AddListener()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { nodeParser.StartDialogue(graph); });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        nodeParser.hoverGraph = graph;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        nodeParser.hoverGraph = null;
    }
}
