using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreBattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public Animator animator;
    public GameObject[] enemyList;
    public GameObject PlayerStats;
    public UnitBehavior selectedUnit;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI energyText;
    public int energy;
    public int evilEnergy = 4;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI accesoryText;
    public GameObject[] BattleStations;

    public GameObject SelectedPlayer1;
    public GameObject SelectedPlayer2;
    public GameObject SelectedPlayer3;
    public List<GameObject> SelectedPlayerList;
    public GameObject SelectedEnemy1;
    public GameObject SelectedEnemy2;
    public GameObject SelectedEnemy3;
    public List<GameObject> SelectedEnemyList;
    void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();

        SelectedPlayer1 = Instantiate(gameManager.team[0], BattleStations[0].transform);
        SelectedPlayer2 = Instantiate(gameManager.team[1], BattleStations[1].transform);
        SelectedPlayer3 = Instantiate(gameManager.team[2], BattleStations[2].transform);
        SelectedEnemy1 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[3].transform);
        SelectedEnemy2 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[4].transform);
        SelectedEnemy3 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[5].transform);
        SelectedPlayerList.Add(SelectedPlayer1);
        SelectedPlayerList.Add(SelectedPlayer2);
        SelectedPlayerList.Add(SelectedPlayer3);
        SelectedEnemyList.Add(SelectedEnemy1);
        SelectedEnemyList.Add(SelectedEnemy2);
        SelectedEnemyList.Add(SelectedEnemy3);
        Select1();
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString(); statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        equipText.text = "Arma:\n" + unitBehavior.Weapon.ItemName + "\nAtk:" + unitBehavior.Weapon.str + "\nAcerto:" + unitBehavior.Weapon.hit + "\nCrit:" + unitBehavior.Weapon.crit;
        if (unitBehavior.Accesory != null)
        {
            accesoryText.text = "Accesorio:\n" + unitBehavior.Accesory.ItemName + "\nAtk:" + unitBehavior.Accesory.str + "\nDef:" + unitBehavior.Weapon.def + "\nDes:" + unitBehavior.Weapon.dex + "\nSorte:" + unitBehavior.Weapon.luck + "\nvel:" + unitBehavior.Weapon.speed;
        }
        else
        {
            accesoryText.text = "";
        }
    }
    public void Select1()
    {
        Select(SelectedPlayer1.GetComponent<UnitBehavior>());
    }
    public void Select2()
    {
        Select(SelectedPlayer2.GetComponent<UnitBehavior>());
    }
    public void Select3()
    {
        Select(SelectedPlayer3.GetComponent<UnitBehavior>());
    }
    public void AfiarArma()
    {
        if (energy > 0)
        {
            selectedUnit.str += 4;
            energy--;
            energyText.text = energy.ToString();
        }
    }
    public void AfiarEscudo()
    {
        if (energy > 0)
        {
            selectedUnit.def += 4;
            energy--;
            energyText.text = energy.ToString();
        }
    }
    public void AfiarEsperto()
    {
        if (energy > 0)
        {
            selectedUnit.mag += 4;
            energy--;
            energyText.text = energy.ToString();
        }
    }
    public void ExportTeamToBattle()
    {
        EnemyPrepSkill();
        foreach (GameObject GO in gameManager.teamPostPreBattle)
        {
            Destroy(GO);
        }
        foreach (GameObject GO in gameManager.enemyTeamPostPreBattle)
        {
            Destroy(GO);
        }
        InstantiateToGM(SelectedPlayerList, SelectedEnemyList);
           
        gameManager.SceneLoader("Battle");
    }
    public void EnemyPrepSkill()
    {
        while (evilEnergy > 0)
        {
            int r = Random.Range(0, 3);
            int unitR = Random.Range(0, 3);
            selectedUnit = SelectedEnemyList[unitR].GetComponent<UnitBehavior>();
            switch (r)
            {
                case 0:
                    AfiarArma();
                    break;
                case 1:
                    AfiarEscudo();
                    break;
                case 2:
                    AfiarEsperto();
                    break;
                default:
                    break;
            }
            evilEnergy--;
        }
    }
    public void InstantiateToGM(List<GameObject> List, List<GameObject> EnemyList)
    {
        foreach (GameObject obj in List)
        {
            GameObject newobj = Instantiate(obj, gameManager.transform);
            newobj.AddComponent<DestroyTemp>();
            gameManager.teamPostPreBattle.Add(newobj);
        }
        foreach (GameObject obj in EnemyList)
        {
            GameObject newobj = Instantiate(obj, gameManager.transform);
            newobj.AddComponent<DestroyTemp>();
            gameManager.enemyTeamPostPreBattle.Add(newobj);
        }
    }
}
