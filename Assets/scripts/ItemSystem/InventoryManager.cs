using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject GameManagerOBJ;
    public GameManager Manager;
    public GameObject panel;
    public GameObject charSelectPanel;
    public GameObject unitSelectPanel;
    public GameObject firstButtonPrefab;
    public GameObject buttonPrefab;
    public UnitBehavior selectedUnit;
    public GameObject sword;
    public GameObject axe;
    public GameObject lance;
    public GameObject bow;
    public GameObject tome;
    public GameObject receptacle;
    public Sprite[] sprites;
    public TextMeshProUGUI statText;
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void Equip(Item item)
    {
        selectedUnit.Weapon = item;
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        Debug.Log(unitBehavior.UnitName);
        statText.text = "hp:" +unitBehavior.maxhp + "\n" + "atk:" + unitBehavior.atk + "\n" + "def:" + unitBehavior.def + "\n" + "des:" + unitBehavior.dex + "\n" + "sorte:" + unitBehavior.luck ;

    }
    public void UpdateInventory()
    {
        bool first = true;
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Select(Manager.team[0].GetComponent<UnitBehavior>());
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        while (charSelectPanel.transform.childCount > 0)
        {
            DestroyImmediate(charSelectPanel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in Manager.Inventory)
        {

        }
        foreach (GameObject unit in Manager.team)
        {
            if (first) 
            {
                GameObject charButton = Instantiate(firstButtonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
                first = false;
            }
            else 
            {
                GameObject charButton  = Instantiate(buttonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
            }
        }
    }
    public void UpdateSelectionList()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (GameObject unit in Manager.team)
        {
            GameObject button = Instantiate(buttonPrefab, unitSelectPanel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Manager.selectUnit(unit));
            button.GetComponent<Button>().onClick.AddListener(() => Manager.Battle());
            button.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
        }
    }
}
