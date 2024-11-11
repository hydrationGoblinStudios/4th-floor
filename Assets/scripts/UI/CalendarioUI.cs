using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        UIUpdate();
    }
    public void UIUpdate()
    {
        dinheiro.text = gameManager.money.ToString();
        if(gameManager.TimeIsDay)
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
    }
}