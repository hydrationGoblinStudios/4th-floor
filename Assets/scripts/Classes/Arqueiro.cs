using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiro : UnitBehavior
{
    public void Awake()
    {
     baseSkill = "Foco";
     skill1 = "Precisão Mortal";
     baseSoul = "Tiro Certeiro";
     soul1 = "Rajada de Flechas";   
     UsableWeaponTypes = new() { Item.Weapontype.Bow };
    }
    

}

