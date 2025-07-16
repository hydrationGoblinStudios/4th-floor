using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Místico : UnitBehavior
{
    public Místico()
    {
        currentRank = 1;
    }
    public void Awake()
    {
        baseSkill = "Encantamento";
        skill1 = "Maldição";
        baseSoul = "Restauração Espiritual";
        soul1 = "Benção dos Ventos";
        UsableWeaponTypes = new() { Item.Weapontype.Receptacle };

    }
}

