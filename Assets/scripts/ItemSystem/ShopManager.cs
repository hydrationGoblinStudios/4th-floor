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
    public DialogueGraph graph;

    void Start()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (Item item in stock)
        {
            GameObject button = Instantiate(buttonPrefab, panel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Buy(item));
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName;
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
