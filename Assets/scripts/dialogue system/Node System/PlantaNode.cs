using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Manager.plantaDia = Manager.day;
        return "ItemNode/";
    }
}
