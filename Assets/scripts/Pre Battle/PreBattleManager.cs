using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class PreBattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public InventoryManager inventoryManager;
    public Animator animator;
    public GameObject[] enemyList;
    public GameObject[] enemyListRandomP1;
    public GameObject[] enemyListRandomP2;
    public GameObject[] enemyListRandomP3;
    public GameObject PlayerStats;
    public GameObject enemyStats;
    public UnitBehavior selectedUnit;
    public UnitBehavior selectedEnemyUnit;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI[] enemyStatTexts;
    public SpriteRenderer playerMugshot;
    public SpriteRenderer enemyMugshot;
    public List<TextMeshProUGUI> playerWeaponStatText;
    public List<TextMeshProUGUI> enmeyWeaponStatText;
    public List<SpriteRenderer> equipsRenderers;
    public List<SpriteRenderer> enemyEquipsRenderers;
    public TextMeshProUGUI energyText;
    public int energy;
    public int evilEnergy = 4;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI enemyEquipText;
    public TextMeshProUGUI accesoryText;
    public TextMeshProUGUI enemyAccesoryText;
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
        inventoryManager = GameObject.FindAnyObjectByType<InventoryManager>(FindObjectsInactive.Include);
        SelectedPlayer1 = Instantiate(gameManager.team[0], BattleStations[0].transform);
        SelectedPlayer1.name = SelectedPlayer1.GetComponent<UnitBehavior>().UnitName + "Temp";
        SelectedPlayer2 = Instantiate(gameManager.team[1], BattleStations[1].transform);
        SelectedPlayer2.name = SelectedPlayer2.GetComponent<UnitBehavior>().UnitName + "Temp";
        SelectedPlayer3 = Instantiate(gameManager.team[2], BattleStations[2].transform);
        SelectedPlayer3.name = SelectedPlayer3.GetComponent<UnitBehavior>().UnitName + "Temp";
        if (gameManager.testMode)
        {
            SelectedEnemy1 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[3].transform);
            SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
            SelectedEnemy2 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[4].transform);
            SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
            SelectedEnemy3 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[5].transform);
            SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        else
        {
            bool choice = true;
            int week = (gameManager.day / 5) + 1;
            while (choice == true)
            {
               GameObject slot1 = enemyListRandomP1[Random.Range(0, enemyListRandomP1.Length)];
                Debug.Log(slot1.name);
                if (slot1.name.Contains("A1") && choice)
                {
                    Debug.Log(slot1.name +"é do primeiro andar");
                    switch (week)
                    {
                        case 1: if (slot1.name.Contains("S1")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                        case 2: if (slot1.name.Contains("S2")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                        case 3: if (slot1.name.Contains("S3")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                        case 4: if (slot1.name.Contains("S4")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                        case 5: if (slot1.name.Contains("S5")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                       default: if (slot1.name.Contains("S5")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; }  break;
                    }if(SelectedEnemy1 != null)
                    {
                    SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
                    }
                }
            }   
             choice = true;
            while (choice == true)
            {
                GameObject slot1 = enemyListRandomP2[Random.Range(0, enemyListRandomP2.Length)];

                if (slot1.name.Contains("A1") && choice)
                {
                    switch (week)
                    {
                        case 1: if (slot1.name.Contains("S1")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                        case 2: if (slot1.name.Contains("S2")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                        case 3: if (slot1.name.Contains("S3")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                        case 4: if (slot1.name.Contains("S4")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                        case 5: if (slot1.name.Contains("S5")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                       default: if (slot1.name.Contains("S5")) { SelectedEnemy2 = Instantiate(slot1, BattleStations[4].transform); choice = false; } break;
                    }if(SelectedEnemy2 != null)
                    {
                    SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
                    }

                }
            }
            choice = true;

            while (choice == true)
            {
                GameObject slot1 = enemyListRandomP3[Random.Range(0, enemyListRandomP3.Length)];
                if (slot1.name.Contains("A1") && choice)
                {
                    switch (week)
                    {
                        case 1: if (slot1.name.Contains("S1")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                        case 2: if (slot1.name.Contains("S2")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                        case 3: if (slot1.name.Contains("S3")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                        case 4: if (slot1.name.Contains("S4")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                        case 5: if (slot1.name.Contains("S5")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                       default: if (slot1.name.Contains("S5")) { SelectedEnemy3 = Instantiate(slot1, BattleStations[5].transform); choice = false; } break;
                    }
                    if (SelectedEnemy3!=null)
                    {
                    SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";
                    }
                }
            }
        }
        SelectedPlayerList.Add(SelectedPlayer1);
        SelectedPlayerList.Add(SelectedPlayer2);
        SelectedPlayerList.Add(SelectedPlayer3);
        SelectedEnemyList.Add(SelectedEnemy1);
        SelectedEnemyList.Add(SelectedEnemy2);
        SelectedEnemyList.Add(SelectedEnemy3);
        Select1();
        EnemySelect(SelectedEnemy1.GetComponent<UnitBehavior>());
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString(); statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        if(inventoryManager.playableMugShots.Where(obj => obj.name == selectedUnit.name + " mugshot").SingleOrDefault() != null)
        {
            playerMugshot.sprite = inventoryManager.playableMugShots.Where(obj => obj.name == selectedUnit.UnitName + " mugshot").SingleOrDefault();
        }
        else
        {
            playerMugshot.sprite = inventoryManager.playableMugShots[0];
        }
        playerWeaponStatText[0].text = selectedUnit.Weapon.power.ToString();
        playerWeaponStatText[1].text = selectedUnit.Weapon.hit.ToString();
        playerWeaponStatText[2].text = selectedUnit.Weapon.crit.ToString();
        playerWeaponStatText[3].text= selectedUnit.Weapon.name.ToString();
        equipsRenderers[0].sprite = inventoryManager.EquipableImages.Where(obj => obj.name == selectedUnit.Weapon.ItemName).SingleOrDefault();
        if (selectedUnit.Accesory != null)
        {
        playerWeaponStatText[4].text = selectedUnit.Accesory.name.ToString();
        equipsRenderers[1].sprite = inventoryManager.EquipableImages.Where(obj => obj.name == selectedUnit.Accesory.ItemName).SingleOrDefault();
        }
        else
        {
            playerWeaponStatText[4].text = "";
            equipsRenderers[1].sprite = null;
        }

    }
    public void EnemySelect(UnitBehavior unitBehavior)
    {
        selectedEnemyUnit = unitBehavior;
        enemyStatTexts[0].text = selectedEnemyUnit.maxhp.ToString(); enemyStatTexts[1].text = selectedEnemyUnit.str.ToString(); 
        enemyStatTexts[2].text = selectedEnemyUnit.mag.ToString(); enemyStatTexts[3].text = selectedEnemyUnit.dex.ToString(); 
        enemyStatTexts[4].text = selectedEnemyUnit.speed.ToString();enemyStatTexts[5].text = selectedEnemyUnit.def.ToString(); 
        enemyStatTexts[6].text = selectedEnemyUnit.mdef.ToString(); enemyStatTexts[7].text = selectedEnemyUnit.luck.ToString();
        enmeyWeaponStatText[0].text = selectedEnemyUnit.Weapon.power.ToString();
        enmeyWeaponStatText[1].text = selectedEnemyUnit.Weapon.hit.ToString();
        enmeyWeaponStatText[2].text = selectedEnemyUnit.Weapon.crit.ToString();
        enmeyWeaponStatText[3].text = selectedEnemyUnit.Weapon.name.ToString();
        enemyEquipsRenderers[0].sprite = inventoryManager.EquipableImages.Where(obj => obj.name == selectedEnemyUnit.Weapon.ItemName).SingleOrDefault();
        if (selectedEnemyUnit.Accesory != null)
        {
            enmeyWeaponStatText[4].text = selectedEnemyUnit.Accesory.name.ToString();
            enemyEquipsRenderers[1].sprite = inventoryManager.EquipableImages.Where(obj => obj.name == selectedEnemyUnit.Accesory.ItemName).SingleOrDefault();
        }
        else
        {
            enmeyWeaponStatText[4].text = "";
            enemyEquipsRenderers[1].sprite = null;
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
