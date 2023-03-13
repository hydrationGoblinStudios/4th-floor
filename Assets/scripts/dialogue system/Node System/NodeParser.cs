using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;

public class NodeParser : MonoBehaviour
{
    public int buttonPress = -1;
    public DialogueGraph graph;
    Coroutine _parser;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public Image speakerImage;
    public Button action;
    public GameObject options;

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
            options.SetActive(false);
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => buttonPress != -1);
            buttonPress = -1;
            NextNode("exit");
        }
        if (dataParts[0] == "QuestionNode")
        {
            options.SetActive(true);
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => buttonPress != -1);
            switch (buttonPress)
            {
                case 2:
                    NextNode("exit2");
                    break;
                case 3:
                    NextNode("exit3");
                    break;
                case 4:
                    NextNode("exit4");
                    break;
                default:
                    NextNode("exit");
                    break;
            }
            buttonPress = -1;
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
    public void ButtonPress(int option)
    {
        buttonPress = option;
    }
}
