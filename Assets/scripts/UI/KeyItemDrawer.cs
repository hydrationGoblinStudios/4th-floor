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
          Drawer.transform.localPosition = new() {x= Drawer.transform.localPosition.x -200,y= Drawer.transform.localPosition.y};
            foreach(Transform tr in Panel.transform)
            {
                tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
        else
        {
        Drawer.transform.localPosition = new() { x = Drawer.transform.localPosition.x + 200, y = Drawer.transform.localPosition.y };
            foreach (Transform tr in Panel.transform)
            {
                Debug.Log(tr.name);
                tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject itemButton = Instantiate(inventoryManager.ItemButtonPrefab, inventoryManager.ItemSelectPanel.transform);
                tr.gameObject.GetComponent<Button>().onClick.AddListener(() => inventoryManager.InstantiateKeyItem(gameManager.KeyItems[0], false));
            }
        }
        activated = !activated;
    }
}
