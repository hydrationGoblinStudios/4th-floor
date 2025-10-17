using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;
using Unity.VisualScripting;

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

    public GameObject TextBox;
    public float cooldown = 0.3f;
    public float currentCooldown = 0.3f;

    public bool banText = false;
    public bool welcometext;

    public List<string> dialogueHistory = new();
    public List<Sprite> spriteHistory;
    public GameObject dialogueHistoryTarget;

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

        if (dataParts[0] == "Start")
        {
            interactables.SetActive(false);
            UserInterface.SetActive(true);
            dialogueHistory.Clear();
            spriteHistory.Clear();
            NextNode("exit");
        }
        else if (dataParts[0] == "Stop" || dataParts[0] == null)
        {
            interactables.SetActive(true);
            if (action.onClick != null)
            {
                action.onClick.Invoke();
            }
            foreach (GameObject fakeOption in fakeOptions)
            {
                fakeOption.SetActive(false);
            }
            UserInterface.SetActive(false);
            yield return null;
        }
        else if (dataParts[0] == "DialogueNode")
        {
            extraTextObj.SetActive(false);
            foreach (GameObject gameOBJ in options)
            {
                gameOBJ.SetActive(true);
            }
            foreach (GameObject gameOBJ in fakeOptions)
            {
                gameOBJ.SetActive(false);
            }
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            dialogueHistory.Add(data);
            spriteHistory.Add(b.GetSprite());
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
                if (counter == 0)
                {
                    gameOBJ.SetActive(true);
                    fakeOptions[0].SetActive(true);
                }
                if (counter >= 1)
                {
                    if (extraTexts[counter - 1].text != "")
                    {
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
            spriteHistory.Add(b.GetSprite());
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
                    string questionData = "question/";
                    questionData += speaker.text + "/";
                    Debug.Log(questionData);
                    questionData += extraTexts[1].text;
                    dialogueHistory.Add(questionData);
                    NextNode("exit2");
                    break;
                case 3:
                    buttonPress = -1;
                    questionData = "question/";
                    questionData += speaker.text + "/";
                    Debug.Log(questionData);
                    questionData += extraTexts[1].text;
                    dialogueHistory.Add(questionData);
                    NextNode("exit3");
                    break;
                case 4:
                    buttonPress = -1;
                    questionData = "question/";
                    questionData += speaker.text + "/";
                    Debug.Log(questionData);
                    questionData += extraTexts[1].text;
                    dialogueHistory.Add(questionData);
                    NextNode("exit4");
                    break;
                default:
                    buttonPress = -1;
                    questionData = "question/";
                    questionData += speaker.text + "/";
                    Debug.Log(questionData);
                    questionData += dialogue.text;
                    dialogueHistory.Add(questionData);
                    NextNode("exit");

                    break;
            }
        }
        else if (dataParts[0] == "ItemCheck")
        {
            if (dataParts[1] == "1")
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
        foreach (Transform child in dialogueHistoryTarget.transform)
        {
            Destroy(child.gameObject);
        }
            StartCoroutine(DelayOptions());
            if (_parser != null)
            {
                StopCoroutine(_parser);
                _parser = null;
            }
            foreach (NodePort p in graph.current.Ports)
            {
                if (p.fieldName == fieldName)
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
    IEnumerator DelayOptions()
    {
        foreach (GameObject go in options)
        {
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        yield return new WaitForSeconds(0.2f);
        int c = 0;
        foreach (GameObject go in options)
        {
            go.GetComponent<Button>().onClick.AddListener(() => ButtonPress(c));
        }
    }
    public void ShowHistory()
    {
        int c = 0;
        foreach(string data in dialogueHistory)
        {
            string[] dataParsed = data.Split("/");
            Debug.Log($"{dataParsed[1]}: {dataParsed[2]}");
            GameObject textHistObj = Instantiate(TextBox, dialogueHistoryTarget.transform);
            Debug.Log(textHistObj.name);
            TextMeshProUGUI tmp = textHistObj.transform.GetChild(0).Find("text").GetComponent<TextMeshProUGUI>();
            tmp.text = dataParsed[2];
            tmp = textHistObj.transform.GetChild(0).Find("name text").GetComponent<TextMeshProUGUI>();
            tmp.text = dataParsed[1];
            textHistObj.transform.GetChild(0).Find("head").GetComponent<Image>().sprite = spriteHistory[c];
            c++;
        }
    }

    public void StartDialogue(DialogueGraph NewGraph)
    {
        if (!banText)
        {
            StartCoroutine(DelayOptions());
                /* InventoryManager IM = FindObjectOfType<InventoryManager>(true);
                 IM.Activatable = !IM.Activatable;
                 MapToggle MT = FindObjectOfType<MapToggle>(true);
                 MT.activatable = !MT.activatable;*/
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

}
