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
    public DialogueGraph hoverGraph;
    Coroutine _parser;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public Image speakerImage;
    public Button action;
    public GameObject fakeptions;
    public GameObject UserInterface;
    public GameObject interactables;
    public GameObject extraTextObj;
    public TextMeshProUGUI[] extraTexts;
    public GameObject[] options;
    public GameObject[] fakeOptions;

    public bool welcometext;

    private void Start()
    {
        if (welcometext)
        {
            StartDialogue(graph);
        }
    }
    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data  = b.GetString();
        string[] dataParts = data.Split('/');
       
        if(dataParts[0] == "Start")
        {
            interactables.SetActive(false);
            UserInterface.SetActive(true);
            NextNode("exit");
        }
        else if (dataParts[0] == "Stop")
        {
            interactables.SetActive(true);
            if(action.onClick != null)
            {
                action.onClick.Invoke();
            }
            foreach(GameObject fakeOption in fakeOptions)
            {
                fakeOption.SetActive(false);
            }
            UserInterface.SetActive(false);
            yield return null;
        }
        else if(dataParts[0] == "DialogueNode")
        {
            extraTextObj.SetActive(false);
            foreach (GameObject gameOBJ in options)
            {
                gameOBJ.SetActive(true);
            }
            foreach(GameObject gameOBJ in fakeOptions)
            {
                gameOBJ.SetActive(false);
            }
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            if(speakerImage.sprite == null)
            {
                speakerImage.color = new Color(255, 255, 255, 0);
            }
            else
            {
                speakerImage.color = new Color(255, 255, 255, 255);
            }
            yield return new WaitUntil(() => buttonPress != -1);
            buttonPress = -1;
            NextNode("exit");
        }
        else if (dataParts[0] == "QuestionNode")
        {
            int counter = 0;
            extraTextObj.SetActive(true);
            
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            QuestionNode questionNode = (QuestionNode)b;
            extraTexts[0].text = questionNode.option2;
            extraTexts[1].text = questionNode.option3;
            extraTexts[2].text = questionNode.option4;
            foreach (GameObject gameOBJ in options)
            {
                if(counter == 0)
                {
                    gameOBJ.SetActive(true);
                    fakeOptions[0].SetActive(true);
                }
                if (counter >= 1)
                {
                    if (extraTexts[counter - 1].text != "")
                    {
                        Debug.Log(extraTexts[counter - 1].text);
                        options[counter].SetActive(true);
                        fakeOptions[counter].SetActive(true);
                    }
                    else
                    {
                        options[counter].SetActive(false);
                        fakeOptions[counter].SetActive(false);
                    }
                }
                counter++;
            }
            speakerImage.sprite = b.GetSprite();
            if (speakerImage.sprite == null)
            {
                speakerImage.color = new Color(255, 255, 255, 0);
            }
            else
            {
                speakerImage.color = new Color(255, 255, 255, 255);
            }
            yield return new WaitUntil(() => buttonPress != -1);
            switch (buttonPress)
            {
                case 2:
                    buttonPress = -1;
                    NextNode("exit2");
                    break;
                case 3:
                    buttonPress = -1;
                    NextNode("exit3");
                    break;
                case 4:
                    buttonPress = -1;
                    NextNode("exit4");
                    break;
                default:
                    buttonPress = -1;
                    NextNode("exit");
                    break;
            }
        }
        else if (dataParts[0] == "ItemCheck")
        {
            if(dataParts[1] == "1")
            {
                NextNode("exit");
            }
            else
            {
                NextNode("exit2");
            }
        }
        else
        {
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
    public void StartDialogue(DialogueGraph NewGraph)
    {
        hoverGraph = null;
        graph = NewGraph;
        foreach (BaseNode b in graph.nodes)
        {
            if (b.GetString() == "Start")
            {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }

}
