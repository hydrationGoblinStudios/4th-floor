using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject GameManagerOBJ;
    private GameManager Manager;
    public GameObject panel;
    public GameObject charSelectPanel;
    public GameObject buttonPrefab;
    public UnitBehavior selectedUnit;
  
    private void Start()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (Item item in Manager.Inventory)
        {
            GameObject button = Instantiate(buttonPrefab, panel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Equip(item));
        }
        foreach (GameObject obj in Manager.teamInstances)
        {
            GameObject button = Instantiate(buttonPrefab, charSelectPanel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Select(obj.GetComponent<UnitBehavior>()));
        }
    }
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
}
