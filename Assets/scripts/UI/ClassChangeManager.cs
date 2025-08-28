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
                case 201: if (entry.Value >= 5) {  ClassChangeManagerSprites[7].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[7].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(201, ImprovisedWeapon[0])); } break;
                case 202: if (entry.Value >= 5) {  ClassChangeManagerSprites[8].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[8].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(202, ImprovisedWeapon[0])); } break;
                case 203: if (entry.Value >= 5) {  ClassChangeManagerSprites[9].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[9].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(203, ImprovisedWeapon[0])); } break;
                case 204: if (entry.Value >= 5) {  ClassChangeManagerSprites[10].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[10].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(204, ImprovisedWeapon[0])); } break;
                case 205: if (entry.Value >= 5) {  ClassChangeManagerSprites[11].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[11].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(205, ImprovisedWeapon[0])); } break;
                case 206: if (entry.Value >= 5) {  ClassChangeManagerSprites[12].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[12].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(206, ImprovisedWeapon[0])); } break;
                case 207: if (entry.Value >= 5) {  ClassChangeManagerSprites[13].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[13].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(207, ImprovisedWeapon[0])); } break;
                case 208: if (entry.Value >= 5) { ClassChangeManagerSprites[14].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[14].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(208, ImprovisedWeapon[0])); } break;
                case 209: if (entry.Value >= 5) { ClassChangeManagerSprites[15].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[15].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(209, ImprovisedWeapon[0])); } break;
                case 210: if (entry.Value >= 5) { ClassChangeManagerSprites[16].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[16].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(210, ImprovisedWeapon[0])); } break;
                case 211: if (entry.Value >= 5) { ClassChangeManagerSprites[17].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[17].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(211, ImprovisedWeapon[0])); } break;
                case 212: if (entry.Value >= 5) { ClassChangeManagerSprites[18].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[18].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(212, ImprovisedWeapon[0])); } break;
                case 213: if (entry.Value >= 5) { ClassChangeManagerSprites[19].color = new Color(1, 1, 1, 1); ClassChangeManagerButtons[19].GetComponentInChildren<Button>().onClick.AddListener(() => Manager.ClassChange(213, ImprovisedWeapon[0])); } break;

            }
        }
        foreach(GameObject go in ClassChangeManagerButtons)
        {
            go.GetComponent<Button>().onClick.AddListener(inventoryManager.ToggleClassChange);
        }
        inventoryManager.Select(inventoryManager.selectedUnit);
        inventoryManager.UpdateUITop();
    }
    public void ChooseClassToLearn()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (Manager.selectedUB4Activity == null)
        {
            Manager.selectedUB4Activity = Manager.team[0].GetComponent<UnitBehavior>();
        }
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
        AllowPromotion(Manager.selectedUB4Activity);
        List<int> classID = new() { 101, 106, 102, 103, 104, 105, 107, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213 };
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
                case 101: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 101); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 106: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 106); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 102: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 102); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 103: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 103); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 104: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 104); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                case 105: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 105); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 107: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 107); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 201: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 201); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 202: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 202); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 203: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 203); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 204: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 204); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 205: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 205); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 206: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 206); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 207: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 207); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 208: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 208); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 209: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 209); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 210: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 210); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 211: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 211); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 212: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 212); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
                case 213: entry.GetComponent<Button>().onClick.AddListener(() => Manager.selectedUB4Activity.currentLearnigClassID = 213); entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5";  break;
            }
            c++;
        }
    }

    public void AllowPromotion(UnitBehavior ub)
    {
        List<int> classID = new() { 101, 106, 102, 103, 104, 105, 107, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213 };
        foreach (int classid in ub.ClassLearning.Keys)
        {
            switch (classid)
            {
                case 101 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(201)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(202)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(203)].SetActive(true); } break;
                case 106 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(203)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(204)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(205)].SetActive(true); ; }break;
                case 102 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(205)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(206)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(207)].SetActive(true); } break;
                case 103 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(207)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(208)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(209)].SetActive(true); } break;
                case 104 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(209)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(210)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(211)].SetActive(true); } break;
                case 105 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(211)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(212)].SetActive(true); ClassChangeManagerButtons[classID.IndexOf(201)].SetActive(true); } break;
                case 107 :if (ub.ClassLearning.GetValueOrDefault(classid) >= 5) { ClassChangeManagerButtons[classID.IndexOf(213)].SetActive(true); } break;
                default:break;
            }
            
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
