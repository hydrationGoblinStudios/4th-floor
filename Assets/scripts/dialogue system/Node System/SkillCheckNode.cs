using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    [Output] public int exit2;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public int DifficultyClass;
    public string stat;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        UnitBehavior character = Manager.team[0].GetComponent<UnitBehavior>();
        int PassedNode = 1;
        float r = Random.Range(0, 100);
        Debug.Log(r);
                switch (stat)
                {
                    case "maxHp": r += character.maxhp; break;
                    case "str": r += character.str; break;
                    case "mag": r += character.mag; break;
                    case "dex": r += character.dex; break;
                    case "def": r += character.def; break;
                    case "mdef": r += character.mdef; break;
                    case "speed": r += character.speed; break;
                    case "luck": r += character.luck; break;
                }                 
            if (r >= DifficultyClass)
        {
            PassedNode = 0;
        }
        return "ItemCheck/"+PassedNode;
    }
}