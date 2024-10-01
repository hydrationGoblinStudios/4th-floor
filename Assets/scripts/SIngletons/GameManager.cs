using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton , IDataPersistence
{
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
    [HideInInspector]public List<Item> ExampleList;

    public void Start()
    {
        LoadTeam();
        moneyText.text = "Dinheiro:"+money;
    }
    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
        this.Inventory = data.Inventory;
        this.KeyItems = data.KeyItems;
        this.team = data.team;
    }
    public void SaveData(ref GameData data)
    {
        data.money = this.money;
        data.day = this.day;
        data.Inventory = this.Inventory;
        data.KeyItems = this.KeyItems;
        data.team = this.team;
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
