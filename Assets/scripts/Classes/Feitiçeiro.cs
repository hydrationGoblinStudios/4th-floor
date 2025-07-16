using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feitiçeiro : UnitBehavior
{
    public Feitiçeiro()
    {
        currentRank = 1;
    }
    public void Awake()
    {
        baseSkill = "Presença Inabalável";
        skill1 = "Pancada";
        baseSoul = "Golpe Atordoante";
        soul1 = "Fortificar";
        UsableWeaponTypes = new() { Item.Weapontype.Tome };
    }
}