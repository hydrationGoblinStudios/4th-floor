using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitNode : DialogueNode
{
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public GameObject recruit;

    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Manager.AddtoTeam(recruit);
        return "RecruitNode/";
    }
}
