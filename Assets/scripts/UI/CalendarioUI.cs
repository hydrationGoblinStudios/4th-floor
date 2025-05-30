using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CalendarioUI : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer Calendario;
    public SpriteRenderer Tempo;
    public TextMeshPro dinheiro;
    public TextMeshPro[] dias;
    public Sprite[] tempos;
    public Sprite[] batalhas;
    public void Start()
    {
       // StartCoroutine(GetafterSingletonCleanse());
       // StartCoroutine(UIUpdate());
    }
    public void OnEnable()
    {
        StartCoroutine(GetafterSingletonCleanse());
        StartCoroutine(UIUpdate());
    }
    public IEnumerator GetafterSingletonCleanse()
    {
        yield return new WaitForEndOfFrame();
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
    }
    public IEnumerator UIUpdate()
    {
        yield return new WaitForEndOfFrame();
        dinheiro.text = gameManager.money.ToString();      
        if(gameManager.storyBattle)
        {
            Tempo.sprite = tempos[0];
        }
        else
        {
            Tempo.sprite = tempos[1];
        }
        int c = 0;
        foreach(TextMeshPro tmp in dias)
        {
            tmp.text = (gameManager.day + c).ToString();
            c++;
        }
        yield return new WaitForSeconds(1);     
       StartCoroutine(UIUpdate());        
    }
}