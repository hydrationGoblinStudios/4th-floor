using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnitBehavior;

[Serializable]
public struct UnitData
{
    [Header("classID")]
    public int classId;
    [Header("Equips")]
    public Item Weapon;
    public Item Accesory;
    public List<Item.Weapontype> UsableWeaponTypes;
    [Header("Parameters")]
    public string UnitName;
    public int currentLevel;
    public float expmarkplier;
    public int currentRank;
    public int currentExp;
    public List<int> ClassID;
    public int[] ClassLevel;
    public Dictionary<int, int> ClassLearning;
    public SeriazableDictionary ClassLearningSerializable;
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
    public float soul;
    public int maxsoul;
    public float soulgain;
    [Header("Status que não aparecem na UI")]
    public int damagereduction;
    public int lifesteal;
    public int armorpen;
    public int magicpen;
    [Header("Cooking")]
    public int cooking;
    [Header("actrivity related")]
    public string activity;
    public int fortalecerStat;
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
