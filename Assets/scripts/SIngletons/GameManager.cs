using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>, IDataPersistence
{
    UnitData CurrentUnitData;

    public List<UnitData> units;
    public Dictionary<int, DialogueGraph> eventDictionary;
    public Scene m_scene;
    public int money;
    public int lumenita;
    public int day;
    public bool testMode;
    public bool storyBattle;
    public int BossBattleID = 0;
    public bool TimeIsDay;
    public bool wakeUpTalk;
    public List<GameObject> mapButtons;
    public List<DialogueGraph> graphs;
    public DayResultsManager DayResultsManager;
    public CalendarioUI cui;
    public UnitBehavior selectedUB4Activity;
    public List<GameObject> playerUnit = new();
    public List<GameObject> enemyUnit = new();
    public List<GameObject> team;
    public List<GameObject> teamPostPreBattle = new();
    public List<GameObject> enemyTeamPostPreBattle = new();
    public UnitBehavior selectedPlayerBehavior;
    public UnitBehavior selectedEnemyBehavior;
    public TextMeshPro moneyText;
    public List<Item> Inventory;
    public List<Item> KeyItems;
    public List<string> StoryFlags;
    [Header("ItemParseLists")]
    public List<Item> SwordList;
    public List<Item> LanceList;
    public List<Item> AxeList;
    public List<Item> BowList;
    public List<Item> TomeList;
    public List<Item> ReceptacleList;
    public List<Item> AccesoriesList;
    public List<Item> ExampleList;
    [Header("ClassChange")]
    public GameObject SelectedUBClassChange;


    public void Start()
    {
        LoadTeam();
        if (moneyText != null)
        {
            moneyText.text = "Dinheiro:" + money;
        }
        SelectedUBClassChange = team[0];
    }
    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
        this.Inventory = data.Inventory;
        this.KeyItems = data.KeyItems;
    }
    public void SaveData(ref GameData data)
    {
        int count = 0;
        units = new List<UnitData>();
        foreach (GameObject unitInTeam in team)
        {
            CurrentUnitData = new();
            UnitBehavior CurrentUnitBehavior = unitInTeam.GetComponent<UnitBehavior>();
            SaveUnitasData(CurrentUnitBehavior);
            Debug.Log(CurrentUnitData.UnitName);
            units.Add(CurrentUnitData);
            count++;
        }
        data.money = this.money;
        data.day = this.day;
        data.Inventory = this.Inventory;
        data.KeyItems = this.KeyItems;
        count = 0;
        data.units = units;
        Debug.Log(data.units);
    }
    public void PrepScreen()
    {
        SceneManager.LoadScene("Preparation1A");
    }
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }
    public void SceneLoader(string str)
    {
        SceneManager.LoadScene(str);
    }
    public void SaveUnitasData(UnitBehavior CurrentUnitBehavior)
    {
        CurrentUnitData.classId = CurrentUnitBehavior.classId;
        //equip
        if (CurrentUnitBehavior.Weapon != null)
        {
            CurrentUnitData.Weapon = CurrentUnitBehavior.Weapon;
        }
        if (CurrentUnitBehavior.Accesory != null)
        {
            CurrentUnitData.Accesory = CurrentUnitBehavior.Accesory;
        }
        //parameters
        CurrentUnitData.UnitName = CurrentUnitBehavior.UnitName;
        CurrentUnitData.currentLevel = CurrentUnitBehavior.currentLevel;
        CurrentUnitData.expmarkplier = CurrentUnitBehavior.expmarkplier;
        CurrentUnitData.currentRank = CurrentUnitBehavior.currentRank;
        CurrentUnitData.currentExp = CurrentUnitBehavior.currentExp;
        CurrentUnitData.ClassID = CurrentUnitBehavior.ClassID;
        CurrentUnitData.ClassLevel = CurrentUnitBehavior.ClassLevel;
        CurrentUnitData.hit = CurrentUnitBehavior.hit;
        CurrentUnitData.avoid = CurrentUnitBehavior.avoid;
        CurrentUnitData.crit = CurrentUnitBehavior.crit;
        //stats
        CurrentUnitData.maxhp = CurrentUnitBehavior.maxhp;
        CurrentUnitData.hp = CurrentUnitBehavior.hp;
        CurrentUnitData.power = CurrentUnitBehavior.power;
        CurrentUnitData.str = CurrentUnitBehavior.str;
        CurrentUnitData.mag = CurrentUnitBehavior.mag;
        CurrentUnitData.dex = CurrentUnitBehavior.dex;
        CurrentUnitData.def = CurrentUnitBehavior.def;
        CurrentUnitData.mdef = CurrentUnitBehavior.mdef;
        CurrentUnitData.defenses = CurrentUnitBehavior.defenses;
        CurrentUnitData.luck = CurrentUnitBehavior.luck;
        CurrentUnitData.speed = CurrentUnitBehavior.speed;
        //sistema de skills
        CurrentUnitData.skills = CurrentUnitBehavior.skills;
        CurrentUnitData.skillInventory = CurrentUnitBehavior.skillInventory;
        CurrentUnitData.equipedSoul = CurrentUnitBehavior.equipedSoul;
        CurrentUnitData.equippedSoulIsAttack = CurrentUnitBehavior.equippedSoulIsAttack;
        CurrentUnitData.soulInventory = CurrentUnitBehavior.soulInventory;
        CurrentUnitData.soul = CurrentUnitBehavior.soul;
        CurrentUnitData.maxsoul = CurrentUnitBehavior.maxsoul;
        CurrentUnitData.soulgain = CurrentUnitBehavior.soulgain;
        CurrentUnitData.damagereduction = CurrentUnitBehavior.damagereduction;
        CurrentUnitData.lifesteal = CurrentUnitBehavior.lifesteal;
        CurrentUnitData.armorpen = CurrentUnitBehavior.armorpen;
        CurrentUnitData.magicpen = CurrentUnitBehavior.magicpen;
        //cooking
        CurrentUnitData.cooking = CurrentUnitBehavior.cooking;
        //growths
        CurrentUnitData.growths = CurrentUnitBehavior.growths;
        //learnset
        CurrentUnitData.classSkill = CurrentUnitBehavior.classSkill;
        CurrentUnitData.personalSkill = CurrentUnitBehavior.personalSkill;
        CurrentUnitData.baseSkill = CurrentUnitBehavior.baseSkill;
        CurrentUnitData.skill1 = CurrentUnitBehavior.skill1;
        CurrentUnitData.skill2 = CurrentUnitBehavior.skill2;
        CurrentUnitData.skill3 = CurrentUnitBehavior.skill3;
        CurrentUnitData.baseSoul = CurrentUnitBehavior.baseSoul;
        CurrentUnitData.soul1 = CurrentUnitBehavior.soul1;
        CurrentUnitData.soul2 = CurrentUnitBehavior.soul2;
        CurrentUnitData.soul3 = CurrentUnitBehavior.soul3;
        CurrentUnitData.description = CurrentUnitBehavior.description;
    }
    public void OnLevelWasLoaded()
    {
        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Battle Text");
        moneyText = tempGameObject.GetComponent<TextMeshPro>();
        if (moneyText != null)
        {
            moneyText.text = "Dinheiro:" + money;
        }
    }
    public void LoadTeam()
    {
        foreach (GameObject obj in playerUnit)
        {
            GameObject newobj = Instantiate(obj, this.transform);
            team.Add(newobj);
            newobj.name = newobj.GetComponent<UnitBehavior>().UnitName + "Prep";
            SoulPrice(newobj.GetComponent<UnitBehavior>().equipedSoul, newobj.GetComponent<UnitBehavior>());
        }
    }
    public void AddtoTeam(GameObject recruit)
    {
        GameObject newobj = Instantiate(recruit, this.transform);
        team.Add(newobj);
        newobj.name = newobj.GetComponent<UnitBehavior>().UnitName + "Prep";
    }
    public void SelectUnit(GameObject unit)
    {
        GameObject newunit = unit;
        team.Remove(unit);
        team.Insert(0, newunit);
    }
    public void ParseWeaponList()
    {
        foreach (Item ExampleItem in ExampleList)
        {
            switch (ExampleItem.weapontype)
            {
                case Item.Weapontype.Sword:
                    SwordList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Sword)
                        {
                            SwordList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Lance:
                    LanceList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Lance)
                        {
                            LanceList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Axe:
                    AxeList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Axe)
                        {
                            AxeList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Bow:
                    BowList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Bow)
                        {
                            BowList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Tome:
                    TomeList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Tome)
                        {
                            TomeList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Receptacle:
                    ReceptacleList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Receptacle)
                        {
                            ReceptacleList.Add(currentItem);
                        }
                    }
                    break;
                case Item.Weapontype.Accesory:
                    AccesoriesList.Clear();
                    foreach (Item currentItem in Inventory)
                    {
                        if (currentItem.weapontype == Item.Weapontype.Accesory)
                        {
                            AccesoriesList.Add(currentItem);
                        }
                    }
                    break;
            }
        }
    }
    public void ClassChange( int ClassId)
    {
        GameObject Unit = SelectedUBClassChange;
        UnitBehavior OriginalUB = Unit.GetComponent<UnitBehavior>();
        CopyUnitBehavior(OriginalUB, Unit, ClassId);
        Destroy(OriginalUB);
        Unit.GetComponent<UnitBehavior>().classId = ClassId;
    }
    T CopyUnitBehavior<T>(T original, GameObject destination, int ClassId) where T : Component
    {
        System.Type type = original.GetType();
        Component copy;
        switch (ClassId)
        {
            case (101): copy = destination.AddComponent<Espadachim>(); break;
            case (102): copy = destination.AddComponent<Guerreiro>(); break;
            case (103): copy = destination.AddComponent<Soldado>(); break;
            case (104): copy = destination.AddComponent<Feitiçeiro>(); break;
            case (105): copy = destination.AddComponent<Místico>(); break;
            case (106): copy = destination.AddComponent<Arqueiro>(); break;
            case (107): copy = destination.AddComponent<Prisioneiro>(); break;

            default:
                 copy = destination.AddComponent(type);
                break;
        }
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        Destroy(original);
        return copy as T;
    }
        public void Sleep()
    {
        if (storyBattle == true)
        {
            NodeParser dm = FindObjectOfType<NodeParser>(true);
            dm.StartDialogue(graphs.Where(obj => obj.name == "Batalha Mandatoria").SingleOrDefault());
        }
        else
        {
            DayResultsManager = FindObjectOfType<DayResultsManager>(true);
            DayResultsManager.Sleep();
            cui = FindObjectOfType<CalendarioUI>(true);
            cui.UIUpdate();
            wakeUpTalk = true;
            storyBattle = true;
            GameEventHandler();
        }
    }
    public void GameEventHandler()
    {
        m_scene = SceneManager.GetActiveScene();
        /*if(day == 4 || day == 8 || day == 12)
        {
            storyBattle = true;
        }
        else
        {
            storyBattle = false;
        }*/
        switch (m_scene.name)
        {
            case "Preparation1A":
                Debug.Log("prep1A");
                GameObject go = GameObject.Find("SceneInteractables" + m_scene.name);
                if (go.transform.Find("cama") != null)
                {
                    Button button = go.transform.Find("cama").GetComponent<Button>();
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(delegate { go.transform.Find("cama").GetComponent<GMButtonAssigner>().Sleep(); });
                }
                if (storyBattle)
                {
                    ChangeGraph(graphs.Where(obj => obj.name == "Batalha Mandatoria").SingleOrDefault(), "cama", m_scene.name);
                }
                if (day == 1 && wakeUpTalk)
                {
                    NodeParser dm = FindObjectOfType<NodeParser>(true);
                    dm.StartDialogue(graphs.Where(obj => obj.name == "Gandios Dia 1").SingleOrDefault());
                    mapButtons.Where(obj => obj.name == "Cantina").SingleOrDefault().SetActive(true);
                    wakeUpTalk = false;
                }
                if (day == 2 && wakeUpTalk)
                {
                    Debug.Log("DIA 2");
                    NodeParser dm = FindObjectOfType<NodeParser>(true);
                    dm.StartDialogue(graphs.Where(obj => obj.name == "Adrian Começo Dia 2").SingleOrDefault());
                    mapButtons.Where(obj => obj.name == "Patio").SingleOrDefault().SetActive(true);
                    wakeUpTalk = false;
                }
                if(day== 5 && storyBattle)
                {
                    BossBattleID = 101;
                }
                if (day == 6 && wakeUpTalk)
                {
                    Debug.Log("DIA 6");
                    NodeParser dm = FindObjectOfType<NodeParser>(true);
                    dm.StartDialogue(graphs.Where(obj => obj.name == "Gandios Dia 6").SingleOrDefault());
                    mapButtons.Where(obj => obj.name == "Patio").SingleOrDefault().SetActive(true);
                    wakeUpTalk = false;
                }
                break;
            case "Patio":
                if (day >= 3)
                {
                    ChangeSprite("Leyni Sprite", 1);
                    //ChangeGraph(graphs[3], "Leyni Interactable",m_scene.name);
                }
                break;

        }
    }
    public void ChangeGraph(DialogueGraph dialogueGraph, string buttonAssigner, string sceneName)
    {
        GameObject go = GameObject.Find("SceneInteractables" + sceneName);
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().graph = dialogueGraph;
        Button button = go.transform.Find(buttonAssigner).GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().AddListener();
    }
    public void GmChangeGraph(DialogueGraph dialogueGraph, string buttonAssigner, string sceneName)
    {
        GameObject go = GameObject.Find("SceneInteractables" + sceneName);
        Debug.Log(go.name);
        foreach (Transform obj in go.transform)
        {
            Debug.Log(obj.name);
        }
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().graph = dialogueGraph;
        Button button = go.transform.Find(buttonAssigner).GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        go.transform.Find(buttonAssigner).GetComponent<ButtonAssigner>().AddListener();
    }
    public void ChangeSprite(string spriteObject, int targetSprite)
    {
        GameObject go = GameObject.Find(spriteObject);
        go.GetComponent<SpriteChanger>().ChangeSprite(targetSprite);
    }
    public void SoulPrice(string soulName, UnitBehavior unit)
    {
        switch (soulName)
        {
            //espadachin
            case ("Golpe Triplo"):
                unit.maxsoul = 70;
                break;
            case ("Golpe Focado"):
                unit.maxsoul = 100;
                break;
            //arqueiro
            case ("Tiro Certeiro"):
                unit.maxsoul = 80;
                break;
            case ("Rajada de Flechas"):
                unit.maxsoul = 110;
                break;
            //Guerreiro
            case ("Golpe Poderoso"):
                unit.maxsoul = 100;
                break;
            case ("Revigoramento"):
                unit.maxsoul = 100;
                break;
            //soldado
            case ("Golpe Atordoante"):
                unit.maxsoul = 110;
                break;
            case ("Fortificar"):
                unit.maxsoul = 110;
                break;
            //Feiticeiro
            case ("Bola de Fogo"):
                unit.maxsoul = 70;
                break;
            case ("Trovoada"):
                unit.maxsoul = 100;
                break;
            case ("Disparo de Gelo"):
                unit.maxsoul = 80;
                break;
            //mistico
            case ("Restauração Espiritual"):
                unit.maxsoul = 110;
                break;
            case ("Benção dos ventos"):
                unit.maxsoul = 115;
                break;
            //Prisioneiro
            case ("Poder Oculto"):
                unit.maxsoul = 100;
                break;
            case ("Ataque Inspirador"):
                unit.maxsoul = 130;
                break;
            default: unit.maxsoul = 100; break;
        }
        switch (soulName)
        {
            case ("Golpe Poderoso"):
                unit.equippedSoulIsAttack = false;
                break;
            case ("Revigoramento"):
                unit.equippedSoulIsAttack = false;
                break;
            case ("Fortalecimento"):
                unit.equippedSoulIsAttack = false;
                break;
            case ("Restauração Espiritual"):
                unit.equippedSoulIsAttack = false;
                break;
            case ("Benção dos Ventos"):
                unit.equippedSoulIsAttack = false;
                break;
            default:
                unit.equippedSoulIsAttack = true;
                break;

        }
    }
}
