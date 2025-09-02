using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static UnityEditor.Progress;
using UnityEditor.Experimental.GraphView;
public class InventoryManager : MonoBehaviour
{
    public GameObject GameManagerOBJ;
    public GameObject SceneInteractable;
    public GameObject calendario;
    public Button button;
    [HideInInspector] public GameManager Manager;
    public GameObject panel;
    public List<Item> CurrentItemList;
    public GameObject charSelectPanel;
    public GameObject ItemSelectPanel;
    public GameObject unitSelectPanel;
    public List<GameObject> panelList;
    public List<Sprite> panelSprites;
    public GameObject buttonPrefab;
    public GameObject ItemButtonPrefab;
    public GameObject UnitSelectButton;
    public UnitBehavior selectedUnit;
    public Sprite[] sprites;
    public Sprite[] keyItemSprites;
    public Sprite[] skillIcons;
    public GameObject[] skillIconsObjects;
    public GameObject soulIconObject;
    public List<TextMeshProUGUI> skillNames;
    public TextMeshProUGUI soulName;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI accesoryText;
    public NodeParser nodeParser;
    public GameObject[] ItemSelectorList;
    public Sprite[] ItemSelectorListSprites;
    public bool Activated;

    [Header("Ui top")]
    public SpriteRenderer MugShot;
    public Sprite[] playableMugShots;
    public TextMeshProUGUI UITopName;
    public TextMeshProUGUI LvlText;
    public Slider expbar;
    public Image ClassIconObj;
    public List<Sprite> ClassIcons;
    public SpriteRenderer WeaponImage;
    public SpriteRenderer AccesoryImage;
    public Sprite[] EquipableImages;

    public List<GameObject> DragAndDroppables;

    public GameObject ClassChangeObject;

