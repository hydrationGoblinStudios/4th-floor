using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChangeManager : MonoBehaviour
{
     public InventoryManager inventoryManager;
     public List<GameObject> ClassChangeManagerButtons;
    public List<SpriteRenderer> ClassChangeManagerSprites;
    [HideInInspector] public GameObject GameManagerOBJ;
    [HideInInspector] public GameManager Manager;    


    public void AllowChanges()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (SpriteRenderer sr in ClassChangeManagerSprites)
        {
           sr.color = new Color((float)0.6, (float)0.6, (float)0.6, (float)0.4);
        }
        foreach (KeyValuePair<int, int> entry in Manager.SelectedUBClassChange.GetComponent<UnitBehavior>().ClassLearning)
        {
            Debug.Log("checando classes de " + Manager.SelectedUBClassChange.GetComponent<UnitBehavior>().UnitName);
            Debug.Log("CLASSE ID " + entry.Key);
            Debug.Log("CLASSE Value " + entry.Value);
            
            switch (entry.Key)
            {
                case 101: if (entry.Value >= 5) { ClassChangeManagerSprites[0].color = new Color(1,1,1, 1) ; } break;
                case 106: if (entry.Value >= 5) {  ClassChangeManagerSprites[1].color = new Color(1, 1, 1, 1); } break;
                case 102: if (entry.Value >= 5) {  ClassChangeManagerSprites[2].color = new Color(1, 1, 1, 1); } break;
                case 103: if (entry.Value >= 5) { ClassChangeManagerSprites[3].color = new Color(1, 1, 1, 1); } break;
                case 104: if (entry.Value >= 5) { ClassChangeManagerSprites[4].color = new Color(1, 1, 1, 1); } break;
                case 105: if (entry.Value >= 5) {  ClassChangeManagerSprites[5].color = new Color(1, 1, 1, 1); } break;
                case 107: if (entry.Value >= 5) {  ClassChangeManagerSprites[6].color = new Color(1, 1, 1, 1); } break;

            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryManager.ToggleClassChange();
        }
    }
}
