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
    public List<GameObject> team;
    public UnitBehavior selectedPlayerBehavior;
    public UnitBehavior selectedEnemyBehavior;
    public TextMeshPro moneyText;
    public List<Item> Inventory;
    public List<Item> KeyItems;
    public void Start()
    {
        LoadTeam();
        moneyText.text = "Dinheiro:"+money;
    }
    public void PrepScreen()
    {
        SceneManager.LoadScene("Preparation");
    }
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }
    public void SceneLoader(string str)
    {
        SceneManager.LoadScene(str);   
    }
    public void OnLevelWasLoaded()
    {
        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Battle Text");
        moneyText = tempGameObject.GetComponent<TextMeshPro>();
        if (moneyText != null)
        {
            moneyText.text = "Dinheiro:"+money;
        }
    }
    public void LoadTeam()
    {
        foreach(GameObject obj in playerUnit)
        {
            GameObject newobj = Instantiate(obj, this.transform);
            team.Add(newobj);
        }
    }
    public void AddtoTeam(GameObject recruit)
    {
        GameObject newobj = Instantiate(recruit, this.transform);
        team.Add(newobj);
    }
    public void selectUnit(GameObject unit)
    {
        GameObject newunit = unit;
        team.Remove(unit);
        team.Insert(0, newunit);
    }
}
