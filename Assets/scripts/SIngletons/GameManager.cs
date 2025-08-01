using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
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
    public List<string> unlockedMaps;
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
    public List<Item> Inventory;
    public List<Item> KeyItems;
    public List<Item> StoryFlags;
   // [HideInInspector]
    public int plantaDia = 40;
    [Header("ItemParseLists")]
    public List<Item> SwordList;
    public List<Item> LanceList;
    public List<Item> AxeList;
    public List<Item> BowList;
    public List<Item> TomeList;
    public List<Item> ReceptacleList;
    public List<Item> AccesoriesList;
    public List<Item.Weapontype> weaponTypeList = new() { Item.Weapontype.Sword, Item.Weapontype.Lance, Item.Weapontype.Axe, Item.Weapontype.Bow, Item.Weapontype.Tome, Item.Weapontype.Receptacle, Item.Weapontype.Accesory };
    [Header("ClassChange")]
    public GameObject SelectedUBClassChange;
    [Header("Song manager")]
    public SongManager songManager;
    [Header("Item Lists")]
    public List<Item> equipList;
    public List<Item> keyItemList;
    public List<Item> storyFlagList;

    public enum UIState {Available, Ocuppied}
    public UIState currentState = UIState.Available;


    public void Start()
    {
        StartCoroutine(LoadTeam());
    }
    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.day = data.day;
        //this.storyBattle = data.storyBattle;
        this.storyBattle = true;

        this.Inventory.Clear();
        foreach(int itemID in data.Inventory)
        {
            Inventory.Add(GetbyID(itemID, equipList));
        }
        this.KeyItems.Clear();
        foreach (int itemID in data.KeyItems)
        {
            KeyItems.Add(GetbyID(itemID, keyItemList));
        }
        this.StoryFlags.Clear();
        foreach (int itemID in data.StoryFlags)
        {
            StoryFlags.Add(GetbyID(itemID, storyFlagList));
        }
        this.unlockedMaps = data.unlockedMaps;
        plantaDia = data.plantaDia;
        wakeUpTalk = true;
    }
    public void SaveData(ref GameData data)
    {
        Debug.Log("gamemanager data save"); 
        int count = 0;
        units = new List<UnitData>();
        foreach (GameObject unitInTeam in team)
        {
            CurrentUnitData = new();
            UnitBehavior CurrentUnitBehavior = unitInTeam.GetComponent<UnitBehavior>();
            SaveUnitasData(CurrentUnitBehavior);
            units.Add(CurrentUnitData);
            count++;
        }
        data.money = this.money;
        data.day = this.day;
        data.storyBattle = this.storyBattle;
        data.Inventory.Clear();
       foreach (Item itemId in Inventory)
        {
            data.Inventory.Add(itemId.id);
        }
        data.StoryFlags.Clear();
        foreach (Item itemId in StoryFlags)
        {
            data.StoryFlags.Add(itemId.id);
        }
        data.KeyItems.Clear();
        foreach (Item itemId in KeyItems)
        {
            data.KeyItems.Add(itemId.id);
        }
        data.unlockedMaps = this.unlockedMaps;
        data.plantaDia = this.plantaDia;
        data.units = units;
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
            CurrentUnitData.Weapon = CurrentUnitBehavior.Weapon.id;
        }
        else
        {
            CurrentUnitData.Weapon = 0;
        }
        if (CurrentUnitBehavior.Accesory != null)
        {
            CurrentUnitData.Accesory = CurrentUnitBehavior.Accesory.id;
        }
        else
        {
            CurrentUnitData.Accesory = 0;
        }
        CurrentUnitData.UsableWeaponTypes = CurrentUnitBehavior.UsableWeaponTypes;
        //parameters
        CurrentUnitData.UnitName = CurrentUnitBehavior.UnitName;
        CurrentUnitData.currentLevel = CurrentUnitBehavior.currentLevel;
        CurrentUnitData.expmarkplier = CurrentUnitBehavior.expmarkplier;
        CurrentUnitData.currentRank = CurrentUnitBehavior.currentRank;
        CurrentUnitData.currentExp = CurrentUnitBehavior.currentExp;
        CurrentUnitData.ClassID = CurrentUnitBehavior.ClassID;
        CurrentUnitData.ClassLevel = CurrentUnitBehavior.ClassLevel;
        CurrentUnitData.ClassLearning = CurrentUnitBehavior.ClassLearning;
        CurrentUnitData.ClassLearningSerializable = CurrentUnitBehavior.ClassLearningSerializable;
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
        CurrentUnitData.activity = CurrentUnitBehavior.activity;
        CurrentUnitData.fortalecerStat = CurrentUnitBehavior.fortalecerStat;
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
    public IEnumerator LoadDataAsUnit(UnitData CurrentUnitData)
    {
        yield return new WaitForEndOfFrame();
        GameObject newUnit = Instantiate(new GameObject(), transform);
        UnitBehavior CurrentUnitBehavior = newUnit.AddComponent<UnitBehavior>();
        CurrentUnitBehavior.classId = CurrentUnitData.classId;
        //equip
        if (CurrentUnitData.Weapon != 0)
        {
            CurrentUnitBehavior.Weapon = GetbyID(CurrentUnitData.Weapon, equipList );
        }
        if (CurrentUnitData.Accesory != 0)
        {
            CurrentUnitBehavior.Accesory = GetbyID(CurrentUnitData.Accesory, equipList);
        }
        CurrentUnitBehavior.UsableWeaponTypes = CurrentUnitData.UsableWeaponTypes;
        //parameters
        CurrentUnitBehavior.UnitName = CurrentUnitData.UnitName;
        CurrentUnitBehavior.currentLevel = CurrentUnitData.currentLevel;
        CurrentUnitBehavior.expmarkplier = CurrentUnitData.expmarkplier;
        CurrentUnitBehavior.currentRank = CurrentUnitData.currentRank;
        CurrentUnitBehavior.currentExp = CurrentUnitData.currentExp;
        CurrentUnitBehavior.ClassID = CurrentUnitData.ClassID;
        CurrentUnitBehavior.ClassLevel = CurrentUnitData.ClassLevel;
        CurrentUnitBehavior.ClassLearning = CurrentUnitData.ClassLearning;
        CurrentUnitBehavior.ClassLearningSerializable = CurrentUnitData.ClassLearningSerializable;
        CurrentUnitBehavior.hit = CurrentUnitData.hit;
        CurrentUnitBehavior.avoid = CurrentUnitData.avoid;
        CurrentUnitBehavior.crit = CurrentUnitData.crit;
        //stats
        CurrentUnitBehavior.maxhp = CurrentUnitData.maxhp;
        CurrentUnitBehavior.hp = CurrentUnitData.hp;
        CurrentUnitBehavior.power = CurrentUnitData.power;
        CurrentUnitBehavior.str = CurrentUnitData.str;
        CurrentUnitBehavior.mag = CurrentUnitData.mag;
        CurrentUnitBehavior.dex = CurrentUnitData.dex;
        CurrentUnitBehavior.def = CurrentUnitData.def;
        CurrentUnitBehavior.mdef = CurrentUnitData.mdef;
        CurrentUnitBehavior.defenses = CurrentUnitData.defenses;
        CurrentUnitBehavior.luck = CurrentUnitData.luck;
        CurrentUnitBehavior.speed = CurrentUnitData.speed;
        CurrentUnitBehavior.activity = CurrentUnitData.activity;
        CurrentUnitBehavior.fortalecerStat = CurrentUnitData.fortalecerStat;
        //sistema de skills
        CurrentUnitBehavior.skills = CurrentUnitData.skills;
        CurrentUnitBehavior.skillInventory = CurrentUnitData.skillInventory;
        CurrentUnitBehavior.equipedSoul = CurrentUnitData.equipedSoul;
        CurrentUnitBehavior.equippedSoulIsAttack = CurrentUnitData.equippedSoulIsAttack;
        CurrentUnitBehavior.soulInventory = CurrentUnitData.soulInventory;
        CurrentUnitBehavior.soul = CurrentUnitData.soul;
        CurrentUnitBehavior.maxsoul = CurrentUnitData.maxsoul;
        CurrentUnitBehavior.soulgain = CurrentUnitData.soulgain;
        CurrentUnitBehavior.damagereduction = CurrentUnitData.damagereduction;
        CurrentUnitBehavior.lifesteal = CurrentUnitData.lifesteal;
        CurrentUnitBehavior.armorpen = CurrentUnitData.armorpen;
        CurrentUnitBehavior.magicpen = CurrentUnitData.magicpen;
        //cooking
        CurrentUnitBehavior.cooking = CurrentUnitData.cooking;
        //growths
        CurrentUnitBehavior.growths = CurrentUnitData.growths;
        //learnset
        CurrentUnitBehavior.classSkill = CurrentUnitData.classSkill;
        CurrentUnitBehavior.personalSkill = CurrentUnitData.personalSkill;
        CurrentUnitBehavior.baseSkill = CurrentUnitData.baseSkill;
        CurrentUnitBehavior.skill1 = CurrentUnitData.skill1;
        CurrentUnitBehavior.skill2 = CurrentUnitData.skill2;
        CurrentUnitBehavior.skill3 = CurrentUnitData.skill3;
        CurrentUnitBehavior.baseSoul = CurrentUnitData.baseSoul;
        CurrentUnitBehavior.soul1 = CurrentUnitData.soul1;
        CurrentUnitBehavior.soul2 = CurrentUnitData.soul2;
        CurrentUnitBehavior.soul3 = CurrentUnitData.soul3;
        CurrentUnitBehavior.description = CurrentUnitData.description;
        newUnit.name = CurrentUnitBehavior.UnitName;
        SelectedUBClassChange = newUnit;
        //todo
       ClassChange(newUnit.GetComponent<UnitBehavior>().classId, GetbyID(CurrentUnitData.Weapon, equipList));
        
        team.Add(newUnit);
    }

    IEnumerator LoadTeam()
    {
        yield return new WaitForEndOfFrame();
        foreach (GameObject obj in playerUnit)
        {
            GameObject newobj = Instantiate(obj, this.transform);
            team.Add(newobj);
            newobj.name = newobj.GetComponent<UnitBehavior>().UnitName + "Prep";
            SoulPrice(newobj.GetComponent<UnitBehavior>().equipedSoul, newobj.GetComponent<UnitBehavior>());
        }
        SelectedUBClassChange = team[0];
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
        foreach (Item.Weapontype ExampleItem in weaponTypeList)
        {
            switch (ExampleItem)
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
                default:break;
            }
        }
    }
    public void ClassChange(int ClassId, Item item = null)
    {
        GameObject Unit = SelectedUBClassChange;
        UnitBehavior OriginalUB = Unit.GetComponent<UnitBehavior>();
        if(item != null)
        {
        OriginalUB.Weapon = item;
        }
        CopyUnitBehavior(OriginalUB, Unit, ClassId);
        Destroy(OriginalUB);
        }
    UnitBehavior CopyUnitBehavior(UnitBehavior original, GameObject destination, int ClassId)
    {
        original.classId = ClassId;
        switch (ClassId)
        {
            case (101): original.UsableWeaponTypes = new() { Item.Weapontype.Sword }; break;
            case (102): original.UsableWeaponTypes = new() { Item.Weapontype.Axe }; break;
            case (103): original.UsableWeaponTypes = new() { Item.Weapontype.Lance }; break;
            case (104): original.UsableWeaponTypes = new() { Item.Weapontype.Tome }; break;
            case (105): original.UsableWeaponTypes = new() { Item.Weapontype.Receptacle }; break;
            case (106): original.UsableWeaponTypes = new() { Item.Weapontype.Bow }; break;
            case (107): original.UsableWeaponTypes = new() { Item.Weapontype.Sword }; break;
        }
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
        copy.GetComponent<UnitBehavior>().classId = ClassId;
       /* if(copy.GetComponent<UnitBehavior>().Weapon == null)
        {
            copy.GetComponent<UnitBehavior>().Weapon = Inventory[0] ;
        }*/
        return copy as UnitBehavior;
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
           StartCoroutine(cui.UIUpdate());
            wakeUpTalk = true;
            storyBattle = true;
            TimeIsDay = true;
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
        songManager.ChecksScene();
        switch (m_scene.name)
        {
            case "Preparation1A":
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
                    unlockedMaps.Add("Cantina");
                    unlockedMaps.Add("Preparation1A");
                    wakeUpTalk = false;
                }
                if (day == 2 && wakeUpTalk)
                {
                    Debug.Log("DIA 2");
                    NodeParser dm = FindObjectOfType<NodeParser>(true);
                    dm.StartDialogue(graphs.Where(obj => obj.name == "Adrian Começo Dia 2").SingleOrDefault());
                    unlockedMaps.Add("Patio");
                    wakeUpTalk = false;
                }
                if(day== 4 && storyBattle)
                {
                    BossBattleID = 101;
                }
                if (day == 6 && wakeUpTalk)
                {
                    Debug.Log("DIA 6");
                    NodeParser dm = FindObjectOfType<NodeParser>(true);
                    dm.StartDialogue(graphs.Where(obj => obj.name == "Gandios Dia 6").SingleOrDefault());
                    unlockedMaps.Add("Laboratorio");
                    wakeUpTalk = false;
                }
                if(day < 2)
                {
                    Debug.Log(go.name);

                    go.transform.Find("Quadro de tarefas").gameObject.SetActive(false);   
                    GameObject.Find("quadro de tarefas sprite").GetComponent<SpriteRenderer>().enabled = false;
                    
                }
                if(day > 19)
                {
                    GameObject.Find("ObrigadoPorJogar").gameObject.GetComponent<SpriteRenderer>().enabled = true ;
                }
                break;
            case "Patio":
                if (day >= 3)
                {
                    ChangeSprite("Leyni Sprite", 1);
                    ChangeGraph(graphs.Where(obj => obj.name == "Leyni Pátio").SingleOrDefault(), "Leyni Interactable",m_scene.name);
                }
                break;
            case "Dispensa":
                bool t = false;
                foreach(Item item in StoryFlags)
                {
                    if(item.name == "CartaHumano02")
                    {
                        t = true;
                    }
                }

                if (t)
                {
                    Debug.Log("tem Carta 2");
                    ChangeSprite("sacos caixa direita", 1);
                    ChangeSprite("sacos caixa esquerda", 0);
                    ChangeGraph(graphs[6], "sacos caixa esquerda Interactable", m_scene.name);
                    GameObject.Find("sacos caixa esquerda Interactable").SetActive(false);
                }
                if (day >= 9)
                {
                    ChangeGraph(graphs.Where(obj => obj.name == "Pegar Gancho de Carne Semana 2+").SingleOrDefault(), "Gancho De Carne Interactable", m_scene.name);
                ChangeSprite("carnes sprites", 1);
                }
                    break;
            case "Cutscene":

                StartCoroutine(WaitToLoad(graphName: "Noite 0 Figura Misteriosa"));

                break;
            case "Horta":
                if (day >= plantaDia)
                {
                    ChangeGraph(graphs.Where(obj => obj.name == "Interagir Planta Esquisita Regada").SingleOrDefault(), "Planta estranha Interactable", m_scene.name);
                    ChangeSprite("planta murcha Sprite", 1);
                }
                if (day >= plantaDia +4)
                {
                    ChangeGraph(graphs.Where(obj => obj.name == "Interagir Planta esquisita Regada semana 4+").SingleOrDefault(), "Planta estranha Interactable", m_scene.name);
                    ChangeSprite("planta murcha Sprite", 2);
                }
                break;
            case "Abertura":
                if (true)
                {
                    StartCoroutine(WaitToLoad(graphName: "Introdução"));
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
    IEnumerator WaitToLoad(float seconds = 0, string graphName = "")
    {
        if(seconds == 0)
        {
        yield return new WaitForEndOfFrame();
        }
        else
        {
        yield return new WaitForSeconds(seconds);
        }
        NodeParser dm = FindObjectOfType<NodeParser>(true);
        dm.StartDialogue(graphs.Where(obj => obj.name == graphName).SingleOrDefault());
        if(cui != null)
        {
        cui.gameManager = this;
        StartCoroutine(cui.UIUpdate());
        }
    }

    public Item  GetbyID(int itemID, List<Item> ItemList)
    {
        if(itemID != 0)
        {
        return ItemList.Where(obj => obj.id == itemID).SingleOrDefault();
        }
        return null;
    }
}
