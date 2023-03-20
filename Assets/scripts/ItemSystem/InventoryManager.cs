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
    public GameObject buttonPrefab;
    public UnitBehavior selectedUnit;
    public GameObject sword;
    public GameObject axe;
    public GameObject lance;
    public GameObject bow;
    public GameObject tome;
    public GameObject receptacle;

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
    }
    public void UpdateInventory()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        selectedUnit = Manager.team[0].GetComponent<UnitBehavior>();
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
            GameObject button = Instantiate(buttonPrefab, panel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Equip(item));
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName;

        }
        foreach (GameObject unit in Manager.team)
        {
            GameObject button = Instantiate(buttonPrefab, charSelectPanel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
            button.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName ;

        }
    }
}
