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
        if(gameManager.team.Count > unit && gameManager.team[unit].GetComponent<UnitBehavior>() != null)
        {
        gameManager.selectedUB4Activity = gameManager.team[unit].GetComponent<UnitBehavior>();
            GameObject ab = GameObject.FindGameObjectWithTag("Activity Board");
            ab.GetComponentInChildren<ActivityBoardUserInterface>().highlight.transform.parent = transform;
            ab.GetComponentInChildren<ActivityBoardUserInterface>().highlight.transform.localPosition = Vector3.zero;
            ab.GetComponentInChildren<ActivityBoardUserInterface>().highlight.SetActive(true);
        }
        Debug.Log("unitSelected");
    }
    public void ChangeActivity(string activity)
    {
        if(gameManager.selectedUB4Activity != null)
        {
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
        ConfigToggle CT = FindObjectOfType<ConfigToggle>(true);
        CT.activatable = false;
    }
}
