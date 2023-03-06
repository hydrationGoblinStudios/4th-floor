using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Button button;
    public GameManager gameManager;
    public bool weclomeText;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public float textSpeeds;
    public Image headSlot;

    public Dialogue dialogue;

    private int index;
    private void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        textComponent.text = string.Empty;
        nameText.text = string.Empty;
        if (weclomeText)
        {
            StartDialogue();
        }
    }
    public void Proceed()
    {
        if (dialogue.lines.Length > 0)
        {
            if (textComponent.text == dialogue.lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogue.lines[index];
            }
        }
    }
    private void StartDialogue()
    {
        headSlot.sprite = dialogue.head;
        headSlot.color = Color.white;
        index = 0;
        nameText.text = dialogue.Speaker;
        StartCoroutine(TypeLine()); 
        
    }
    IEnumerator TypeLine()
    {  
            foreach (char c in dialogue.lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeeds);
            }
    }
    void NextLine()
    {
        if(index < dialogue.lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Color c = headSlot.color;
            c.a = 0;
            headSlot.color = c;
            button.onClick.Invoke();
            gameObject.SetActive(false);
        }
    }
}