using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class KeyItemDrawer : MonoBehaviour
{
    public GameObject Drawer;
    public GameObject Panel;
    bool activated = false;
    private InventoryManager inventoryManager;
    private GameManager gameManager;
    public void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>(true);
        gameManager = FindObjectOfType<GameManager>(true);
    }
    public void Amongus()
    {
        if (activated)
        {
            foreach(Transform tr in Panel.transform)
            {
          tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
          //GameObject itemButton = Instantiate(inventoryManager.ItemButtonPrefab, inventoryManager.ItemSelectPanel.transform);
          tr.gameObject.GetComponent<Button>().onClick.AddListener(() => inventoryManager.InstantiateKeyItem(gameManager.KeyItems[0], false));
            }
          Drawer.transform.localPosition = new() {x= Drawer.transform.localPosition.x -200,y= Drawer.transform.localPosition.y};
        }
        else
        {
        Drawer.transform.localPosition = new() { x = Drawer.transform.localPosition.x + 200, y = Drawer.transform.localPosition.y };
        }
        activated = !activated;
    }
}
