using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject GameManagerOBJ;
    private GameManager Manager;
    public GameObject panel;
    public GameObject buttonPrefab;
  
    private void Start()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (Item item in Manager.Inventory)
        {
            GameObject button = Instantiate(buttonPrefab, panel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Equip(item));
        }
    }
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void Equip(Item item)
    {
        UnitBehavior selectedUnit = Manager.playerUnit[0].GetComponent<UnitBehavior>();
        selectedUnit.Weapon = item;
    }
}
