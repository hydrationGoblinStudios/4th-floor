using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager> , IDataPersistence
{
    UnitData CurrentUnitData;

    public List<UnitData> units;
    public int money;
    public int day;
    public List<GameObject> playerUnit = new();
    public List<GameObject> enemyUnit = new();
    public List<GameObject> team;
    public UnitBehavior selectedPlayerBehavior;
    public UnitBehavior selectedEnemyBehavior;
    public TextMeshPro moneyText;
    public List<Item> Inventory;
    public List<Item> KeyItems;
    public List<string> StoryFlags;
    [Header("ItemParseLists")]
    public List<Item> SwordList;
    public List<Item> LanceList;
    public List<Item> AxeList;
    public List<Item> BowList;
    public List<Item> TomeList;
    public List<Item> ReceptacleList;
    public List<Item> AccesoriesList;
    public List<Item> ExampleList;

    public void Start()
    {
        LoadTeam();
        if(moneyText != null)
        {
        moneyText.text = "Dinheiro:"+money;
        }
    }
    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
        this.Inventory = data.Inventory;
        this.KeyItems = data.KeyItems;
    }
    public void SaveData(ref GameData data)
    {
        int count = 0;
        units = new List<UnitData>();
        foreach(GameObject unitInTeam in team)
        {
            CurrentUnitData = new();
            UnitBehavior CurrentUnitBehavior  = unitInTeam.GetComponent<UnitBehavior>();
            SaveUnitasData(CurrentUnitBehavior);
            Debug.Log(CurrentUnitData.UnitName);
            units.Add(CurrentUnitData);
            count++;
        }
        data.money = this.money;
        data.day = this.day;
        data.Inventory = this.Inventory;
        data.KeyItems = this.KeyItems;
        count = 0;
        data.units  = units;
        Debug.Log(data.units); 
    }
    public void PrepScreen()
    {
        SceneManager.LoadScene("Preparation");
    }
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }
    public void SceneLoader(string str)
    {
        SceneManager.LoadScene(str);   
    }
    public void SaveUnitasData(UnitBehavior CurrentUnitBehavior)
    {
        CurrentUnitData.classId = CurrentUnitBehavior.classId;
        //equip
        if (CurrentUnitBehavior.Weapon != null)
        {
            CurrentUnitData.Weapon = CurrentUnitBehavior.Weapon;
        }
        if (CurrentUnitBehavior.Accesory != null)
        {
            CurrentUnitData.Accesory = CurrentUnitBehavior.Accesory;
        }
        //parameters
        CurrentUnitData.UnitName = CurrentUnitBehavior.UnitName;
        CurrentUnitData.currentLevel = CurrentUnitBehavior.currentLevel;
        CurrentUnitData.expmarkplier = CurrentUnitBehavior.expmarkplier;
        CurrentUnitData.currentRank = CurrentUnitBehavior.currentRank;
        CurrentUnitData.currentExp = CurrentUnitBehavior.currentExp;
        CurrentUnitData.ClassID = CurrentUnitBehavior.ClassID;
        CurrentUnitData.ClassLevel = CurrentUnitBehavior.ClassLevel;
        CurrentUnitData.hit = CurrentUnitBehavior.hit;
        CurrentUnitData.avoid = CurrentUnitBehavior.avoid;
        CurrentUnitData.crit = CurrentUnitBehavior.crit;
        //stats
        CurrentUnitData.maxhp = CurrentUnitBehavior.maxhp;
        CurrentUnitData.hp = CurrentUnitBehavior.hp;
        CurrentUnitData.power = CurrentUnitBehavior.power;
        CurrentUnitData.str = CurrentUnitBehavior.str;
        CurrentUnitData.mag = CurrentUnitBehavior.mag;
        CurrentUnitData.dex = CurrentUnitBehavior.dex;
        CurrentUnitData.def = CurrentUnitBehavior.def;
        CurrentUnitData.mdef = CurrentUnitBehavior.mdef;
        CurrentUnitData.defenses = CurrentUnitBehavior.defenses;
        CurrentUnitData.luck = CurrentUnitBehavior.luck;
        CurrentUnitData.speed = CurrentUnitBehavior.speed;
        //sistema de skills
        CurrentUnitData.skills = CurrentUnitBehavior.skills;
        CurrentUnitData.skillInventory = CurrentUnitBehavior.skillInventory;
        CurrentUnitData.equipedSoul = CurrentUnitBehavior.equipedSoul;
        CurrentUnitData.equippedSoulIsAttack = CurrentUnitBehavior.equippedSoulIsAttack;
        CurrentUnitData.soulInventory = CurrentUnitBehavior.soulInventory;
        CurrentUnitData.soul = CurrentUnitBehavior.soul;
        CurrentUnitData.maxsoul = CurrentUnitBehavior.maxsoul;
        CurrentUnitData.soulgain = CurrentUnitBehavior.soulgain;
        CurrentUnitData.damagereduction = CurrentUnitBehavior.damagereduction;
        CurrentUnitData.lifesteal = CurrentUnitBehavior.lifesteal;
        CurrentUnitData.armorpen = CurrentUnitBehavior.armorpen;
        CurrentUnitData.magicpen = CurrentUnitBehavior.magicpen;
        //cooking
        CurrentUnitData.cooking = CurrentUnitBehavior.cooking;
        //growths
        CurrentUnitData.growths = CurrentUnitBehavior.growths;
        //learnset
        CurrentUnitData.classSkill = CurrentUnitBehavior.classSkill;
        CurrentUnitData.personalSkill = CurrentUnitBehavior.personalSkill;
        CurrentUnitData.baseSkill = CurrentUnitBehavior.baseSkill;
        CurrentUnitData.skill1 = CurrentUnitBehavior.skill1;
        CurrentUnitData.skill2 = CurrentUnitBehavior.skill2;
        CurrentUnitData.skill3 = CurrentUnitBehavior.skill3;
        CurrentUnitData.baseSoul = CurrentUnitBehavior.baseSoul;
        CurrentUnitData.soul1 = CurrentUnitBehavior.soul1;
        CurrentUnitData.soul2 = CurrentUnitBehavior.soul2;
        CurrentUnitData.soul3 = CurrentUnitBehavior.soul3;
        CurrentUnitData.description = CurrentUnitBehavior.description;
    }
    public void OnLevelWasLoaded()
    {
        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Battle Text");
        moneyText = tempGameObject.GetComponent<TextMeshPro>();
        if (moneyText != null)
        {
            moneyText.text = "Dinheiro:"+money;
        }
    }
    public void LoadTeam()
    {
        foreach(GameObject obj in playerUnit)
        {
            GameObject newobj = Instantiate(obj, this.transform);
            team.Add(newobj);
            Debug.Log("unidade adicionada ao time: " + newobj.GetComponent<UnitBehavior>().UnitName);
        }
    }
    public void AddtoTeam(GameObject recruit)
    {
        GameObject newobj = Instantiate(recruit, this.transform);
        team.Add(newobj);
    }
    public void SelectUnit(GameObject unit)
    {
        GameObject newunit = unit;
        team.Remove(unit);
        team.Insert(0, newunit);
    }
    public void ParseWeaponList()
    {
        foreach(Item ExampleItem in ExampleList)
        {
            switch (ExampleItem.weapontype)
            {
                case Item.Weapontype.Sword:
                    SwordList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Sword)
                        {
                            SwordList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Lance:
                    LanceList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Lance)
                        {
                            LanceList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Axe:
                    AxeList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Axe)
                        {
                            AxeList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Bow:
                    BowList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Bow)
                        {
                            BowList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Tome:
                    TomeList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Tome)
                        {
                            TomeList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Receptacle:
                    ReceptacleList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Receptacle)
                        {
                            ReceptacleList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Accesory:
                    AccesoriesList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Accesory)
                        {
                            AccesoriesList.Add(currentItem);
                        }
                    }
                    break;
            }
        }
    }
}
