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
        yield return new WaitForSeconds((float)0.3);
        dpm.LoadGame();
        gm.LoadData(dpm.gameData);
        gm.team.Clear();
        foreach(UnitData unit in dpm.gameData.units)
        {
        gm.LoadDataAsUnit(unit);
        }
        gm.PrepScreen();
        
    }
}
