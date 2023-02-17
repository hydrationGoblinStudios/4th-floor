using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManager : MonoBehaviour
{
    public GameManager Manager;
    public GameObject GameManagerOBJ;
    public UnitBehavior UnitBehavior;
    public void Start()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
    }
    public void Raise()
    {
        UnitBehavior = Manager.playerUnitInstance.GetComponent<UnitBehavior>();
        UnitBehavior.atk += 1;
    }
    public void Equip(Item equipItem)
    {
        UnitBehavior = Manager.playerUnitInstance.GetComponent<UnitBehavior>();
        UnitBehavior.Weapon = equipItem;
    }
}
