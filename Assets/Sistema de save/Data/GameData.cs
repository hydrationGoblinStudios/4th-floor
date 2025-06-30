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
    public List<int> Inventory;
    public List<int> KeyItems;
    public List<int> StoryFlags;
    public List<string> unlockedMaps;
    public List<UnitData> units;
    public UnitData unitData;

    public GameData()
    {
        money = 0;
        day = 0;
        plantaDia = 40;
        Inventory = new List<int>();
        KeyItems = new List<int>();
        StoryFlags = new List<int>();
        unlockedMaps = new();

        units = new();
    }
}
