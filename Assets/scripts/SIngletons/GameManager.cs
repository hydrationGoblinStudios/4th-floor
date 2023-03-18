using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton
{
    public int money;
    public List<GameObject> playerUnit = new();
    public List<GameObject> enemyUnit = new();
    public List<GameObject> teamInstances;
    public GameObject playerUnitInstance;
    public GameObject enemyUnitInstance;
    public UnitBehavior selectedPlayerBehavior;
    public UnitBehavior selectedEnemyBehavior;
    public TextMeshPro moneyText;
    public List<Item> Inventory;
    public void Start()
    {
        LoadTeam();
        playerUnitInstance = Instantiate(teamInstances[0]);
        enemyUnitInstance = Instantiate(enemyUnit[0]);
        selectedPlayerBehavior = playerUnitInstance.GetComponent<UnitBehavior>();
        selectedEnemyBehavior = enemyUnitInstance.GetComponent<UnitBehavior>();
        DontDestroyOnLoad(playerUnitInstance);
        DontDestroyOnLoad(enemyUnitInstance);
        moneyText.text = ""+money;
    }
    public void PrepScreen()
    {
        SceneManager.LoadScene("Preparation");
    }
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void OnLevelWasLoaded()
    {
        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Battle Text");
        moneyText = tempGameObject.GetComponent<TextMeshPro>();
        if (moneyText != null)
        {
            moneyText.text = ""+money;
        }
    }
    public void LoadTeam()
    {
        foreach(GameObject obj in playerUnit)
        {
            teamInstances.Add(Instantiate(obj));
        }
    }
}
