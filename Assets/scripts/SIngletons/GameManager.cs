using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton
{
    public int money;
    public GameObject playerUnit;
    public GameObject enemyUnit;
    public GameObject playerUnitInstance;
    public GameObject enemyUnitInstance;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public TextMeshPro moneyText;
    public void Start()
    {
        playerUnitInstance = Instantiate(playerUnit);
        enemyUnitInstance = Instantiate(enemyUnit);
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
    public void Raise()
    {
        playerBehavior.atk += 1;
    }
    public void Equip(Item equipItem)
    {
        playerBehavior.Weapon = equipItem;
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
