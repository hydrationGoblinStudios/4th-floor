using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DayResultsManager : MonoBehaviour
{
    public SpriteRenderer[] mugShots;
    public TextMeshProUGUI[] Texts;
    public GameManager gameManager;
    public InventoryManager inventoryManager;
    public GameObject Background;

    public void Sleep()
    {
        Background.SetActive(true);
        gameManager = FindObjectOfType<GameManager>(true);
        inventoryManager = FindObjectOfType<InventoryManager>(true);
        gameManager.day += 1;
        int c = 0;
        foreach (GameObject unit in gameManager.team)
        {
            UnitBehavior currentUnit = unit.GetComponent<UnitBehavior>();
            Debug.Log(currentUnit.UnitName);
            if (inventoryManager.playableMugShots.Where(obj => obj.name == currentUnit.UnitName + " mugshot").SingleOrDefault() != null)
            {
            mugShots[c].sprite = inventoryManager.playableMugShots.Where(obj => obj.name == currentUnit.UnitName + " mugshot").SingleOrDefault();
            }
            else
            {
                mugShots[c].sprite = inventoryManager.playableMugShots[0];
            }
            string activity = unit.GetComponent<UnitBehavior>().activity;
            switch (activity)
            {
                case "homer":
                    Texts[c].text = "simpon";
                    break;
                case "Treinar":
                    currentUnit.currentExp += 25;
                    if (currentUnit.currentExp >= 99)
                    {
                        currentUnit.currentExp = 99;
                    }
                    Texts[c].text = "+25 XP";
                    break;
                case "Fortalecer":
                    int r = Random.Range(0, 100);
                    switch (currentUnit.fortalecerStat)
                    {
                        case 0:
                            if(r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                            currentUnit.maxhp++; Texts[c].text = "+1 HP";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 1:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.str++; Texts[c].text = "+1 For";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 2:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.mag++; Texts[c].text = "+1 Mag";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 3:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.dex++; Texts[c].text = "+1 Des";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 4:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.def++; Texts[c].text = "+1 Def";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 5:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.mdef++; Texts[c].text = "+1 MDef";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 6:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.speed++; Texts[c].text = "+1 Vel";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                        case 7:
                            if (r <= currentUnit.growths[currentUnit.fortalecerStat])
                            {
                                currentUnit.luck++; Texts[c].text = "+1 Sor";
                            }
                            else
                            {
                                Texts[c].text = "Treino não foi bem sucedido";
                            }
                            break;
                    }
                    break;
                case "Trabalhar":
                    gameManager.money += 500;
                    break;
                case "Aprender":
                    if (currentUnit.ClassLearning.ContainsKey(currentUnit.currentLearnigClassID))
                    {
                        currentUnit.ClassLearning[currentUnit.currentLearnigClassID] += 1;
                    }
                    else
                    {
                        currentUnit.ClassLearning.Add(currentUnit.currentLearnigClassID, 1);
                    }
                    Texts[c].text = "Comecou a aprender classe";
                    if (currentUnit.ClassLearning[currentUnit.currentLearnigClassID] >= 5)
                    {
                        currentUnit.ClassID.Append(currentUnit.currentLearnigClassID);
                        currentUnit.ClassID.Add(currentUnit.currentLearnigClassID);
                        Texts[c].text = "Aprendeu classe";
                        currentUnit.activity = "Treinar";
                    }
                    break;
                case "":
                    Texts[c].text = "sem atividade";
                    break;
                default:
                    Texts[c].text = activity;
                    break;
            }
            c++;
        }
        DataPersistenceManager dpm = FindAnyObjectByType<DataPersistenceManager>();
        dpm.SaveGame();
    }
}
