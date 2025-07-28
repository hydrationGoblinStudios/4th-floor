using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class KeyItemDrawer : MonoBehaviour
{
    public GameObject Drawer;
    public GameObject Panel;
    public bool activated = false;
    private InventoryManager inventoryManager;
    private GameManager gameManager;
    public void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>(true);
        gameManager = FindObjectOfType<GameManager>(true);
    }
    public void Draw()
    {
        if(gameManager.currentState == GameManager.UIState.Available || activated)
        {
        if (activated)
        {
                gameManager.currentState = GameManager.UIState.Available;
                Drawer.transform.localPosition = new() {x= Drawer.transform.localPosition.x -200,y= Drawer.transform.localPosition.y};
            foreach(Transform tr in Panel.transform)
            {
                tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
        else
        {
                gameManager.currentState = GameManager.UIState.Ocuppied;

                Drawer.transform.localPosition = new() { x = Drawer.transform.localPosition.x + 200, y = Drawer.transform.localPosition.y };
            int c = 0;
            foreach (Transform tr in Panel.transform)
            {
                tr.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject itemButton = Instantiate(inventoryManager.ItemButtonPrefab, inventoryManager.ItemSelectPanel.transform);
                int keyCounts = gameManager.KeyItems.Count;
                if (c < keyCounts)
                {
                tr.gameObject.name = gameManager.KeyItems[c].ItemName;
                int a = c;
                tr.gameObject.GetComponent<Button>().onClick.AddListener(() => inventoryManager.InstantiateKeyItem(gameManager.KeyItems[a], false));
                tr.gameObject.GetComponent<Image>().sprite = inventoryManager.keyItemSprites.Where(obj => obj.name == gameManager.KeyItems[a].name).SingleOrDefault();
                    tr.gameObject.GetComponent<Image>().color = new() { r = 255, g = 255, b=255,a = 255 };
                c++;
                }
                else
                {
                    tr.gameObject.GetComponent<Image>().sprite = null;
                    tr.gameObject.GetComponent<Image>().color = new() {a=0}
                    ;

                }
            }
        }
        activated = !activated;
        }
    }
}
