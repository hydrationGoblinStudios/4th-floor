using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;


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

    public SpriteRenderer WeaponSprite;
    public List<TextMeshProUGUI> Texts;

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
            button.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.ItemName +" " + item.price;
            button.transform.Find("Power").GetComponent<TextMeshProUGUI>().text = $"{item.power}";
            button.transform.Find("Hit").GetComponent<TextMeshProUGUI>().text = $"{item.hit}";
            button.transform.Find("Crit").GetComponent<TextMeshProUGUI>().text = $"{item.crit}";
            HoverShop hv = button.AddComponent<HoverShop>();
            hv.item = item;
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
    private void Update()
    {
        //  money.text = Manager.money.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
        if (Input.GetKeyDown(KeyCode.Numlock))
        {
            UpdateSprites(Manager.Inventory[3]);
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
    public void UpdateSprites(Item item)
    {
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>(FindObjectsInactive.Include);

        WeaponSprite.sprite = inventoryManager.EquipableImages.Where(obj => obj.name == item.ItemName).SingleOrDefault();
        if (inventoryManager.EquipableImages.Where(obj => obj.name == item.ItemName).SingleOrDefault() == null)
        {
            WeaponSprite.sprite = inventoryManager.EquipableImages.Where(obj => obj.name == item.name).SingleOrDefault();
        }

        Texts[0].text = item.power.ToString();
        Texts[1].text = item.hit.ToString();
        Texts[2].text = item.crit.ToString();
        Texts[3].text = item.description.ToString();
    }
}
