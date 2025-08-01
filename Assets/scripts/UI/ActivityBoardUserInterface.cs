using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class ActivityBoardUserInterface : MonoBehaviour
{
    public GameManager gameManager;
    public InventoryManager inventoryManager;
    public List<TextMeshProUGUI> activityTexts;
    public List<SpriteRenderer> mugshots;
    public GameObject highlight;
    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        inventoryManager = FindObjectOfType<InventoryManager>(true);
    }
    public void Mugshot()
    {
        int c = 0;
        Debug.Log("mugshot");
        highlight.SetActive(false);
        foreach (GameObject go in gameManager.team)
        {
            SpriteRenderer sr = mugshots[c];
            if (gameManager.team[c] != null &&inventoryManager.playableMugShots.Where(obj => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault() != null && gameManager.team[c] != null)
            {
            sr.sprite = inventoryManager.playableMugShots.Where((obj) => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault();
            }
            else
            {
                sr.sprite = null ;
            }
            c++;
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        GameObject GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        GameManager Manager = GameManagerOBJ.GetComponent<GameManager>();

                Manager.currentState = GameManager.UIState.Available;           
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
        int c = 0;
        foreach (GameObject go in gameManager.team)
        {
            activityTexts[c].text = gameManager.team[c].GetComponent<UnitBehavior>().activity;
            c++;
        }
    }
}
