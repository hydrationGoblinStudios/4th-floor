using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public string map;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Manager.unlockedMaps.Add(map);
        return "ItemNode/";
    }
}
