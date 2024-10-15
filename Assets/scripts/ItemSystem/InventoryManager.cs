using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject GameManagerOBJ;
    [HideInInspector] public GameManager Manager;
    public GameObject panel;
    public GameObject charSelectPanel;
    public GameObject ItemSelectPanel;
    public GameObject unitSelectPanel;
    public GameObject buttonPrefab;
    public GameObject ItemButtonPrefab;
    public GameObject UnitSelectButton;
    public UnitBehavior selectedUnit;
    public Sprite[] sprites;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI accesoryText;
    public NodeParser nodeParser;
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void Equip(Item item)
    {if(item.type == Item.Type.weapon)
        {
            selectedUnit.Weapon = item;
            UpdateEquips(item);
        }
        else
        {
            selectedUnit.Accesory = item;
            UpdateAccesory(item);
        }
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString(); statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        equipText.text = "Arma:\n" + unitBehavior.Weapon.ItemName + "\nAtk:" + unitBehavior.Weapon.str + "\nAcerto:" + unitBehavior.Weapon.hit + "\nCrit:" + unitBehavior.Weapon.crit;
        if(unitBehavior.Accesory != null)
        {
            accesoryText.text = "Accesorio:\n" + unitBehavior.Accesory.ItemName + "\nAtk:" + unitBehavior.Accesory.str + "\nDef:" + unitBehavior.Weapon.def + "\nDes:" + unitBehavior.Weapon.dex + "\nSorte:" + unitBehavior.Weapon.luck + "\nvel:" + unitBehavior.Weapon.speed;
        }
        else
        {
            accesoryText.text = "";
        }
    }
    public void UpdateInventory()
    {
        bool first = true;
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Select(Manager.team[0].GetComponent<UnitBehavior>());
        while (charSelectPanel.transform.childCount > 0)
        {
            DestroyImmediate(charSelectPanel.transform.GetChild(0).gameObject);
        }

        DisplayItemList(Manager.Inventory);

        foreach (GameObject unit in Manager.team)
        {
            if (first) 
            {
                GameObject charButton = Instantiate(buttonPrefab, charSelectPanel.transform);
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
            GameObject button = Instantiate(UnitSelectButton, unitSelectPanel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Manager.SelectUnit(unit));
            button.GetComponent<Button>().onClick.AddListener(() => Manager.Battle());
            button.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
        }
    }
    public void UpdateEquips(Item item)
    {
        equipText.text = "Arma:\n"+ item.ItemName + "\nAtk:" + item.str + "\nAcerto:" + item.hit + "\nCrit:" +item.crit;
    }
    public void UpdateAccesory(Item item)
    {
        accesoryText.text = "Accesorio:\n" + item.ItemName + "\nAtk:" + item.str + "\nDef:" + item.def + "\nDes:" + item.dex + "\nSorte:" + item.luck + "\nvel:" + item.speed;
    }
    public void DisplayItemList(List<Item> ItemList)
    {
        Manager.ParseWeaponList();

        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            GameObject itemButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            itemButton.GetComponent<Button>().onClick.AddListener(() => Equip(item));
            itemButton.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName;
            switch (item.weapontype)
            {
                case Item.Weapontype.Sword:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[0];
                    break;
                case Item.Weapontype.Lance:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[1];
                    break;
                case Item.Weapontype.Axe:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[2];
                    break;
                case Item.Weapontype.Bow:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[3];
                    break;
                case Item.Weapontype.Tome:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[4];
                    break;
                case Item.Weapontype.Receptacle:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[5];
                    break;
                case Item.Weapontype.Accesory:
                    itemButton.GetComponentInChildren<Image>().sprite = sprites[6];
                    break;
            }
        }
    }
    public void DisplaySwordList()
    {
        DisplayItemList(Manager.SwordList);
    }
    public void DisplayLanceList()
    {
        DisplayItemList(Manager.LanceList);
    }
    public void DisplayAxeList()
    {
        DisplayItemList(Manager.AxeList);
    }
    public void DisplayBowList()
    {
        DisplayItemList(Manager.BowList);
    }
    public void DisplayTomeList()
    {
        DisplayItemList(Manager.TomeList);
    }
    public void DisplayReceptacleList()
    {
        DisplayItemList(Manager.ReceptacleList);
    }
    public void DisplayAccesoriesList()
    {
        DisplayItemList(Manager.AccesoriesList);
    }
}
