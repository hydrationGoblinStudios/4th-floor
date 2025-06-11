using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ClassChangeManager : MonoBehaviour
{
     public InventoryManager inventoryManager;
     public List<GameObject> ClassChangeManagerButtons;
    public List<Item> ImprovisedWeapon;
    public List<SpriteRenderer> ClassChangeManagerSprites;
    [HideInInspector] public GameObject GameManagerOBJ;
    [HideInInspector] public GameManager Manager;
    public bool classLearn = false;


    public void AllowChanges()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (SpriteRenderer sr in ClassChangeManagerSprites)
        {
           sr.color = new Color((float)0.6, (float)0.6, (float)0.6, (float)0.4); 
        }
        foreach( GameObject go in ClassChangeManagerButtons)
        {
            go.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
        foreach (KeyValuePair<int, int> entry in Manager.SelectedUBClassChange.GetComponent<UnitBehavior>().ClassLearning)
        {
            
            switch (entry.Key)
            {
                case 101: if (entry.Value >= 5) { ClassChangeManagerSprites[0].color = new Color(1,1,1, 1) ; ClassChangeManagerButtons[0].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(101, ImprovisedWeapon[0])); } break;
                case 106: if (entry.Value >= 5) {  ClassChangeManagerSprites[1].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[1].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(106, ImprovisedWeapon[3])); } break;
                case 102: if (entry.Value >= 5) {  ClassChangeManagerSprites[2].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[2].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(102, ImprovisedWeapon[1])); } break;
                case 103: if (entry.Value >= 5) { ClassChangeManagerSprites[3].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[3].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(103, ImprovisedWeapon[2])); } break;
                case 104: if (entry.Value >= 5) { ClassChangeManagerSprites[4].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[4].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(104, ImprovisedWeapon[4])); } break;
                case 105: if (entry.Value >= 5) {  ClassChangeManagerSprites[5].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[5].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(105, ImprovisedWeapon[5])); } break;
                case 107: if (entry.Value >= 5) {  ClassChangeManagerSprites[6].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[6].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(107, ImprovisedWeapon[0])); } break;
            }
        }
        inventoryManager.Select(inventoryManager.selectedUnit);
        inventoryManager.UpdateUITop();
    }
    public void ChooseClassToLearn()
    {
        classLearn = true;
        if (inventoryManager.SceneInteractable == null)
        {
            inventoryManager.SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
        }
        foreach (GameObject go in new List<GameObject> { inventoryManager.SceneInteractable})
        {
            if (go != null)
            {
                go.SetActive(!go.activeInHierarchy);
            }
        }
        inventoryManager.ClassChangeObject.SetActive(true);
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        FindObjectOfType<ActivityBoardUserInterface>(true).gameObject.SetActive(false);
        foreach (SpriteRenderer sr in ClassChangeManagerSprites)
        {
            sr.color = new Color(1, 1, 1, 1);
        }
        foreach (GameObject go in ClassChangeManagerButtons)
        {
            go.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
        List<int> classID = new() { 101,106,102,103,104,105,107};
        int c = 0;
        foreach (GameObject entry in ClassChangeManagerButtons)
        {
            string learnedClassLevel = "0";
            Debug.Log(Manager.selectedUB4Activity.ClassLearning.TryGetValue(classID[c], out int value));
            Debug.Log(value);
            if (Manager.selectedUB4Activity.ClassLearning.TryGetValue(classID[c], out int temp))
            {
                learnedClassLevel = value.ToString();
            }

            switch (classID[c])
            {
                case 101: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  101); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 106: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  106); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 102: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  102); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 103: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  103); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 104: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  104); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 105: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  105); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 107: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID =  107); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
            }
            c++;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("classLearn");
            inventoryManager.ToggleClassChange();
            classLearn = false;
        }
    }
}
