using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public GameObject GameManagerOBJ;
    public GameObject SceneInteractable;
    [HideInInspector] public GameManager Manager;
    public GameObject panel;
    public GameObject charSelectPanel;
    public GameObject ItemSelectPanel;
    public GameObject unitSelectPanel;
    public GameObject buttonPrefab;
    public GameObject ItemButtonPrefab;
    public GameObject UnitSelectButton;
    public UnitBehavior selectedUnit;
    public Sprite[] sprites;
    public Sprite[] skillIcons;
    public GameObject[] skillIconsObjects;
    public List<TextMeshProUGUI> skillNames;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI accesoryText;
    public NodeParser nodeParser;

    [Header("Ui top")]
    public SpriteRenderer MugShot;
    public Sprite[] playableMugShots;
    public TextMeshProUGUI UITopName;
    public TextMeshProUGUI LvlText;
    public Slider expbar;
    public Image ClassIcon;
    public SpriteRenderer WeaponImage;
    public SpriteRenderer AccesoryImage;
    public Sprite[] EquipableImages;


    [Header("Hover Object")]
    public GameObject hoverObject;
    public bool HoverOn;
    public void Toggle()
    {
        if(SceneInteractable == null)
        {
        SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
        }
        if(SceneInteractable != null)
        {
        SceneInteractable.SetActive(!SceneInteractable.activeInHierarchy);
        }
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void Equip(Item item)
    {if(item.type == Item.Type.weapon)
        {
            selectedUnit.Weapon = item;
            UpdateEquips(item);
        }
        else
        {
            selectedUnit.Accesory = item;
            UpdateAccesory(item);
        }
        UpdateUITop();
    }
    public void EquipSkill(string Skill, int SkillSlot)
    {
        while (selectedUnit.skills.Count <= SkillSlot)
        {
            selectedUnit.skills.Add("");
        }
        selectedUnit.skills[SkillSlot] = Skill;
        skillIconsObjects[SkillSlot].GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == Skill).SingleOrDefault();
        UpdateInventory();
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString(); statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
        equipText.text = "Arma:\n" + unitBehavior.Weapon.ItemName + "\nAtk:" + unitBehavior.Weapon.str + "\nAcerto:" + unitBehavior.Weapon.hit + "\nCrit:" + unitBehavior.Weapon.crit;
        if(unitBehavior.Accesory != null)
        {
            accesoryText.text = "Accesorio:\n" + unitBehavior.Accesory.ItemName + "\nAtk:" + unitBehavior.Accesory.str + "\nDef:" + unitBehavior.Weapon.def + "\nDes:" + unitBehavior.Weapon.dex + "\nSorte:" + unitBehavior.Weapon.luck + "\nvel:" + unitBehavior.Weapon.speed;
        }
        else
        {
            accesoryText.text = "";
        }
        UpdateUITop();
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
        DisplayItemList(Manager.Inventory);

        foreach (GameObject unit in Manager.team)
        {
            if (first) 
            {
                GameObject charButton = Instantiate(buttonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
                first = false;
            }
            else 
            {
                GameObject charButton  = Instantiate(buttonPrefab, charSelectPanel.transform);
                charButton.GetComponent<Button>().onClick.AddListener(() => Select(unit.GetComponent<UnitBehavior>()));
                charButton.GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<UnitBehavior>().UnitName;
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
        WeaponImage.sprite = EquipableImages.Where(obj => obj.name == selectedUnit.Weapon.ItemName).SingleOrDefault();
        if (selectedUnit.Accesory != null)
        {
            AccesoryImage.sprite = EquipableImages.Where(obj => obj.name == selectedUnit.Accesory.ItemName).SingleOrDefault();
        }
        else{
            AccesoryImage.sprite = null;
        }
    }
    public void DisplayItemList(List<Item> ItemList)
    {
        Manager.ParseWeaponList();

        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            GameObject itemButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            itemButton.GetComponent<Button>().onClick.AddListener(() => Equip(item));
            itemButton.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName;
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
    public void DisplaySkillList(int skillSLot)
    {
        selectedUnit.skillInventory.Sort();
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (string skill in selectedUnit.skillInventory)
        {
            GameObject SkillButton = Instantiate(ItemButtonPrefab,ItemSelectPanel.transform);
            SkillButton.GetComponent<Button>().onClick.AddListener(() => EquipSkill(skill,skillSLot));
            SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = skill;
            SkillButton.GetComponentInChildren<Image>().sprite = skillIcons.Where(obj => obj.name == skill).SingleOrDefault();
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
    public void DisplaySwordList()
    {
        DisplayItemList(Manager.SwordList);
    }
    public void DisplayLanceList()
    {
        DisplayItemList(Manager.LanceList);
    }
    public void DisplayAxeList()
    {
        DisplayItemList(Manager.AxeList);
    }
    public void DisplayBowList()
    {
        DisplayItemList(Manager.BowList);
    }
    public void DisplayTomeList()
    {
        DisplayItemList(Manager.TomeList);
    }
    public void DisplayReceptacleList()
    {
        DisplayItemList(Manager.ReceptacleList);
    }
    public void DisplayAccesoriesList()
    {
        DisplayItemList(Manager.AccesoriesList);
    }

    public string Description(string skillName)
    {
        return skillName switch
        {
            //class tier 1 skills
            //espadachim
            "Dano Ascendente" => "A cada 3 vezes que causar dano, Aumenta o Dano em 1 e Redu??o de dano em 1 pelo resto do combate",
            "Ataque R?pido" => "%DES de quando fazer um ataque basico, em vez disso fazer 2 ataques basicos.",
            //arqueiro
            "Foco" => "Ganha +20 de Acerto e Evas?o por 15 Segundos no inicio da batalha",
            "Precis?o mortal" => "Causa mais Dano equivalente ao Acerto/10.",
            //guerreiro
            "Lutador Vers?til" => "Ganha efeitos baseado na posi??o do usu?rio: 1: Ganha +3 de Defesa Fisica e Defesa Magica , 2 ou 3: Causa 3 a mais de dano.",
            "Dur?o" => "Aumenta o HP m?ximo em 25%.",
            //prisioneiro
            "Persist?ncia" => "Ganha mais 1 de Velocidade para cada 10 de vida perdida.",
            "Tecnica Improvisada" => "Ganha efeitos baseado na posi??o do usu?rio: 1: Ganha +20 de Evas?o e Acerto quando est? com <50% de Vida. 2: Ganha mais 2 de Dano e 2 Defesa Fisica e Defesa Magica. 3: Ganha 5 de Critico e Dano quando est? com >90% de Vida.",
            _ => skillName,
        };
    }
    public void Update()
    {
        TextMeshPro[] texts = hoverObject.GetComponentsInChildren<TextMeshPro>();

        if (HoverOn && texts[1].text != "")
        {
        hoverObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hoverObject.transform.position = new Vector3(hoverObject.transform.position.x, hoverObject.transform.position.y, 0);
        }
        else
        {
            hoverObject.transform.position = new Vector3(100, 200, 0);
        }
    }
}
