using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GMButtonAssigner : MonoBehaviour
{
    public GameManager gameManager;
    public Button button;
    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    public void Sleep()
    {
        gameManager.Sleep();
    }
    public void SelectUnit(int unit)
    {
        if(gameManager.team[unit] != null && gameManager.team[unit].GetComponent<UnitBehavior>() != null)
        {
        gameManager.selectedUB4Activity = gameManager.team[unit].GetComponent<UnitBehavior>();
        }
        Debug.Log("unitSelected");
    }
    public void ChangeActivity(string activity)
    {
        Debug.Log(activity);
        if(gameManager.selectedUB4Activity != null)
        {
            Debug.Log("ub4A encontrado");
            gameManager.selectedUB4Activity.activity = activity;
        }        
    }
    public void ActivityBoard()
    {
        InventoryManager IM = FindObjectOfType<InventoryManager>(true);
        IM.Activatable = !IM.Activatable;
        MapToggle MT = FindObjectOfType<MapToggle>(true);
        MT.activatable = !MT.activatable;
        GameObject ab = GameObject.FindGameObjectWithTag("Activity Board");
        ab.transform.GetChild(0).gameObject.SetActive(true);
        ab.GetComponentInChildren<ActivityBoardUserInterface>().Mugshot();
    }
}
