using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class DragAndDroppable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Canvas canvas;

    public List<DialogueGraph> dialogueGraphTarget;
    public List<DialogueGraph> dialogueGraphs;
    public Dictionary<DialogueGraph, DialogueGraph> interactions = new();
    public NodeParser nodeParser;


    private bool isDragging;

    public void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<NodeParser>();
        canvas = gameObject.GetComponentInParent<Canvas>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        int c = 0;
        foreach(DialogueGraph Target in dialogueGraphTarget)
        {
            interactions.Add(dialogueGraphTarget[c], dialogueGraphs[c]);
            c++;
        }
        nodeParser.banText = true;
        isDragging = true;
    }
    private void Update()
    {
        if (isDragging)
        {
            transform.position = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 60, Input.mousePosition.y + 60, canvas.planeDistance));
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isDragging == true && nodeParser.hoverGraph != null)
        {
            if (nodeParser.hoverGraph != null  && interactions.ContainsKey(nodeParser.hoverGraph))
            {
                nodeParser.banText = false;
            nodeParser.StartDialogue(interactions[nodeParser.hoverGraph]);
            }
            isDragging = false;
            StartCoroutine(TurnOnNodeParser());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
            StartCoroutine(TurnOnNodeParser());
                }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouseOver = false;
    }

    IEnumerator TurnOnNodeParser()
    {
        gameObject.GetComponent<Image>().color = new(0, 0, 0, 0);
        yield return new WaitForSeconds((float)0.2);
        nodeParser.banText = false;
        Destroy(gameObject);
    }
}