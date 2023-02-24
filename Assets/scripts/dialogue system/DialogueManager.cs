using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField]
    private Queue<string> sentences;
    public Item item;

    private void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        sentences = new Queue<string>();
    }
        
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.name);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        if(item != null)
        {
            gameManager.Inventory.Add(item);
        }
    }
    public void DisplayNextSentence() 
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence  = sentences.Dequeue();
        Debug.Log(sentence);
    }
    void EndDialogue()
    {
        Debug.Log("dialogo acabou");

    }
}
