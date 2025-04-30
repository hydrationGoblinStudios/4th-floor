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
        yield return new WaitForSeconds(1);
        dpm.LoadGame();
        gm.LoadData(dpm.gameData);
        gm.team.Clear();
        gm.LoadDataAsUnit(dpm.gameData.units[0]);       
        gm.PrepScreen();
        
    }
}
