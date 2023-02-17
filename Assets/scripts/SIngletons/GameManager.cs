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
    public GameObject playerUnitInstance;
    public GameObject enemyUnitInstance;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public TextMeshPro moneyText;
    public void Start()
    {
        playerUnitInstance = Instantiate(playerUnit[0]);
        enemyUnitInstance = Instantiate(enemyUnit[0]);
        playerBehavior = playerUnitInstance.GetComponent<UnitBehavior>();
        enemyBehavior = enemyUnitInstance.GetComponent<UnitBehavior>();
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
}
