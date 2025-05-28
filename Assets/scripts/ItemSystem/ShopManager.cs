using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject GameManagerOBJ;
    public GameManager Manager;
    public GameObject SceneInteractable;
    public GameObject calendario;
    public GameObject panel;
    public GameObject buttonPrefab;
    public NodeParser nodeParser; 
    public Item[] stock;
    public Sprite[] sprites;
    public DialogueGraph graph;
    public bool blackMarketOpen = false;
    public TextMeshProUGUI money;

    void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<NodeParser>();
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (Item item in stock)
        {
            GameObject button = Instantiate(buttonPrefab, panel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Buy(item));
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName +" " + item.price;
            money.text = Manager.money.ToString();
            switch (item.weapontype)
            {
                case Item.Weapontype.Sword:
                    button.GetComponentInChildren<Image>().sprite = sprites[0];
                    break;
                case Item.Weapontype.Lance:
                    button.GetComponentInChildren<Image>().sprite = sprites[1];
                    break;
                case Item.Weapontype.Axe:
                    button.GetComponentInChildren<Image>().sprite = sprites[2];
                    break;
                case Item.Weapontype.Bow:
                    button.GetComponentInChildren<Image>().sprite = sprites[3];
                    break;
                case Item.Weapontype.Receptacle:
                    button.GetComponentInChildren<Image>().sprite = sprites[4];
                    break;
                case Item.Weapontype.Tome:
                    button.GetComponentInChildren<Image>().sprite = sprites[5];
                    break;
                case Item.Weapontype.Accesory:
                    button.GetComponentInChildren<Image>().sprite = sprites[6];
                    break;
            }
        }
    }
    public void Buy(Item item)
    {
        Debug.Log(Manager.money);
        if(Manager.money >= item.price)
        {
            Manager.money -= item.price;
            Manager.Inventory.Add(item);
        }
        else
        {
            Toggle();
            nodeParser.StartDialogue(graph);
        }
        GameObject[] moneys =  GameObject.FindGameObjectsWithTag("money");
        foreach (GameObject money in moneys)
        {
            if(money.GetComponent<TextMeshPro>() != null)
            {
                money.GetComponent<TextMeshPro>().text = Manager.money.ToString();
            }
        }
    }
    public void Toggle()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (Manager.TimeIsDay || blackMarketOpen)
        {
            if (SceneInteractable == null)
            {
                SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
            }
            if (SceneInteractable != null)
            {
                foreach (Transform child in SceneInteractable.transform)
                {
                    if (child.name != "shop")
                    {
                        child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
                    }
                }
            }
            gameObject.SetActive(!gameObject.activeInHierarchy);
            if (calendario == null)
            {
                calendario = FindObjectOfType<CalendarioUI>(true).gameObject;
            }
            if (calendario != null)
            {
                calendario.SetActive(!calendario.activeInHierarchy);
            }
            blackMarketOpen = false;
        }
    }
}
