using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public int day;
    public int plantaDia;
    public bool storyBattle;
    public List<Item> Inventory;
    public List<Item> KeyItems;
    public List<Item> StoryFlags;
    public List<string> unlockedMaps;
    public List<UnitData> units;
    public UnitData unitData;

    public GameData()
    {
        this.money = 0;
        this.day = 0;
        plantaDia = 40;
        Inventory = new List<Item>();
        KeyItems = new List<Item>();

        units = new();
    }
}
