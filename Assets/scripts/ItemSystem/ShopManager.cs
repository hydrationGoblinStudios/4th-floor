using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private GameObject GameManagerOBJ;
    private GameManager Manager;
    public GameObject panel;
    public GameObject buttonPrefab;
    public NodeParser nodeParser;
    public Item[] stock;
    public Sprite[] sprites;
    public DialogueGraph graph;

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
                case Item.Weapontype.Accesory:
                    button.GetComponentInChildren<Image>().sprite = sprites[4];
                    break;
            }
        }
    }
    public void Buy(Item item)
    {
        Debug.Log(Manager.money);
        if(Manager.money >= item.price)
        {
            Manager.money = item.price;
            Manager.moneyText.text = ""+Manager.money;
            Manager.Inventory.Add(item);
        }
        else
        {
            Toggle();
            nodeParser.StartDialogue(graph);
        }
    }
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
