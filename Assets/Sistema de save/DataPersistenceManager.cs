using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
   public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiplos data instance managers");
        }
        instance = this;
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // loadar save
        if (this.gameData == null)
        {
            Debug.Log("Save File n?o encontrada, iniciando new game");
            NewGame();
        }
        // empurrar save para objetos
    }
    public void SaveGame()
    {

    }
}
