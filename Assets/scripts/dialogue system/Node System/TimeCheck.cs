using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TimeCheck : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    public int id;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        int isDay = 0;
            if(Manager.TimeIsDay)
            {
                isDay = 1;
            }
        return "ItemCheck/" + isDay;
    }
}
