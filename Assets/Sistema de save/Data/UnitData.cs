using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct UnitData
{
    [Header("classID")]
    public int classId;
    [Header("Equips")]
    public Item Weapon;
    public Item Accesory;
    [Header("Parameters")]
    public string UnitName;
    public int currentLevel;
    public int expmarkplier;
    public int currentRank;
    public int currentExp;
    public int[] ClassID;
    public int[] ClassLevel;
    public int hit;
    public int avoid;
    public int crit;
    [Header("Stats")]
    public int maxhp;
    public int hp;
    public int power;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int mdef;
    public int[] defenses;
    public int luck;
    public float speed;
    [Header("Sistema de skill")]
    public List<string> skills;
    public List<string> skillInventory;
    public string equipedSoul;
    public bool equippedSoulIsAttack;
    public List<string> soulInventory;
    public int soul;
    public int maxsoul;
    public int soulgain; [Header("Status que não aparecem na UI")]
    public int damagereduction;
    public int lifesteal;
    public int armorpen;
    public int magicpen;
    [Header("Cooking")]
    public int cooking;
    [Header("Growths")]
    public List<int> growths;
    [Header("Learn Set")]
    public string classSkill;
    public string personalSkill;
    public string baseSkill;
    public string skill1;
    public string skill2;
    public string skill3;
    public string baseSoul;
    public string soul1;
    public string soul2;
    public string soul3;
    public string description;
}
