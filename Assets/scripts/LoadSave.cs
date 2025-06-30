using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    public DataPersistenceManager dpm;
    public GameManager gm;
    void Start()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        if (!Application.isEditor)
        {
           // StartCoroutine(ForceLoad());
        }
        yield return new WaitForSeconds((float)0.3);
        gm = FindObjectOfType<GameManager>();
        dpm.gameData.StoryFlags.Clear();
        dpm.gameData.Inventory.Clear();
        dpm.gameData.KeyItems.Clear();
        dpm.gameData.unlockedMaps.Clear();
        dpm.LoadGame();
        gm.LoadData(dpm.gameData);
        gm.team.Clear();
        foreach(UnitData unit in dpm.gameData.units)
        {
            StartCoroutine(gm.LoadDataAsUnit(unit));
        }
        gm.PrepScreen();
        
    }
}
