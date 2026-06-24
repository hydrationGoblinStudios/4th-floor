using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public int Valor;
    public bool Lumenita;
    private GameManager Manager;
    private GameObject GameManagerOBJ;
    public override string GetString()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (Lumenita)
        {
            Manager.lumenita += Valor;
        }
        else
        {
            Manager.money += Valor;
        }
        return "ItemNode/";
    }
}
