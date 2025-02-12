using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class PreBattleManager : MonoBehaviour
{
    //animations
    public RuntimeAnimatorController[] Animations;
    public Animator[] playerAnimations;
    public Animator[] enemyAnimations;

    public GameManager gameManager;
    public InventoryManager inventoryManager;
    public Animator animator;
    public GameObject[] bossBattles;
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
    public List<Button> PrepSkills;
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
    public List<Button> UnitSelectButton;
    public int selectedUnitSlot;

    public SkillManager skillManager;
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
        playerAnimations[0].runtimeAnimatorController = SelectedPlayer1.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        };
        playerAnimations[1].runtimeAnimatorController = SelectedPlayer2.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        }; playerAnimations[2].runtimeAnimatorController = SelectedPlayer3.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        };
        if (gameManager.testMode)
        {
            SelectedEnemy1 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[3].transform);
            SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
            SelectedEnemy2 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[4].transform);
            SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
            SelectedEnemy3 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[5].transform);
            SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        else if (gameManager.BossBattleID != 0)
        {
           
            switch (gameManager.BossBattleID)
            {
                case 101:  SpawnTeam(bossBattles.Where(obj => obj.name == "Day5").SingleOrDefault()); break;
                default:
                    SelectedEnemy1 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[3].transform);
                    SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
                    SelectedEnemy2 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[4].transform);
                    SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
                    SelectedEnemy3 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[5].transform);
                    SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";break;
            }
        }
        else
        {
            bool choice = true;
            int week = (gameManager.day / 5) + 1;
            Debug.Log(week);
            while (choice == true)
            {
               GameObject slot1 = enemyListRandomP1[Random.Range(0, enemyListRandomP1.Length)];
                if (slot1.name.Contains("A1") && choice)
                {
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
            
            enemyAnimations[0].runtimeAnimatorController = SelectedEnemy1.GetComponent<UnitBehavior>().classId switch
            {
                107 => Animations[6],
                101 => Animations[0],
                102 => Animations[1],
                103 => Animations[2],
                104 => Animations[3],
                105 => Animations[4],
                106 => Animations[5],
                _ => Animations[1],
            };
            enemyAnimations[1].runtimeAnimatorController = SelectedEnemy2.GetComponent<UnitBehavior>().classId switch
            {
                107 => Animations[6],
                101 => Animations[0],
                102 => Animations[1],
                103 => Animations[2],
                104 => Animations[3],
                105 => Animations[4],
                106 => Animations[5],
                _ => Animations[1],
            }; enemyAnimations[2].runtimeAnimatorController = SelectedEnemy3.GetComponent<UnitBehavior>().classId switch
            {
                107 => Animations[6],
                101 => Animations[0],
                102 => Animations[1],
                103 => Animations[2],
                104 => Animations[3],
                105 => Animations[4],
                106 => Animations[5],
                _ => Animations[1],
            };
        }
        SelectedPlayerList.Add(SelectedPlayer1);
        SelectedPlayerList.Add(SelectedPlayer2);
        SelectedPlayerList.Add(SelectedPlayer3);
        SelectedEnemyList.Add(SelectedEnemy1);
        SelectedEnemyList.Add(SelectedEnemy2);
        SelectedEnemyList.Add(SelectedEnemy3);
        gameManager.SoulPrice(SelectedEnemy1.GetComponent<UnitBehavior>().equipedSoul, SelectedEnemy1.GetComponent<UnitBehavior>());
        gameManager.SoulPrice(SelectedEnemy2.GetComponent<UnitBehavior>().equipedSoul, SelectedEnemy2.GetComponent<UnitBehavior>());
        gameManager.SoulPrice(SelectedEnemy3.GetComponent<UnitBehavior>().equipedSoul, SelectedEnemy3.GetComponent<UnitBehavior>());
        Select1();
        int c = 0;
        foreach (Button b in UnitSelectButton)
        {
            b.onClick.RemoveAllListeners();
            Debug.Log(gameManager.team[c].name);
            switch (c)
            {
                case 0:b.onClick.AddListener(delegate {UnitSelect(selectedUnitSlot, gameManager.team[0]);});break;
                case 1: b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[1]); }); break;
                case 2: b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[2]); }); break;
                default: b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[3]); }); break;
            }
            if(inventoryManager.playableMugShots.Where(obj => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault() != null)
            {
            b.transform.GetComponent<Image>().sprite = inventoryManager.playableMugShots.Where(obj => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault();
            }
            else
            {
                b.transform.GetComponent<Image>().sprite = inventoryManager.playableMugShots[0];
            }
            Debug.Log("c = " + c);
            c++;
        }
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
        PrepSkills[0].onClick.RemoveAllListeners();
        PrepSkills[0].onClick.AddListener(delegate { AfiarArma(selectedUnit); });
        PrepSkills[1].onClick.RemoveAllListeners();
        PrepSkills[1].onClick.AddListener(delegate { AfiarEscudo(selectedUnit); });
        PrepSkills[2].onClick.RemoveAllListeners();
        PrepSkills[2].onClick.AddListener(delegate { AfiarEsperto(selectedUnit); });
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

    public void EnemySelectButton(int slot)
    {
        switch (slot)
        {
            case 1:
                EnemySelect(SelectedEnemy1.GetComponent<UnitBehavior>());
                break;
            case 2:
                EnemySelect(SelectedEnemy2.GetComponent<UnitBehavior>());
                break;
            default:
                EnemySelect(SelectedEnemy3.GetComponent<UnitBehavior>());
                break;
        }
    }
    public void Select1()
    {
        Select(SelectedPlayer1.GetComponent<UnitBehavior>());
        selectedUnitSlot = 1;
    }
    public void Select2()
    {
        Select(SelectedPlayer2.GetComponent<UnitBehavior>());
        selectedUnitSlot = 2;
    }
    public void Select3()
    {
        Select(SelectedPlayer3.GetComponent<UnitBehavior>());
        selectedUnitSlot = 3;
    }
    public void AfiarArma(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.str += (int) (selectedUnit.str * 0.15);
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    public void AfiarEscudo(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.def += (int)(selectedUnit.def * 0.15);
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    public void AfiarEsperto(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.mag += (int)(selectedUnit.mag * 0.15);
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
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
                    AfiarArma(selectedUnit);
                    break;
                case 1:
                    AfiarEscudo(selectedUnit);
                    break;
                case 2:
                    AfiarEsperto(selectedUnit);
                    break;
                default:
                    break;
            }
            evilEnergy--;
        }
    }
    public void UnitSelect(int i,GameObject SelectedPlayer = null)
    {
        if(SelectedPlayer != null)
        {
            Debug.Log(SelectedPlayer.name);
            selectedUnit = SelectedPlayer.GetComponent<UnitBehavior>();
            switch (i)
            {
                case 1: SelectedPlayer1 = SelectedPlayer; SelectedPlayerList[0] = SelectedPlayer; break;
                case 2: SelectedPlayer2 = SelectedPlayer; SelectedPlayerList[1] = SelectedPlayer; break;
                default: SelectedPlayer3 = SelectedPlayer; SelectedPlayerList[2] = SelectedPlayer; break;
            }
        }
        else { Debug.Log("null SP"); }
    }
    public void InstantiateToGM(List<GameObject> List, List<GameObject> EnemyList)
    {
        foreach (GameObject obj in List)
        {
            GameObject newobj = Instantiate(obj, gameManager.transform);
            newobj.AddComponent<DestroyTemp>();
            CopyComponent(skillManager, newobj);
            newobj.GetComponent<SkillManager>().GetBaseStats();
            gameManager.teamPostPreBattle.Add(newobj);
        }
        foreach (GameObject obj in EnemyList)
        {
            GameObject newobj = Instantiate(obj, gameManager.transform);
            newobj.AddComponent<DestroyTemp>();
            CopyComponent(skillManager, newobj);
            newobj.GetComponent<SkillManager>().GetBaseStats();
            gameManager.enemyTeamPostPreBattle.Add(newobj);
        }
    }
    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
    public void SpawnTeam(GameObject enemyTeam)
    {
        SelectedEnemy1 = enemyTeam.transform.GetChild(0).gameObject;
        SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
        SelectedEnemy2 = enemyTeam.transform.GetChild(1).gameObject;
        SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
        SelectedEnemy3 = enemyTeam.transform.GetChild(2).gameObject;
        SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";
        enemyAnimations[0].runtimeAnimatorController = SelectedEnemy1.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        };
        enemyAnimations[1].runtimeAnimatorController = SelectedEnemy2.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        }; enemyAnimations[2].runtimeAnimatorController = SelectedEnemy3.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => Animations[1],
        };
    }
}
