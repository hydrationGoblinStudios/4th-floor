using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public int hp;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int mdef;
    public int luck;
    public float speed;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Manager.team[0].GetComponent<UnitBehavior>().hp += hp;
        Manager.team[0].GetComponent<UnitBehavior>().str += str;
        Manager.team[0].GetComponent<UnitBehavior>().mag += mag;
        Manager.team[0].GetComponent<UnitBehavior>().dex += dex;
        Manager.team[0].GetComponent<UnitBehavior>().def += def;
        Manager.team[0].GetComponent<UnitBehavior>().mdef += mdef;
        Manager.team[0].GetComponent<UnitBehavior>().luck += luck;
        Manager.team[0].GetComponent<UnitBehavior>().speed += speed;

        return "TrainingNode/";
    }
}
