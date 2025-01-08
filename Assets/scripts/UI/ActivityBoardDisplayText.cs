using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityBoardDisplayText : MonoBehaviour
{
    public GameManager gameManager;
    public List<TextMeshProUGUI> activityTexts;
    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
    }
    void Update()
    {
        int c = 0;
        foreach (TextMeshProUGUI activity in activityTexts)
        {    
            activity.text = gameManager.team[c].GetComponent<UnitBehavior>().activity;
            c++;
        }
    }
}
