using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
            nodeParser.StartDialogue(interactions[nodeParser.hoverGraph]);
            }
            isDragging = false;
            Destroy(gameObject);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouseOver = false;
    }
}