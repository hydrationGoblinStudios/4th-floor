using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        gameManager.selectedUB4Activity = gameManager.team[unit].GetComponent<UnitBehavior>();
    }
    public void ChangeActivity(string activity)
    {
        gameManager.selectedUB4Activity.activity = activity;
    }
    public void ActivityBoard()
    {
        
    }
}
