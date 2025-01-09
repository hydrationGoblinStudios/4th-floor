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
    public GameObject calendario;
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
    public bool Activatable;

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

    public List<GameObject> DragAndDroppables;


    [Header("Hover Object")]
    public TextMeshPro[] texts;
    public GameObject hoverObject;
    public bool HoverOn;
    public void Toggle()
    {
        if(SceneInteractable == null)
        {
        SceneInteractable = GameObject.FindGameObjectWithTag("Scene Interactables");
        }
        if (Activatable)
        {
            if(SceneInteractable != null )
            {
            SceneInteractable.SetActive(!SceneInteractable.activeInHierarchy);
            }
            gameObject.SetActive(!gameObject.activeInHierarchy);
            calendario.SetActive(!calendario.activeInHierarchy);
        }
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
        Select(selectedUnit);
    }
    public void EquipSoul(string soulName)
    {
        selectedUnit.equipedSoul = soulName;
        //valor de alma maxima
        SoulPrice(soulName);
        Select(selectedUnit);
    }
    public void InstantiateKeyItem(GameObject button, Item item)
    {
        Toggle();
        Instantiate(DragAndDroppables.Where(obj => obj.name == item.name).SingleOrDefault(), gameObject.GetComponentInParent<Canvas>().transform);
    }
    public void Select(UnitBehavior unitBehavior)
    {
        selectedUnit = unitBehavior;
        statTexts[0].text = selectedUnit.maxhp.ToString(); statTexts[1].text = selectedUnit.str.ToString(); statTexts[2].text = selectedUnit.mag.ToString();
        statTexts[3].text = selectedUnit.dex.ToString(); statTexts[4].text = selectedUnit.speed.ToString();
        statTexts[5].text = selectedUnit.def.ToString(); statTexts[6].text = selectedUnit.mdef.ToString(); statTexts[7].text = selectedUnit.luck.ToString();
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
        equipText.text = "Arma:\n" + unitBehavior.Weapon.ItemName + "\nAtk:" + unitBehavior.Weapon.str + "\nAcerto:" + unitBehavior.Weapon.hit + "\nCrit:" + unitBehavior.Weapon.crit;
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
        SoulPrice(selectedUnit.equipedSoul);
        foreach(GameObject go in skillIconsObjects)
        {
            go.GetComponent<SpriteRenderer>().sprite = skillIcons.Where(obj => obj.name == skillNames[c].text).SingleOrDefault();
            c++;
        }
        DisplayItemList(Manager.Inventory);
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
    public void DisplayKeyItemList(List<Item> ItemList)
    {
        while (panel.transform.childCount > 0)
        {
            DestroyImmediate(panel.transform.GetChild(0).gameObject);
        }
        foreach (Item item in ItemList)
        {
            GameObject itemButton = Instantiate(ItemButtonPrefab, ItemSelectPanel.transform);
            itemButton.GetComponent<Button>().onClick.AddListener(() => InstantiateKeyItem(itemButton, item));
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
    public void DisplayKeyItems()
    {
        DisplayKeyItemList(Manager.KeyItems);
    }


    public string Description(string skillName)
    {
        return skillName switch
        {
            //Class tier 1 skills
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


            _ => skillName,
        };
    }
    public void SoulPrice(string soulName)
    {
        Manager.SoulPrice(soulName, selectedUnit);
    }
    public void Update()
    {
        if(hoverObject == null)
        {
            hoverObject = GameObject.FindGameObjectWithTag("Hover Object");
            texts = hoverObject.GetComponentsInChildren<TextMeshPro>();
        }

        if (HoverOn && texts[1].text != "")
        {
        hoverObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hoverObject.transform.position = new Vector3(hoverObject.transform.position.x, hoverObject.transform.position.y, 0);
        }
        else
        {
            hoverObject.transform.position = new Vector3(100, 200, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }
}
