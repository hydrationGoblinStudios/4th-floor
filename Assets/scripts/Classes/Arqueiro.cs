using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiro : UnitBehavior
{
   public Arqueiro()
    {
        currentRank = 1;
        ClassGrowths = new() { 0, 0, 0, 5, 5, 0, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    public void Awake()
    {
     baseSkill = "Foco";
     skill1 = "Precisão Mortal";
     baseSoul = "Tiro Certeiro";
     soul1 = "Rajada de Flechas";   
     UsableWeaponTypes = new() { Item.Weapontype.Bow };
        ClassGrowths = new() { 0, 0, 0, 5, 5, 0, 0, 0 };
        classStats = new() { 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}