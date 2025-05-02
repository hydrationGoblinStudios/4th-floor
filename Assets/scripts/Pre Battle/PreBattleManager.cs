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
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI[] enemyStatTexts;
    public TextMeshProUGUI enemyUnitName;
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
    public GameObject StatIcons;
    public int selectedUnitSlot;
    public GameObject ButtonPrefab;

    public SkillManager skillManager;
    public GameObject ActivityPanel;
    public GameObject ItemSelectPanel;
    public GameObject EmptyUnitPrefab;
    void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        inventoryManager = GameObject.FindAnyObjectByType<InventoryManager>(FindObjectsInactive.Include);
        SelectedPlayer1 = Instantiate(gameManager.team[0], BattleStations[0].transform);
        SelectedPlayer1.name = SelectedPlayer1.GetComponent<UnitBehavior>().UnitName + "Temp";
        if (gameManager.team.Count >= 2)
        {
            SelectedPlayer2 = Instantiate(gameManager.team[1], BattleStations[1].transform);
            SelectedPlayer2.name = SelectedPlayer2.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        else
        {
            SelectedPlayer2 = Instantiate(EmptyUnitPrefab, BattleStations[1].transform);
            SelectedPlayer2.name = SelectedPlayer2.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        if (gameManager.team.Count >= 3)
        {
            SelectedPlayer3 = Instantiate(gameManager.team[2], BattleStations[2].transform);
            SelectedPlayer3.name = SelectedPlayer3.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        else
        {
            SelectedPlayer3 = Instantiate(EmptyUnitPrefab, BattleStations[2].transform);
            SelectedPlayer3.name = SelectedPlayer2.GetComponent<UnitBehavior>().UnitName + "Temp";
        }
        playerAnimations[0].runtimeAnimatorController = SelectedPlayer1.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => null,
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
            _ => null,
        }; playerAnimations[2].runtimeAnimatorController = SelectedPlayer3.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => null,
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
                case 101: SpawnTeam(bossBattles.Where(obj => obj.name == "Day5").SingleOrDefault()); break;
                default:
                    SelectedEnemy1 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[3].transform);
                    SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";

                    SelectedEnemy2 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[4].transform);
                    SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";

                    SelectedEnemy3 = Instantiate(enemyList[Random.Range(0, enemyList.Length)], BattleStations[5].transform);
                    SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";

                    break;
            }
        }
        else
        {
            bool choice = true;
            int week = (gameManager.day / 5) + 1;
            while (choice == true)
            {
                GameObject slot1 = enemyListRandomP1[Random.Range(0, enemyListRandomP1.Length)];
                if (slot1.name.Contains("A1") && choice)
                {
                    switch (week)
                    {
                        case 1: if (slot1.name.Contains("S1")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                        case 2: if (slot1.name.Contains("S2")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                        case 3: if (slot1.name.Contains("S3")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                        case 4: if (slot1.name.Contains("S4")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                        case 5: if (slot1.name.Contains("S5")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                        default: if (slot1.name.Contains("S5")) { SelectedEnemy1 = Instantiate(slot1, BattleStations[3].transform); choice = false; } break;
                    } if (SelectedEnemy1 != null)
                    {
                        SelectedEnemy1.name = SelectedEnemy1.GetComponent<UnitBehavior>().UnitName + "Temp";
                    }
                }
            }
            choice = true;
            while (choice == true)
            {
                GameObject slot1 = enemyListRandomP2[Random.Range(0, enemyListRandomP2.Length)];
                if (gameManager.team.Count < 2)
                { SelectedEnemy2 = Instantiate(EmptyUnitPrefab, BattleStations[4].transform); choice = false; }
                else {
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
                        } if (SelectedEnemy2 != null)
                        {
                            SelectedEnemy2.name = SelectedEnemy2.GetComponent<UnitBehavior>().UnitName + "Temp";
                        }
                    }
                }
            }

            choice = true;

            while (choice == true)
            {
                GameObject slot1 = enemyListRandomP3[Random.Range(0, enemyListRandomP3.Length)];
                if (gameManager.team.Count < 3)
                { SelectedEnemy3 = Instantiate(EmptyUnitPrefab, BattleStations[5].transform); choice = false; }
                else
                {
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
                        if (SelectedEnemy3 != null)
                        {
                            SelectedEnemy3.name = SelectedEnemy3.GetComponent<UnitBehavior>().UnitName + "Temp";
                        }
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
                _ => null,
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
                _ => null,
            }; enemyAnimations[2].runtimeAnimatorController = SelectedEnemy3.GetComponent<UnitBehavior>().classId switch
            {
                107 => Animations[6],
                101 => Animations[0],
                102 => Animations[1],
                103 => Animations[2],
                104 => Animations[3],
                105 => Animations[4],
                106 => Animations[5],
                _ => null,
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
            if (c < gameManager.team.Count)
            {


                b.onClick.RemoveAllListeners();
                switch (c)
                {
                    case 0: b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[0]); }); break;
                    case 1: if (gameManager.team.Count >= 2) { b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[1]); }); } break;
                    case 2: if (gameManager.team.Count >= 3) { b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[2]); }); } break;
                    default: if (gameManager.team.Count >= 4) { b.onClick.AddListener(delegate { UnitSelect(selectedUnitSlot, gameManager.team[3]); }); } break;
                }
                if (inventoryManager.playableMugShots.Where(obj => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault() != null)
                {
                    b.transform.GetComponent<Image>().sprite = inventoryManager.playableMugShots.Where(obj => obj.name == gameManager.team[c].GetComponent<UnitBehavior>().UnitName + " mugshot").SingleOrDefault();
                }
                else
                {
                    b.transform.GetComponent<Image>().sprite = inventoryManager.playableMugShots[0];
                }
                c++;
            }
        }
        EnemySelect(SelectedEnemy1.GetComponent<UnitBehavior>());
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString(); statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        unitName.text = selectedUnit.UnitName;
        if (inventoryManager.playableMugShots.Where(obj => obj.name == selectedUnit.UnitName + " mugshot").SingleOrDefault() != null)
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
        playerWeaponStatText[3].text = selectedUnit.Weapon.name.ToString();
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
        switch (selectedUnit.classId)
        {
            case 101:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { AfiarArma(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Afiar arma";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Afiar arma";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta o Próprio Dano em 15% pelo resto do Combate.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { AfiarMente(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Afiar mente";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Afiar mente";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Aumenta o Próprio Acerto em 15 pelo resto do Combate.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 102:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { ForcaDeVontadeAumentada(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Força de vontade Aumentada";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Força de vontade Aumentada";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta o Próprio Ganho de alma por receber dano em 20% pelo resto do combate.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { GolpeSujo(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Golpe sujo";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Golpe sujo";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Golpeia o inimigo na posição da Frente no inicio da batalha.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 103:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { ReforçarArmadura(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Reforçar armadura";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Reforçar armadura";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta Defesa Física de um aliado em 20% pelos primeiros 20 segundos da batalha.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { ReforcarCritico(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Reforçar critico";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Reforçar critico";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Aumenta o Critico de um aliado em 10.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 104:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { ReforcarMagia(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Reforçar magia";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Reforçar magia";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta a Magia de um Aliado em 15%.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { CarregarAlma(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Carregar alma";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Carregar alma";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Começa a Batalha com 50 de Alma Extra.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 105:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { EncantamentoBenevolente(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Encantamento benevolente";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Encantamento benevolente";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta a Velocidade, Acerto e Evasão da equipe inteira em 15% pelos primeiros 10 Segundos do Combate.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { EncantamentoMalevolente(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Encantamento malevolente";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Encantamento malevolente";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Diminui a Velocidade, Acerto e Evasão da equipe inimiga inteira em 10% pelos primeiros 20 Segundos do Combate.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 106:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { MirarNaCabeça(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Mirar na cabeça";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Mirar na cabeça";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Diminui o Próprio Acerto em 20 para Aumentar o Critico em 15.";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { ApontarAberturas(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Apontar abertura";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Apontar abertura";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Aumenta o Acerto da Equipe inteira em 10.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            case 107:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { Camaradagem(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Camaradagem";
                PrepSkills[0].GetComponent<InventoryHoverable>().hoverName = "Camaradagem";
                PrepSkills[0].GetComponent<InventoryHoverable>().description = "Aumenta a Sorte, Destreza e Velocidade de todos os Aliados em 3";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { AprenderComOInimigo(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Aprender com o inimigo";
                PrepSkills[1].GetComponent<InventoryHoverable>().hoverName = "Aprender com o inimigo";
                PrepSkills[1].GetComponent<InventoryHoverable>().description = "Diminui a própria Velocidade e Dano em 50%, mas Recebe mais 30% de experiência a mais nessa luta.";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
            default:
                PrepSkills[0].onClick.RemoveAllListeners();
                PrepSkills[0].onClick.AddListener(delegate { AfiarArma(selectedUnit); });
                PrepSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = "Afiar arma";
                PrepSkills[1].onClick.RemoveAllListeners();
                PrepSkills[1].onClick.AddListener(delegate { AfiarEscudo(selectedUnit); });
                PrepSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = "Afiar mente";
                PrepSkills[2].onClick.RemoveAllListeners();
                return;
        }
        
    }
    public void EnemySelect(UnitBehavior unitBehavior)
    {
        selectedEnemyUnit = unitBehavior;
        enemyStatTexts[0].text = selectedEnemyUnit.maxhp.ToString(); enemyStatTexts[1].text = selectedEnemyUnit.str.ToString();
        enemyStatTexts[2].text = selectedEnemyUnit.mag.ToString(); enemyStatTexts[3].text = selectedEnemyUnit.dex.ToString();
        enemyStatTexts[4].text = selectedEnemyUnit.speed.ToString(); enemyStatTexts[5].text = selectedEnemyUnit.def.ToString();
        enemyStatTexts[6].text = selectedEnemyUnit.mdef.ToString(); enemyStatTexts[7].text = selectedEnemyUnit.luck.ToString();
        enemyUnitName.text = selectedEnemyUnit.UnitName;
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
        if (inventoryManager.playableMugShots.Where(obj => obj.name == selectedEnemyUnit.UnitName + " mugshot").SingleOrDefault() != null)
        {
            enemyMugshot.sprite = inventoryManager.playableMugShots.Where(obj => obj.name == selectedEnemyUnit.UnitName + " mugshot").SingleOrDefault();
        }
        else
        {
            enemyMugshot.sprite = null;
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


    public void InventoryToggleButton()
    {
        InventoryToggle(gameManager.Inventory);
    }
    public void InventoryToggle(List<Item> ItemList)
    {
        StatIcons.SetActive(!StatIcons.activeInHierarchy);
        gameManager.ParseWeaponList();

        while (ItemSelectPanel.transform.childCount > 0)
        {
            DestroyImmediate(ItemSelectPanel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            if (item != selectedUnit.Weapon && item != selectedUnit.Accesory)
            {
                GameObject itemButton = Instantiate(ButtonPrefab, ItemSelectPanel.transform);
                itemButton.GetComponent<Button>().onClick.AddListener(() => Equip(item));
                if (!selectedUnit.UsableWeaponTypes.Contains(item.weapontype) && item.type != Item.Type.accesory && item.type != Item.Type.key)
                {
                    itemButton.GetComponentInChildren<TextMeshProUGUI>().color = new((float)0.6, (float)0.6, (float)0.6, 1);
                }
                itemButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = $"{item.ItemName}";
                itemButton.transform.Find("Power").GetComponent<TextMeshProUGUI>().text = $"{item.power}";
                itemButton.transform.Find("Hit").GetComponent<TextMeshProUGUI>().text = $"{item.hit}";
                itemButton.transform.Find("Crit").GetComponent<TextMeshProUGUI>().text = $"{item.crit}";

                switch (item.weapontype)
                {
                    case Item.Weapontype.Sword:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[0];
                        break;
                    case Item.Weapontype.Lance:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[1];
                        break;
                    case Item.Weapontype.Axe:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[2];
                        break;
                    case Item.Weapontype.Bow:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[3];
                        break;
                    case Item.Weapontype.Tome:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[4];
                        break;
                    case Item.Weapontype.Receptacle:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[5];
                        break;
                    case Item.Weapontype.Accesory:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[6];
                        break;
                }
            }
        }
        if (StatIcons.activeInHierarchy == true)
        {
            while (ItemSelectPanel.transform.childCount > 0)
            {
                DestroyImmediate(ItemSelectPanel.transform.GetChild(0).gameObject);
            }
        }
    }
    public void Equip(Item item)
    {
        if (item.type == Item.Type.weapon)
        {
            if (selectedUnit.UsableWeaponTypes.Contains(item.weapontype))
            {
                selectedUnit.Weapon = item;
            }
        }
        else
        {
            selectedUnit.Accesory = item;
        }
        Select(selectedUnit);
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
    //Habilidades de preparo
    //Espadachim
        public void AfiarArma(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.str += (int)(selectedUnit.str * 0.15);
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
        public void AfiarMente(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.hit += 15;
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    //Arqueiro
    public void MirarNaCabeça(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.hit -= 20;
            selectedUnit.crit += 15;
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    public void ApontarAberturas(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            SelectedPlayer1.GetComponent<UnitBehavior>().hit += 10;
            SelectedPlayer2.GetComponent<UnitBehavior>().hit += 10;
            SelectedPlayer3.GetComponent<UnitBehavior>().hit += 10;
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    //Guerreiro
    public void ForcaDeVontadeAumentada(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.skills.Add("Força de Vontade Aumentada");
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    public void GolpeSujo(UnitBehavior selectedUnit)
    {
        if (energy > 0)
        {
            selectedUnit.skills.Add("Golpe sujo");
            energy--;
            energyText.text = energy.ToString();
            Select(selectedUnit);
        }
    }
    //Soldado
    public void ReforçarArmadura(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                selectedUnit.skills.Add("Reforçar Armadura");
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    public void ReforcarCritico(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                selectedUnit.crit += 10;
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    //Feiticeiro
    public void ReforcarMagia(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                selectedUnit.mag += (int)(selectedUnit.mag / 6.66);
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    public void CarregarAlma(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                selectedUnit.skills.Add("Carregar Alma");
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    //mistico 
    public void EncantamentoBenevolente(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                SelectedPlayer1.GetComponent<UnitBehavior>().skills.Add("Encantamento Benevolente");
                SelectedPlayer2.GetComponent<UnitBehavior>().skills.Add("Encantamento Benevolente");
                SelectedPlayer3.GetComponent<UnitBehavior>().skills.Add("Encantamento Benevolente");
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    public void EncantamentoMalevolente(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                SelectedEnemy1.GetComponent<UnitBehavior>().skills.Add("Encantamento Malevolente");
                SelectedEnemy2.GetComponent<UnitBehavior>().skills.Add("Encantamento Malevolente");
                SelectedEnemy3.GetComponent<UnitBehavior>().skills.Add("Encantamento Malevolente");
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    //prisioneiro
    public void Camaradagem(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                SelectedPlayer1.GetComponent<UnitBehavior>().dex += 3;
                SelectedPlayer1.GetComponent<UnitBehavior>().luck += 3;
                SelectedPlayer1.GetComponent<UnitBehavior>().speed += 3;

                SelectedPlayer2.GetComponent<UnitBehavior>().dex += 3;
                SelectedPlayer2.GetComponent<UnitBehavior>().luck += 3;
                SelectedPlayer2.GetComponent<UnitBehavior>().speed +=3;

                SelectedPlayer3.GetComponent<UnitBehavior>().dex += 3;
                SelectedPlayer3.GetComponent<UnitBehavior>().luck += 3;
                SelectedPlayer3.GetComponent<UnitBehavior>().speed += 3;
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
        }
    }
    public void AprenderComOInimigo(UnitBehavior selectedUnit)
    {
        {
            if (energy > 0)
            {
                selectedUnit.speed -= selectedUnit.speed / 2;
                selectedUnit.expmarkplier += (float)0.25;
                energy--;
                energyText.text = energy.ToString();
                Select(selectedUnit);
            }
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
        if(SelectedPlayer != null && SelectedPlayer.GetComponent<UnitBehavior>().UnitName != SelectedPlayer1.GetComponent<UnitBehavior>().UnitName 
            && SelectedPlayer.GetComponent<UnitBehavior>().UnitName != SelectedPlayer2.GetComponent<UnitBehavior>().UnitName
            && SelectedPlayer.GetComponent<UnitBehavior>().UnitName != SelectedPlayer3.GetComponent<UnitBehavior>().UnitName)
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
            _ => null,
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
            _ => null,
        }; enemyAnimations[2].runtimeAnimatorController = SelectedEnemy3.GetComponent<UnitBehavior>().classId switch
        {
            107 => Animations[6],
            101 => Animations[0],
            102 => Animations[1],
            103 => Animations[2],
            104 => Animations[3],
            105 => Animations[4],
            106 => Animations[5],
            _ => null,
        };
    }
    public void DisplayItemList(List<Item> ItemList)
    {
        gameManager.ParseWeaponList();

        while (inventoryManager.panel.transform.childCount > 0)
        {
            DestroyImmediate(inventoryManager.panel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            if (item != selectedUnit.Weapon && item != selectedUnit.Accesory)
            {
                GameObject itemButton = Instantiate(inventoryManager.ItemButtonPrefab, inventoryManager.ItemSelectPanel.transform);
                itemButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.Equip(item));
                if (!selectedUnit.UsableWeaponTypes.Contains(item.weapontype) && item.type != Item.Type.accesory && item.type != Item.Type.key)
                {
                    itemButton.GetComponentInChildren<TextMeshProUGUI>().color = new((float)0.6, (float)0.6, (float)0.6, 1);
                }
                itemButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = $"{item.ItemName}";
                itemButton.transform.Find("Power").GetComponent<TextMeshProUGUI>().text = $"{item.power}";
                itemButton.transform.Find("Hit").GetComponent<TextMeshProUGUI>().text = $"{item.hit}";
                itemButton.transform.Find("Crit").GetComponent<TextMeshProUGUI>().text = $"{item.crit}";

                switch (item.weapontype)
                {
                    case Item.Weapontype.Sword:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[0];
                        break;
                    case Item.Weapontype.Lance:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[1];
                        break;
                    case Item.Weapontype.Axe:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[2];
                        break;
                    case Item.Weapontype.Bow:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[3];
                        break;
                    case Item.Weapontype.Tome:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[4];
                        break;
                    case Item.Weapontype.Receptacle:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[5];
                        break;
                    case Item.Weapontype.Accesory:
                        itemButton.GetComponentInChildren<Image>().sprite = inventoryManager.sprites[6];
                        break;
                }
            }
        }
    }
}
