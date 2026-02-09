using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DayNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    public int day;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        int IsAfterOrDay = 0;
        if(Manager.day >= day)
        {
            IsAfterOrDay = 1;
        }

        return "ItemCheck/" + IsAfterOrDay;
    }
}
