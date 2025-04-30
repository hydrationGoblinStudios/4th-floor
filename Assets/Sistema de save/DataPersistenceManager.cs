using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public GameData gameData;

    public List<GameObject> ClassList;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
   public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiplos data instance managers");
        }
        instance = this;
    }
    private void Start()
    {
        if (Application.isEditor)
        {
            fileName = "AlvorecerLunarEditor.json";
        }
        else
        {
            fileName = "AlvorecerLunarEditor.json";
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataperistenceObjects();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = new GameData();
        this.gameData = dataHandler.Load();
        // loadar save
        if (this.gameData == null)
        {
            Debug.Log("Save File n?o encontrada, iniciando new game");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        dataHandler.Save(gameData);
    }
    public void SaveGame()
    {
        this.gameData = new GameData();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    private List<IDataPersistence> FindAllDataperistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }   

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NewGame();
            Debug.Log("newGame");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveGame();
            Debug.Log(gameData.money);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
            Debug.Log(gameData.money);
        }
    }
}
