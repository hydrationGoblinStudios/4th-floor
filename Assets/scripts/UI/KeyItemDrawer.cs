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
            int c = 0;
            foreach (Transform tr in Panel.transform)
            {
                tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject itemButton = Instantiate(inventoryManager.ItemButtonPrefab, inventoryManager.ItemSelectPanel.transform);
                int keyCounts = gameManager.KeyItems.Count;
                Debug.Log(c +"  " + keyCounts);
                if (c < keyCounts)
                {
                tr.gameObject.name = gameManager.KeyItems[c].ItemName;
                int a = c;
                tr.gameObject.GetComponent<Button>().onClick.AddListener(() => inventoryManager.InstantiateKeyItem(gameManager.KeyItems[a], false));
                tr.gameObject.GetComponent<Image>().sprite = inventoryManager.sprites[0];
                c++;
                }
                else
                {
                    tr.gameObject.GetComponent<Image>().sprite = null;
                }
            }
        }
        activated = !activated;
    }
}
