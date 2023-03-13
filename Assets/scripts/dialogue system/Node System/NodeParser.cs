using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;

public class NodeParser : MonoBehaviour
{
    public bool buttonPress;
    public DialogueGraph graph;
    Coroutine _parser;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public Image speakerImage;
    public Button action;
    private void Start()
    {
        foreach(BaseNode b in graph.nodes)
        {
            if(b.GetString() == "Start")
            {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());

    }
    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data  = b.GetString();
        string[] dataParts = data.Split('/');
        if(dataParts[0] == "Start")
        {
            NextNode("exit");
        }
        if (dataParts[0] == "Stop")
        {
            if(action.onClick != null)
            {
                action.onClick.Invoke();
            }
            yield return null;
        }
        if(dataParts[0] == "DialogueNode")
        {
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => buttonPress);
            buttonPress = false;
            NextNode("exit");
        }
    }
    public void NextNode(string fieldName)
    {
        if(_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach (NodePort p in graph.current.Ports)
        {
            if(p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
    public void ButtonPress()
    {
        buttonPress = true;
    }
}
