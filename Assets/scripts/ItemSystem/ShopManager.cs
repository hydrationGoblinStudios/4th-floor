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
    public bool open = false;

    void Start()
    {
        if (SceneInteractable == null)
        {
            SceneInteractable = FindObjectOfType<SceneInteractables>(true).gameObject;
        }
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
    private void FixedUpdate()
    {
        money.text = Manager.money.ToString();
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
        open = !open;
        Debug.Log("toggle");
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
            if (SceneInteractable == null)
            {
                SceneInteractable = FindObjectOfType<SceneInteractables>(true).gameObject;
            }
            if (SceneInteractable != null)
            {
            Debug.Log("SI isnt null");
                foreach (Transform child in SceneInteractable.transform)
                {
                    if (child.name != "shop")
                    {
                        child.gameObject.SetActive(!open);
                    }
                }
            }
        if (Manager.TimeIsDay || blackMarketOpen)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
            if (calendario == null)
            {
                calendario = FindObjectOfType<CalendarioUI>(true).gameObject;
            }
            if (!calendario.activeInHierarchy)
            {
                calendario.SetActive(!calendario.activeInHierarchy);
            }
            blackMarketOpen = false;
        }
    }
}