    public List<Sprite> statIcons;
    List<string> targets = new() { "LeastHp" , "HighestStat", "LowestStat", "ClassID", "Weapon", "mais próximo", "mais longe" };
    public GameObject TargetStatButton;
    public GameObject TargetStatPanel;
    public List<TextMeshProUGUI> targetTexts;
    public void Toggle()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        if (Manager.currentState == GameManager.UIState.Available || Activated)
        {
            Activated = !Activated;
        if (FindObjectOfType<KeyItemDrawer>().activated)
        {
            FindObjectOfType<KeyItemDrawer>().Draw();
        }
        if (SceneInteractable == null)
        {
        SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
        }
            foreach (GameObject go in new List<GameObject>{ SceneInteractable,gameObject,calendario})
            {
                if(go != null)
                {
                 go.SetActive(!go.activeInHierarchy);
                }
            }        
        if(Manager.currentState == GameManager.UIState.Available)
        {
            Manager.currentState = GameManager.UIState.Ocuppied;
        }
        else
        {
            Manager.currentState = GameManager.UIState.Available;
        }
        }
    }
    public void ToggleClassChange()
    {

        if (!ClassChangeObject.GetComponent<ClassChangeManager>().classLearn)
        {
            int c = 0;
            ClassChangeManager CCM = ClassChangeObject.GetComponent<ClassChangeManager>();
            List<int> classID = new() { 101, 106, 102, 103, 104, 105, 107,201,202,203,204,205,206,207,208,209,210,211,212,213 };
            UnitBehavior UB = Manager.SelectedUBClassChange.GetComponent<UnitBehavior>();
            foreach (GameObject entry in CCM.ClassChangeManagerButtons)
            {
                string learnedClassLevel = "0";
                UB.ClassLearning.TryGetValue(classID[c], out int value);
                if (UB.ClassLearning.TryGetValue(classID[c], out int temp))
                {
                    learnedClassLevel = value.ToString();
                }
                switch (classID[c])
                {
                    case 101:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 106:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 102:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 103:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 104:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 105:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 107:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 201:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 202:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 203:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 204:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 205:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 206:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 207:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 208:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 209:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 210:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 211:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 212:  entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;
                    case 213: entry.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = $"{learnedClassLevel}/5"; break;

                }
                c++;
            }
        ClassChangeObject.SetActive(!ClassChangeObject.activeInHierarchy);
        gameObject.SetActive(!gameObject.activeInHierarchy);
        ClassChangeObject.GetComponent<ClassChangeManager>().AllowChanges();
        UpdateUITop();
        }
        else
        {
            ClassChangeObject.SetActive(false);
            if (SceneInteractable == null)
            {
                SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
            }
            foreach (GameObject go in new List<GameObject> {SceneInteractable })
            {               
                    go.SetActive(!go.activeInHierarchy);                
            }
            
            ClassChangeObject.GetComponent<ClassChangeManager>().classLearn = false;
            Activated = true;
        }
    }
    public void Equip(Item item)
    {if(item.type == Item.Type.weapon)
        {
            if (selectedUnit.UsableWeaponTypes.Contains(item.weapontype)){
                selectedUnit.Weapon = item;
                UpdateEquips(item);
            }
        }
        else
        {
            selectedUnit.Accesory = item;
            UpdateAccesory(item);
        }
        UpdateUITop();
        Select(selectedUnit);
    }
    public void ChangeTargeting(string target, int targetStat = 1987, bool primaryTarget = true)
    {
        if (primaryTarget)
        {
        selectedUnit.target = target;
        selectedUnit.targetStat = targetStat;
           targetTexts[0].text = GetTargetDisplayName(target, targetStat);
        }
        else
        {
            selectedUnit.target2 = target;
            selectedUnit.targetStat2 = targetStat;
            targetTexts[1].text = GetTargetDisplayName(target, targetStat);
        }
        SubTargeting(target, primaryTarget);
    }
    public void SubTargeting(string target, bool primaryTarget = true)
    {
        switch (target)
        {
            case "HighestStat": DisplayTargetChoice(target,primaryTarget);break;
            case "LowestStat": DisplayTargetChoice(target, primaryTarget); break;
            case "Weapon": DisplayTargetChoice(target, primaryTarget); break;
            case "ClassID": DisplayTargetChoice(target, primaryTarget); break;
            default: DisplayAllList(); TargetStatPanel.transform.parent.gameObject.SetActive(false); break;
        }
    }
    public string GetTargetDisplayName(string target, int targetStat)
    { string stat = "";
        switch (targetStat)
        {
            case 0: stat = " vida"; break;
            case 1: stat = " força"; break;
            case 2: stat = " magia"; break;
            case 3: stat = " destreza"; break;
            case 4: stat = " velocidade"; break;
            case 5: stat = " defesa fisica"; break;
            case 6: stat = " defesa magica"; break;
            case 7: stat = " sorte"; break;
            default: stat = "";break;
        }
        switch (target)
        {
            case "LeastHp": return $"Menor vida";
            case "HighestStat": return $"Maior{stat}";
            case "LowestStat": return $"Menor{stat}";
            case "ClassID": List<string> Classes = new() {"Espadachim", "Arqueiro", "Guerreiro", "Soldado", "Feiticeiro", "Mistico", "Prisioneiro",
            "cavaleiro Encantado","Duelista","Ladino","Atirador","Patrulheiro","Bárbaro","Armadurado","Lanceiro","Paladino","Mago","Curandeiro","Ocultista","Gladiador"}; return $"Focando {Classes[targetStat]}";
            case "Weapon": List<string> Armas = new() { "Espada equipada", "Lança equipada", "Machado equipado", "Arco equipado", "Tomo equipado", "Receptaculo equipado" }; return $"{Armas[targetStat]}";
            case "mais próximo": return $"mais próximo";
            case "mais longe": return $"mais longe";
            default: return "-";
        }
    }

    public void EquipSkill(string Skill, int SkillSlot)
    {
        while (selectedUnit.skills.Count <= SkillSlot)
        {
            selectedUnit.skills.Add("");
        }
        selectedUnit.skills[SkillSlot] = Skill;
        skillIconsObjects[SkillSlot].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == Skill).SingleOrDefault();
        Select(selectedUnit);
    }
    public void EquipSoul(string soulName)
    {
        selectedUnit.equipedSoul = soulName;
        //valor de alma maxima
        SoulPrice(soulName);
        soulIconObject.GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == soulName).SingleOrDefault();
        Select(selectedUnit);
    }
    public void InstantiateKeyItem(Item item, bool toggle = true)
    {
        if (toggle) { Toggle(); }
        KeyItemDrawer kid = GameObject.FindObjectOfType<KeyItemDrawer>();
        if(kid != null && kid.activated)
        {
            kid.Draw();
        }
        Instantiate(DragAndDroppables.Where(obj => obj.name == item.name).SingleOrDefault(), gameObject.GetComponentInParent<Canvas>().transform);
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString();
        statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        switch (selectedUnit.classId)
        {
            case 101: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Espadachim").SingleOrDefault(); break;
            case 102: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Guerreiro").SingleOrDefault(); break;
            case 103: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Soldado").SingleOrDefault(); break;
            case 104: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Feiticeiro").SingleOrDefault(); break;
            case 105: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Mistico").SingleOrDefault(); break;
            case 106: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Arqueiro").SingleOrDefault(); break;
            case 107: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Prisioneiro").SingleOrDefault(); break;
            case 201: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Prisioneiro").SingleOrDefault(); break;
            default: ClassIconObj.sprite = ClassIcons.Where(obj => obj.name == "Icone_Espadachim").SingleOrDefault(); break;
        }
        if(selectedUnit.Weapon.damageType == 0) 
        {
            statTexts[8].text = (selectedUnit.str + selectedUnit.Weapon.power).ToString();
        }
        else
        {
            statTexts[8].text = (selectedUnit.mag + selectedUnit.Weapon.power).ToString();
        }
        statTexts[9].text = ((selectedUnit.dex * 3) +selectedUnit.luck + selectedUnit.Weapon.hit).ToString();
        statTexts[10].text = ((selectedUnit.speed * 2) + selectedUnit.luck).ToString();
        statTexts[11].text = ((int)(selectedUnit.dex/2) + selectedUnit.Weapon.crit).ToString();
        if(unitBehavior.Weapon != null)
        {
        equipText.text = "Arma:\n" + unitBehavior.Weapon.ItemName + "\nAtk:" + unitBehavior.Weapon.str + "\nAcerto:" + unitBehavior.Weapon.hit + "\nCrit:" + unitBehavior.Weapon.crit;
        }
        if(unitBehavior.Accesory != null)
        {
            accesoryText.text = "Accesorio:\n" + unitBehavior.Accesory.ItemName + "\nAtk:" + unitBehavior.Accesory.str + "\nDef:" + unitBehavior.Weapon.def + "\nDes:" + unitBehavior.Weapon.dex + "\nSorte:" + unitBehavior.Weapon.luck + "\nvel:" + unitBehavior.Weapon.speed;
        }
        else
        {
            accesoryText.text = "";
        }
        int c = 0;
        UpdateSkillName();
        UpdateSoulName();
        SoulPrice(selectedUnit.equipedSoul);
        foreach(GameObject go in skillIconsObjects)
        {
            go.GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == skillNames[c].text).SingleOrDefault();
            c++;
        }
        DisplayItemList(Manager.Inventory);
        UpdateUITop();
        if (soulIconObject.GetComponent<SpriteRenderer>().sprite != null && skillIcons.Where(obj => obj.name == selectedUnit.equipedSoul).SingleOrDefault() != null)
        {
        soulIconObject.GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.equipedSoul).SingleOrDefault();
        }
        else
        {
            soulIconObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        while (selectedUnit.skills.Count <= 4)
        {
            selectedUnit.skills.Add("");
        }
        skillIconsObjects[0].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.skills[0]).SingleOrDefault();
        skillIconsObjects[1].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.skills[1]).SingleOrDefault();
        skillIconsObjects[2].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.skills[2]).SingleOrDefault();
        skillIconsObjects[3].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.skills[3]).SingleOrDefault();
        if(selectedUnit != null)
        {
        Manager.SelectedUBClassChange = selectedUnit.gameObject;
        }
        else
        {
            selectedUnit = Manager.SelectedUBClassChange.GetComponent<UnitBehavior>();
        }
        if(panelList.Count > 0)
        {
            int counter = 0;
            GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
            Manager = GameManagerOBJ.GetComponent<GameManager>();
            foreach (GameObject obj in panelList)
            {
                obj.GetComponent<Image>().sprite = panelSprites[0];
            }
            foreach (GameObject obj in Manager.team)
            {
                if(obj.name == selectedUnit.name)
                {
                    panelList[counter].GetComponent<Image>().sprite = panelSprites[1];
                }
                counter++; 
            }
        }


    }
    public void UpdateInventory()
    {
        bool first = true;
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        Select(Manager.team[0].GetComponent<UnitBehavior>());
        while (charSelectPanel.transform.childCount > 0)
        {
            DestroyImmediate(charSelectPanel.transform.GetChild(0).gameObject);
        }
        CurrentItemList = Manager.Inventory;
        DisplayItemList(Manager.Inventory);
        panelList.Clear();
        foreach (GameObject unit in Manager.team)
        {
            if (first) 
            {
                GameObject charButton = Instantiate(buttonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
                panelList.Add(charButton);
                first = false;
            }
            else 
            {
                GameObject charButton  = Instantiate(buttonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
                panelList.Add(charButton);
            }
        }
        UpdateSkillName();
        UpdateUITop();
    }

    public void UpdateSelectionList()
    {
        GameManagerOBJ = GameObject.FindGameObjectWithTag("game manager");
        Manager = GameManagerOBJ.GetComponent<GameManager>();
        foreach (GameObject unit in Manager.team)
        {
            GameObject button = Instantiate(UnitSelectButton, unitSelectPanel.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Manager.SelectUnit(unit));
            button.GetComponent<Button>().onClick.AddListener(() => Manager.Battle());
            button.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
        }
    }
    public void UpdateEquips(Item item)
    {
        equipText.text = "Arma:\n"+ item.ItemName + "\nAtk:" + item.str + "\nAcerto:" + item.hit + "\nCrit:" +item.crit;
    }
    public void UpdateAccesory(Item item)
    {
        accesoryText.text = "Accesorio:\n" + item.ItemName + "\nAtk:" + item.str + "\nDef:" + item.def + "\nDes:" + item.dex + "\nSorte:" + item.luck + "\nvel:" + item.speed;
    }
    public void UpdateUITop()
    {
        UITopName.text = selectedUnit.UnitName;
        expbar.value = selectedUnit.currentExp;
        if (playableMugShots.Where(obj => obj.name == selectedUnit.UnitName + " mugshot").SingleOrDefault() != null)
        {
            MugShot.sprite = playableMugShots.Where(obj => obj.name == selectedUnit.UnitName + " mugshot").SingleOrDefault();
        }
        else
        {
            MugShot.sprite = playableMugShots[0];
        }
        UITopName.text = selectedUnit.UnitName;
        expbar.value = selectedUnit.currentExp;
        LvlText.text = "Lvl:" + selectedUnit.currentLevel;
        if (selectedUnit.Weapon != null)
        {
        WeaponImage.sprite = EquipableImages.Where(obj => obj.name == selectedUnit.Weapon.ItemName).SingleOrDefault();
        }
        if (selectedUnit.Weapon != null && EquipableImages.Where(obj => obj.name == selectedUnit.Weapon.name).SingleOrDefault() != null)
        {
            WeaponImage.sprite = EquipableImages.Where(obj => obj.name == selectedUnit.Weapon.name).SingleOrDefault();
        }
        else
        {
            WeaponImage.sprite = null;
        }
        if (selectedUnit.Accesory != null)
        {
            AccesoryImage.sprite = EquipableImages.Where(obj => obj.name == selectedUnit.Accesory.name).SingleOrDefault();
        }
        else{
            AccesoryImage.sprite = null;
        }
    }

    public void DisplayTargeting(bool primaryTarget)
    {
        Manager = FindObjectOfType<GameManager>();
        if (Manager != null)
        {
            Manager.ParseWeaponList();
        }
        foreach (GameObject itemselector in ItemSelectorList)
        {
            itemselector.GetComponent<Image>().sprite = ItemSelectorListSprites[2];
        }
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (Transform child in TargetStatPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (string target in targets)
        {
        TargetStatPanel.transform.parent.gameObject.SetActive(true);
        GameObject itemButton = Instantiate(TargetStatButton, TargetStatPanel.transform);
        itemButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 0, primaryTarget));
        itemButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = target;
        itemButton.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        }
    }
    public void DisplayItemList(List<Item> ItemList, int button = 0)
    {
            Manager = FindObjectOfType<GameManager>();      
        if (Manager != null)
        {
           Manager.ParseWeaponList();
        }
        foreach(GameObject itemselector in ItemSelectorList)
        {
            itemselector.GetComponent<Image>().sprite = ItemSelectorListSprites[2];
        }
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        List<Item> allEquiped = new();
        foreach(GameObject unit in Manager.team)
        {
            if (unit.GetComponent<UnitBehavior>().Weapon != null)
            {
                allEquiped.Add(unit.GetComponent<UnitBehavior>().Weapon);
            }
            if (unit.GetComponent<UnitBehavior>().Accesory != null)
            {
                allEquiped.Add(unit.GetComponent<UnitBehavior>().Accesory);
            }
        }
        foreach (Item item in ItemList)
        {
            bool equipped = false;
            if (allEquiped.Contains(item))
            { equipped = true;
                allEquiped.Remove(item);
            }   
            GameObject itemButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            itemButton.GetComponent<Button>().onClick.AddListener(() => Equip(item));
                if (!selectedUnit.UsableWeaponTypes.Contains(item.weapontype) && item.type != Item.Type.accesory && item.type != Item.Type.key)
                {
                    itemButton.GetComponentInChildren<TextMeshProUGUI>().color = new((float)0.6, (float)0.6, (float)0.6, 1);
                }
                itemButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = $"{item.ItemName}";
                itemButton.transform.Find("Power").GetComponent<TextMeshProUGUI>().text = $"{item.power}";
                itemButton.transform.Find("Hit").GetComponent<TextMeshProUGUI>().text = $"{item.hit}";
                itemButton.transform.Find("Crit").GetComponent<TextMeshProUGUI>().text = $"{item.crit}";
            InventoryHoverable IH = itemButton.transform.GetComponent<InventoryHoverable>();
            IH.name = item.ItemName;
            IH.hoverName = item.ItemName;
            IH.description = $"{item.description}";

            if (equipped)
            {
                string nameOfEquipee = "";
                foreach(GameObject unit in Manager.team)
                {
                    UnitBehavior unitUB = unit.GetComponent<UnitBehavior>();
                    if(unitUB.Weapon.ItemName == item.ItemName)
                    {
                        nameOfEquipee = unitUB.UnitName;
                    }
                }
                itemButton.GetComponent<Button>().onClick.RemoveAllListeners();
                itemButton.GetComponentInChildren<TextMeshProUGUI>().color = new((float)0.6, (float)0.6, (float)0.6, 1);
                if (playableMugShots.Where(obj => obj.name == nameOfEquipee + " mugshot").SingleOrDefault() != null)
                {
                    itemButton.GetComponentInChildren<Image>().sprite = playableMugShots.Where(obj => obj.name == nameOfEquipee + " mugshot").SingleOrDefault();
                }
            }
            else
            {
                switch (item.weapontype)
                {
                    case Item.Weapontype.Sword:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[0];
                        break;
                    case Item.Weapontype.Lance:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[1];
                        break;
                    case Item.Weapontype.Axe:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[2];
                        break;
                    case Item.Weapontype.Bow:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[3];
                        break;
                    case Item.Weapontype.Tome:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[4];
                        break;
                    case Item.Weapontype.Receptacle:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[5];
                        break;
                    case Item.Weapontype.Accesory:
                        itemButton.GetComponentInChildren<Image>().sprite = sprites[6];
                        break;
                }
            }          
        }
    }
    public void DisplayKeyItemList(List<Item> ItemList)
    {
        foreach (GameObject itemselector in ItemSelectorList)
        {
            itemselector.GetComponent<Image>().sprite = ItemSelectorListSprites[2];
        }
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            GameObject itemButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            itemButton.GetComponent<Button>().onClick.AddListener(() => InstantiateKeyItem(item));
            itemButton.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName;
            itemButton.GetComponentInChildren<Image>().sprite = sprites[0];
        }
    }
    public void DisplaySkillList(int skillSLot)
    {
        selectedUnit.skillInventory.Sort();
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (string skill in selectedUnit.skillInventory)
        {
            if (!selectedUnit.skills.Contains(skill))
            {
                GameObject SkillButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
                SkillButton.GetComponent<Button>().onClick.AddListener(() => EquipSkill(skill, skillSLot));
                SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = skill;
                if (skillIcons.Where(obj => obj.name == skill).SingleOrDefault() != null)
                {
                    SkillButton.GetComponentInChildren<Image>().sprite = skillIcons.Where(obj => obj.name == skill).SingleOrDefault();
                }
            }
        }
    }
    public void DisplaySoulList()
    {
        selectedUnit.soulInventory.Sort();
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (string soul in selectedUnit.soulInventory)
        {
            GameObject SkillButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            SkillButton.GetComponent<Button>().onClick.AddListener(() => EquipSoul(soul));
            SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = soul;
            if (skillIcons.Where(obj => obj.name == soul).SingleOrDefault() != null)
            {
                SkillButton.GetComponentInChildren<Image>().sprite = skillIcons.Where(obj => obj.name == soul).SingleOrDefault();
            }
        }
    }
    public void UpdateSkillName()
    {
        while (selectedUnit.skills.Count <= 4)
        {
            selectedUnit.skills.Add("");
        }
        int c = 0;
        foreach(TextMeshProUGUI tmpugui in skillNames)
        {
            tmpugui.text = selectedUnit.skills[c];
            if(tmpugui.GetComponentInParent<InventoryHoverable>() != null)
            {
            tmpugui.GetComponentInParent<InventoryHoverable>().hoverName = selectedUnit.skills[c];
            tmpugui.GetComponentInParent<InventoryHoverable>().description = Description(selectedUnit.skills[c]);
            }
            if (selectedUnit.skills[c] == "")
            {
                tmpugui.text = "vazio";
            }
            c++;
        }
    }
    public void UpdateSoulName()
    {
        if (soulName.GetComponentInParent<InventoryHoverable>() != null)
        {
            soulName.text = selectedUnit.equipedSoul;
            soulName.GetComponentInParent<InventoryHoverable>().hoverName = selectedUnit.equipedSoul;
            soulName.GetComponentInParent<InventoryHoverable>().description = Description(selectedUnit.equipedSoul);
            soulIconObject.GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == selectedUnit.equipedSoul).SingleOrDefault();
        }
    }
    public void DisplaySwordList()
    {
        CurrentItemList = Manager.SwordList;
        DisplayItemList(Manager.SwordList);
        ItemSelectorList[0].GetComponent<Image>().sprite = ItemSelectorListSprites[0];
    }
    public void DisplayLanceList()
    {
        CurrentItemList = Manager.LanceList;

        DisplayItemList(Manager.LanceList);
        ItemSelectorList[1].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayAxeList()
    {
        CurrentItemList = Manager.AxeList;

        DisplayItemList(Manager.AxeList);
        ItemSelectorList[2].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayBowList()
    {
        CurrentItemList = Manager.BowList;

        DisplayItemList(Manager.BowList);
        ItemSelectorList[3].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayTomeList()
    {
        CurrentItemList = Manager.TomeList;

        DisplayItemList(Manager.TomeList);
        ItemSelectorList[4].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayReceptacleList()
    {
        CurrentItemList = Manager.ReceptacleList;

        DisplayItemList(Manager.ReceptacleList);
        ItemSelectorList[5].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayAccesoriesList()
    {
        CurrentItemList = Manager.AccesoriesList;

        DisplayItemList(Manager.AccesoriesList);
        ItemSelectorList[6].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayAllList()
    {
        CurrentItemList = Manager.Inventory;
        DisplayItemList(Manager.Inventory);
        ItemSelectorList[8].GetComponent<Image>().sprite = ItemSelectorListSprites[1];
    }
    public void DisplayKeyItems()
    {
        CurrentItemList = Manager.KeyItems;

        DisplayKeyItemList(Manager.KeyItems);
        ItemSelectorList[7].GetComponent<Image>().sprite = ItemSelectorListSprites[1];

    }
    public void DisplayTargetChoice(string target = "", bool primaryTarget = true)
    {
        foreach (Transform child in TargetStatPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Manager = FindObjectOfType<GameManager>();
        if (Manager != null)
        {
            Manager.ParseWeaponList();
        }
        foreach (GameObject itemselector in ItemSelectorList)
        {
            itemselector.GetComponent<Image>().sprite = ItemSelectorListSprites[2];
        }
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        if (target != "Weapon" && target != "ClassID")
        {
            List<string> Stats = new() { "vida", "força", "magia", "destreza", "velocidade", "defesa fisica", "defesa magica", "sorte" };
            TargetStatPanel.transform.gameObject.SetActive(true);
            foreach (string stat in Stats)
            {
                GameObject targetButton = Instantiate(TargetStatButton, TargetStatPanel.transform);
                targetButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = stat;
                switch (stat)
                {
                    case "vida": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 0, primaryTarget)); break;
                    case "força": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 1, primaryTarget)); break;
                    case "magia": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 2, primaryTarget)); break;
                    case "destreza": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 3, primaryTarget)); break;
                    case "velocidade": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 4, primaryTarget)); break;
                    case "defesa fisica": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 5, primaryTarget)); break;
                    case "defesa magica": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 6, primaryTarget)); break;
                    case "sorte": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 7, primaryTarget)); break;
                    default: targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 0, primaryTarget)); break;
                }
                targetButton.GetComponent<Button>().onClick.AddListener(() => DisplayAllList());
                targetButton.GetComponent<Button>().onClick.AddListener(() => TargetStatPanel.transform.parent.gameObject.SetActive(false));
                targetButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                targetButton.GetComponentInChildren<Image>().sprite = statIcons.Where(obj => obj.name == stat + " icone").SingleOrDefault();
            }
        }
        else if (target == "Weapon")
        {
            List<string> Stats = new() { "Espada", "Lança", "Machado", "Arco", "Tomo", "Receptaculo" };
            TargetStatPanel.transform.gameObject.SetActive(true);
            foreach (string stat in Stats)
            {
                GameObject targetButton = Instantiate(TargetStatButton, TargetStatPanel.transform);
                targetButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = stat;
                switch (stat)
                {
                    case "Lança": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 1, primaryTarget)); break;
                    case "Machado": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 2, primaryTarget)); break;
                    case "Arco": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 3, primaryTarget)); break;
                    case "Tomo": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 4, primaryTarget)); break;
                    case "Receptaculo": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 5, primaryTarget)); break;
                    default: targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 0, primaryTarget)); break;
                }
                targetButton.GetComponent<Button>().onClick.AddListener(() => DisplayAllList());
                targetButton.GetComponent<Button>().onClick.AddListener(() => TargetStatPanel.transform.parent.gameObject.SetActive(false));
            }
        }
        else if (target == "ClassID")
        {
            List<string> Classes = new() {"Espadachim", "Arqueiro", "Guerreiro", "Soldado", "Feiticeiro", "Mistico", "Prisioneiro",
            "Cavaleiro Encantado","Duelista","Ladino","Atirador","Patrulheiro","Bárbaro","Armadurado","Lanceiro","Paladino","Mago","Curandeiro","Ocultista","Gladiador"};
            TargetStatPanel.transform.gameObject.SetActive(true);
            foreach (string stat in Classes)
            {
                GameObject targetButton = Instantiate(TargetStatButton, TargetStatPanel.transform);
                targetButton.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text = stat;
                switch (stat)
                {
                    case "Arqueiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 1, primaryTarget)); break;
                    case "Guerreiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 2, primaryTarget)); break;
                    case "Soldado": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 3, primaryTarget)); break;
                    case "Feiticeiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 4, primaryTarget)); break;
                    case "Mistico": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 5, primaryTarget)); break;
                    case "Prisioneiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 6, primaryTarget)); break;
                    case "Cavaleiro Encantado": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 7, primaryTarget)); break;
                    case "Duelista": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 8, primaryTarget)); break;
                    case "Ladino": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 9, primaryTarget)); break;
                    case "Atirador": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 10, primaryTarget)); break;
                    case "Patrulheiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 11, primaryTarget)); break;
                    case "Bárbaro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 12, primaryTarget)); break;
                    case "Armadurado": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 13, primaryTarget)); break;
                    case "Lanceiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 14, primaryTarget)); break;
                    case "Paladino": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 15, primaryTarget)); break;
                    case "Mago": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 16, primaryTarget)); break;
                    case "Curandeiro": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 17, primaryTarget)); break;
                    case "Ocultista": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 18, primaryTarget)); break;
                    case "Gladiador": targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 19, primaryTarget)); break;
                    default: targetButton.GetComponent<Button>().onClick.AddListener(() => ChangeTargeting(target, 0, primaryTarget)); break;
                }
                targetButton.GetComponent<Button>().onClick.AddListener(() => DisplayAllList());
                targetButton.GetComponent<Button>().onClick.AddListener(() => TargetStatPanel.transform.parent.gameObject.SetActive(false));
            }
        }
    }
    public string Description(string skillName)
    {
        return skillName switch
        {
            //Class Tier 1 skills
            //Espadachim
            "Dano Ascendente" => "A cada 3 vezes que fazer um ataque básico, Aumenta o Dano em 1 e Redução de dano em 1 pelo resto do combate",
            "Ataque Rápido" => "%DES de quando fazer um ataque basico, em vez disso fazer 2 ataques basicos.",
            //Arqueiro
            "Foco" => "Ganha +20 de Acerto e Evasao por 15 Segundos no inicio da batalha",
            "Precisão Mortal" => "Converte o Acerto maior que 100 quando alvejar um inimigo em % de Dano." ,
            //Guerreiro
            "Lutador Versátil" => "Ganha efeitos baseado na posicao do usuario: 1: Ganha +3 de Redução de Dano , 2 ou 3: Causa 3 a mais de dano.",
            "Durão" => "Aumenta o HP maximo em 25%.",
            //Prisioneiro
            "Persistência" => "Ganha mais 1 de Velocidade para cada 10 de vida perdida.",
            "Técnica Improvisada" => "Ganha efeitos baseado na posição do usuario: 1: Ganha +20 de Evasao e Acerto quando esta com <50% de Vida. 2: Ganha mais 2 de Dano e 2 Defesa Fisica e Defesa Magica. 3: Ganha 5 de Critico e Dano quando esta com >90% de Vida.",
            //Soldado
            "Presença Inabalável" => "Ganha mais 20% de Defesa Fisica e Defesa Magica quando estiver com menos de 50% de Vida.",
            "Pancada" => "DES% de causar mais 30% da Defesa Fisica como dano adicional no ataque básico.",
            //Feiticeiro
            "Concentração de Feiticeiro" => "Ganha mais 15% de Magia se a Vida estiver Cheia.",
            "Magia Destrutiva" => "DES% de chance de causar mais 40% do sua Magia quando usa um ataque básico.",
            "Sabedoria Arcana" => "Ganha mais 25% de experiência.",
            //Místico
            "Encantamento" => "Aumenta a Velocidade do Aliado na Frente do usuário em 15%.",
            "Maldição" => "Diminui a Velocidade do Inimigo na posição oposta do usuário em 15%.",
            //Shop Skills
            "Maestria Posição 1" => "No inicio do combate ganha +3 de Redução de Dano quando posicionado na Posição 1." ,
            "Maestria Posição 2" => "No inicio do combate ganha +15 de Acerto e Evasão quando posicionado na Posição 2.",
            "Maestria Posição 3" => "No inicio do combate ganha +3 de Dano quando posicionado na Posição 3." ,
            "Começo Afortunado" => "Ganha +12 de Evasão e Acerto pelos primeiros 16 segundos da batalha, além disso começa com +15 de Alma.",
            //Class Tier 1 Souls
            "Poder Oculto" => "Ataca 2 vezes com 75% do Dano cada ataque no alvo, o primeiro golpe recupera 50% do dano causado e o 2 atordoa o inimigo por 2 segundos, Custo 100",
            "Ataque Inspirador" => "Causa 125% do Dano no alvo e aumenta a Velocidade e Dano de todos os aliados em 20% por 10 segundos, Custo 130. ",
            "Golpe Triplo" => "Ataca 3 vezes com 50% do Dano. Custo 70. ",
            "Golpe Focado" => "Causa 150% do Dano, esta alma tem chance dobrada de critico e cura 20% do Dano do Usuário se o ataque for um critico. Custo 100. ",
            "Tiro Certeiro" => "Ataca o alvo atual com um golpe que causa 150% do Dano e não pode errar. Custo 80. ",
            "Rajada de Flechas" => "(Só pode ser usada quando estiver usando um arco) Causa 60% do Dano em todos os inimigos. Custo 110. ",
            "Golpe Poderoso" => "Ataca o alvo causando 200% do Dano, mas com -25 de Acerto. Custo 100. ",
            "Revigoramento" => "Cura 30% do hp máximo ou se o hp estiver cheio, ataca o alvo com um golpe que causa 100% do Dano + 15% do HP máximo do usuário como dano. Custo 100. ",
            "Golpe Atordoante " => "Ataca o alvo atual causando 50% de dano a mais e atordoamento de 2 segundos. Custo 110. ",
            "Fortificar" => "Aumenta a Defesa Física em 15% pelo resto do combate. Custo 110. ",
            "Bola de Fogo" => "(Só pode ser usada se tiver uma arma magica) Causa 125% do Dano no alvo que ignora 50% da defesa magica dele. Custo 70 ",
            "Trovoada" => "(Só pode ser usada se tiver uma arma magica) Causa dano no alvo atual equivalente a 25% da vida máxima dele como dano magico. Custo 100. ",
            "Disparo de Gelo" => "(Só pode ser usada se tiver uma arma magica) Causa dano no alvo igual a 125% do Dano e diminui a evasão e a velocidade em 10% + 10% da sua magia por 15 segundos. Custo 80. ",
            "Restauração Espiritual" => "Restaura a vida do aliado com menos vida em 10 + 20% da magia do usuário. Custo 110. ",
            "Benção dos Ventos" => "Aumenta a evasão de todos os aliados em 10% + 20% da Magia do usuário por 20 segundos. Custo 115. ",



            _ => skillName,
        };
    }
    public void SoulPrice(string soulName)
    {
        Manager.SoulPrice(soulName, selectedUnit);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skillNames[0].GetComponentInParent<InventoryHoverable>().activate = false;
            skillNames[1].GetComponentInParent<InventoryHoverable>().activate = false;
            skillNames[2].GetComponentInParent<InventoryHoverable>().activate = false;
            skillNames[3].GetComponentInParent<InventoryHoverable>().activate = false;
            soulName.GetComponentInParent<InventoryHoverable>().activate = false;
            skillNames[0].GetComponentInParent<InventoryHoverable>().HoverObject.GetComponent<DontDestroyHoverObject>().HoverOn = false;
            Toggle();
        }
    }
}
