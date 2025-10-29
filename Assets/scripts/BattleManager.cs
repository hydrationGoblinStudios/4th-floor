using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
public class BattleManager : MonoBehaviour
{
    public AudioSource[] hitAudio;
    public Animator[] animators;
    public Material[] matRaces;
    public UnitBehavior endure;
    public List<string> skillsInUse;
    public List<string> EAskillsInUse;
    public int battleSpeed = 20;
    //Player1
    public int Phit;
    public int Pcrit;

    public bool sureShot = false;
    public GameObject[] enemyList;
    [HideInInspector]
    public int pAccSpeed;
    [HideInInspector]
    public int eAccSpeed;
    [Header("Game objects")]
    public GameManager gameManager;
    public GameObject playerUnit;
    public GameObject playerUnit2;
    public GameObject playerUnit3;
    public GameObject enemyUnit;
    public GameObject enemyUnit2;
    public GameObject enemyUnit3;
    public Transform playerBattleStation;
    public Transform playerBattleStation2;
    public Transform playerBattleStation3;
    public Transform enemyBattleStation;
    public Transform enemyBattleStation2;
    public Transform enemyBattleStation3;
    public UnitBehavior playerBehavior;
    public UnitBehavior player2Behavior;
    public UnitBehavior player3Behavior;
    public List<UnitBehavior> playerTeam;
    public UnitBehavior enemyBehavior;
    public UnitBehavior enemy2Behavior;
    public UnitBehavior enemy3Behavior;
    public List<UnitBehavior> enemyTeam;

    [Header("UI")]
    //names
    //UI sliders
    public List<Slider> playerHpSlider;
    public List<Slider> enemyHpSlider;
    public List<Slider> PlayerActionBar;
    public List<Slider> EnemyActionBar;
    public List<Slider> PlayerSoulBar;
    public List<Slider> EnemySoulBar;
    //Ui elements
    public GameObject playerGo;
    public GameObject playerGo2;
    public GameObject playerGo3;
    [Header("UI")]
    public float PlayerBar;
    public float EnemyBar;
    public float PlayerBar2;
    public float EnemyBar2;
    public float PlayerBar3;
    public float EnemyBar3;
    public List<GameObject> IconSlots;
    public List<TextMeshProUGUI> DamagePopups;
    public List<float> PlayerBars;
    public List<float> EnemyBars;
    public GameObject speedList;
    [Header("ParticleSystem")]
    public ParticleSystem damageParticlesPlayer1;
    public ParticleSystem damageParticlesPlayer2;
    public ParticleSystem damageParticlesPlayer3;
    public ParticleSystem damageParticlesEnemy1;
    public ParticleSystem damageParticlesEnemy2;
    public ParticleSystem damageParticlesEnemy3;

    [Header("LevelUp")]

    public Animator unitLUPAnimatorP1;
    public List<GameObject> LUPObjects;
    public List<TextMeshProUGUI> levelUpUnitStatsP1;
    public TextMeshProUGUI levelTextP1;
    public TextMeshProUGUI LevelUpNameTextP1;
    public Slider expSliderP1;

    public Animator unitLUPAnimatorP2;
    public List<TextMeshProUGUI> levelUpUnitStatsP2;
    public TextMeshProUGUI levelTextP2;
    public TextMeshProUGUI LevelUpNameTextP2;
    public Slider expSliderP2;

    public Animator unitLUPAnimatorP3;
    public List<TextMeshProUGUI> levelUpUnitStatsP3;
    public TextMeshProUGUI levelTextP3;
    public TextMeshProUGUI LevelUpNameTextP3;
    public Slider expSliderP3;
    [Header("HoverStats")]
    public GameObject hoverObject;
    public TextMeshProUGUI hoverName;
    public List<TextMeshProUGUI> weaponStats;
    public List<TextMeshProUGUI> unitStats;
    public List<TextMeshProUGUI> hoverSkillnames;
    public TextMeshProUGUI soulName;
    public SpriteRenderer WeaponImage;
    public SpriteRenderer AccessoryImage;
    public SpriteRenderer Mugshot;
    public bool hovering;
    public Vector3 startingPosition;
    public GameObject postMatch;
    public List<TextMeshProUGUI> postMatchTexts;
    public enum BattleState { BattleStart, Wait, PlayerTurn, EnemyTurn, PlayerWon, EnemyWon }
    public BattleState state;

    public class CombatLog
    {
        public string unitName;
        public int damage;
        public int damageTaken;
        public int damageHeal;
    }
    public CombatLog cl1 = new();
    public CombatLog cl2 = new();
    public CombatLog cl3 = new();

    public bool waiting = false;
    private bool targetingRerun;
    public void Awake()
    {
        PlayerBars.Clear();
        PlayerBars.Add(PlayerBar);
        PlayerBars.Add(PlayerBar2);
        PlayerBars.Add(PlayerBar3);
        EnemyBars.Clear();
        EnemyBars.Add(EnemyBar);
        EnemyBars.Add(EnemyBar2);
        EnemyBars.Add(EnemyBar3);
    }
    void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        playerUnit = gameManager.teamPostPreBattle[0];
        playerUnit2 = gameManager.teamPostPreBattle[1];
        playerUnit3 = gameManager.teamPostPreBattle[2];
        playerTeam.Add(playerUnit.GetComponent<UnitBehavior>());
        playerTeam.Add(playerUnit2.GetComponent<UnitBehavior>());
        playerTeam.Add(playerUnit3.GetComponent<UnitBehavior>());
        enemyUnit = gameManager.enemyTeamPostPreBattle[0];
        enemyUnit2 = gameManager.enemyTeamPostPreBattle[1];
        enemyUnit3 = gameManager.enemyTeamPostPreBattle[2];
        enemyTeam.Add(enemyUnit.GetComponent<UnitBehavior>());
        enemyTeam.Add(enemyUnit2.GetComponent<UnitBehavior>());
        enemyTeam.Add(enemyUnit3.GetComponent<UnitBehavior>());
        state = BattleState.BattleStart;
        gameManager.songManager.ChecksScene();
        StartCoroutine(SetupBattle());
        ////PLACEHOLDER TODO
        if (playerTeam[1].UnitName == "")
        {
            playerHpSlider[1].gameObject.SetActive(false);
            PlayerActionBar[1].gameObject.SetActive(false);
            PlayerSoulBar[1].gameObject.SetActive(false);
            enemyHpSlider[1].gameObject.SetActive(false);
            EnemyActionBar[1].gameObject.SetActive(false);
            EnemySoulBar[1].gameObject.SetActive(false);
        }
        if (playerTeam[2].UnitName == "")
        {
            playerHpSlider[2].gameObject.SetActive(false);
            PlayerActionBar[2].gameObject.SetActive(false);
            PlayerSoulBar[2].gameObject.SetActive(false);
            enemyHpSlider[2].gameObject.SetActive(false);
            EnemyActionBar[2].gameObject.SetActive(false);
            EnemySoulBar[2].gameObject.SetActive(false);
        }
        startingPosition = hoverObject.transform.localPosition;
        hoverObject.transform.localPosition = new Vector3(400, 0, 0);
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>(FindObjectsInactive.Include);

        speedList.transform.Find("player1ToTurn1").GetComponent<Image>().sprite = inventoryManager.playableMugShots.Where(obj => obj.name == playerTeam[0].UnitName + " mugshot").SingleOrDefault();
        speedList.transform.Find("player2ToTurn1").GetComponent<Image>().sprite = inventoryManager.playableMugShots.Where(obj => obj.name == playerTeam[1].UnitName + " mugshot").SingleOrDefault();
        speedList.transform.Find("player3ToTurn1").GetComponent<Image>().sprite = inventoryManager.playableMugShots.Where(obj => obj.name == playerTeam[2].UnitName + " mugshot").SingleOrDefault();
        speedList.transform.Find("enemy1ToTurn1").GetComponent<Image>().sprite = ClassIconPicker(enemyTeam[0].classId);
        speedList.transform.Find("enemy2ToTurn1").GetComponent<Image>().sprite = ClassIconPicker(enemyTeam[1].classId);
        speedList.transform.Find("enemy3ToTurn1").GetComponent<Image>().sprite = ClassIconPicker(enemyTeam[2].classId);
        for (int i = 2; i < 26; i++)
        {
            GameObject newPlayer1 = Instantiate(speedList.transform.Find("player1ToTurn1").gameObject, speedList.transform);
            newPlayer1.name = $"player1ToTurn{i}";
            GameObject newPlayer2 = Instantiate(speedList.transform.Find("player2ToTurn1").gameObject, speedList.transform);
            newPlayer2.name = $"player2ToTurn{i}";
            GameObject newPlayer3 = Instantiate(speedList.transform.Find("player3ToTurn1").gameObject, speedList.transform);
            newPlayer3.name = $"player3ToTurn{i}";
            GameObject newEnemy1 = Instantiate(speedList.transform.Find("enemy1ToTurn1").gameObject, speedList.transform);
            newEnemy1.name = $"enemy1ToTurn{i}";
            GameObject newEnemy2 = Instantiate(speedList.transform.Find("enemy2ToTurn1").gameObject, speedList.transform);
            newEnemy2.name = $"enemy2ToTurn{i}";
            GameObject newEnemy3 = Instantiate(speedList.transform.Find("enemy3ToTurn1").gameObject, speedList.transform);
            newEnemy3.name = $"enemy3ToTurn{i}";
        }
        //tempo em frames para o proximo turno ate 25 turnos
        Dictionary<string, float> repeatTurns = new();
        for (int i = 1; i < 26; i++)
        {
            repeatTurns.Add($"player1ToTurn{i}", ((100 * i) - PlayerBar) /  (playerTeam[0].speed * CheckWeight(playerTeam[0])));
            repeatTurns.Add($"player2ToTurn{i}", ((100 * i) - PlayerBar2) / (playerTeam[1].speed * CheckWeight(playerTeam[1])));
            repeatTurns.Add($"player3ToTurn{i}", ((100 * i) - PlayerBar3) / (playerTeam[2].speed * CheckWeight(playerTeam[2])));
            repeatTurns.Add($"enemy1ToTurn{i}", ((100 * i) - EnemyBar) /    (enemyTeam[0].speed * CheckWeight(enemyTeam[0])));
            repeatTurns.Add($"enemy2ToTurn{i}", ((100 * i) - EnemyBar2) /   (enemyTeam[1].speed * CheckWeight(enemyTeam[1])));
            repeatTurns.Add($"enemy3ToTurn{i}", ((100 * i) - EnemyBar3) /   (enemyTeam[2].speed * CheckWeight(enemyTeam[2])));
        }
        //organiza os turnos em mais proximo a acontecer
        var sortedTurnsrepeatTurns = from entry in repeatTurns orderby entry.Value ascending select entry;
        int loop = 0;
        foreach (KeyValuePair<string, float> i in sortedTurnsrepeatTurns)
        {
            foreach (KeyValuePair<string, float> i2 in repeatTurns)
            {
                if (speedList.transform.Find(i.Key) != null && i.Key == i2.Key) { speedList.transform.Find(i.Key).SetSiblingIndex(loop); }
            }
            loop++;
        }

            cl1.unitName = playerTeam[0].UnitName;
            Debug.Log(cl1.unitName);
        if (playerTeam[1].UnitName != "")
        {
            cl2.unitName = playerTeam[1].UnitName;
            Debug.Log(cl2.unitName);
        }
        if (playerTeam[2].UnitName != "")
        {
            cl3.unitName = playerTeam[2].UnitName;
            Debug.Log(cl3.unitName);
        }
    }
    private void Update()
    {
        if (state == BattleState.Wait && waiting == false)
        {
            StartCoroutine(Wait());
        }

        if (hovering)
        {
            hoverObject.transform.localPosition = startingPosition;
        }
        else
        {
            hoverObject.transform.localPosition = new Vector3(400, 0, 0);
        }
    }
    IEnumerator SetupBattle()
    {
        playerGo = Instantiate(playerUnit, playerBattleStation);
        playerGo.name = playerGo.GetComponent<UnitBehavior>().UnitName + "Battle";
        playerGo2 = Instantiate(playerUnit2, playerBattleStation2);
        playerGo2.name = playerGo2.GetComponent<UnitBehavior>().UnitName + "Battle";
        playerGo3 = Instantiate(playerUnit3, playerBattleStation3);
        playerGo3.name = playerGo3.GetComponent<UnitBehavior>().UnitName + "Battle";

        GameObject enemyGo = Instantiate(enemyUnit, enemyBattleStation);
        enemyGo.name = enemyGo.GetComponent<UnitBehavior>().UnitName + "Battle";
        GameObject enemyGo2 = Instantiate(enemyUnit2, enemyBattleStation2);
        enemyGo2.name = enemyGo2.GetComponent<UnitBehavior>().UnitName + "Battle";
        GameObject enemyGo3 = Instantiate(enemyUnit3, enemyBattleStation3);
        enemyGo3.name = enemyGo3.GetComponent<UnitBehavior>().UnitName + "Battle";
        playerBehavior = playerGo.GetComponent<UnitBehavior>();
        player2Behavior = playerGo2.GetComponent<UnitBehavior>();
        player3Behavior = playerGo3.GetComponent<UnitBehavior>();
        enemyBehavior = enemyGo.GetComponent<UnitBehavior>();
        enemy2Behavior = enemyGo2.GetComponent<UnitBehavior>();
        enemy3Behavior = enemyGo3.GetComponent<UnitBehavior>();
        enemyBehavior.enemy = true;
        enemy2Behavior.enemy = true;
        enemy3Behavior.enemy = true;
        playerTeam[0].position = 1;
        playerTeam[1].position = 2;
        playerTeam[2].position = 3;
        playerTeam[0].animator = animators[0];
        playerTeam[1].animator = animators[1];
        playerTeam[2].animator = animators[2];
        enemyTeam[0].animator = animators[3];
        enemyTeam[1].animator = animators[4];
        enemyTeam[2].animator = animators[5];

        Random.InitState(enemyTeam[0].UnitName.GetHashCode());
        animators[3].GetComponent<SpriteRenderer>().material = matRaces[Random.Range(0, 3)];

        Random.InitState(enemyTeam[1].UnitName.GetHashCode());
        animators[4].GetComponent<SpriteRenderer>().material = matRaces[Random.Range(0, 3)];

        Random.InitState(enemyTeam[2].UnitName.GetHashCode());
        animators[5].GetComponent<SpriteRenderer>().material = matRaces[Random.Range(0, 3)];

        enemyTeam[0].position = 1;
        enemyTeam[1].position = 2;
        enemyTeam[2].position = 3;
        playerTeam[0].Icon = IconSlots[0];
        playerTeam[1].Icon = IconSlots[1];
        playerTeam[2].Icon = IconSlots[2];
        enemyTeam[0].Icon = IconSlots[3];
        enemyTeam[1].Icon = IconSlots[4];
        enemyTeam[2].Icon = IconSlots[5];
        playerTeam[0].damageTMP = DamagePopups[0];
        playerTeam[1].damageTMP = DamagePopups[1];
        playerTeam[2].damageTMP = DamagePopups[2];
        enemyTeam[0].damageTMP = DamagePopups[3];
        enemyTeam[1].damageTMP = DamagePopups[4];
        enemyTeam[2].damageTMP = DamagePopups[5];
        playerTeam[0].startingPosition = playerTeam[0].animator.transform.position;
        playerTeam[1].startingPosition = playerTeam[1].animator.transform.position;
        playerTeam[2].startingPosition = playerTeam[2].animator.transform.position;
        enemyTeam[0].startingPosition = enemyTeam[0].animator.transform.position;
        enemyTeam[1].startingPosition = enemyTeam[1].animator.transform.position;
        enemyTeam[2].startingPosition = enemyTeam[2].animator.transform.position;
        foreach (UnitBehavior ub in playerTeam)
        {
            ub.battleManager = this;
        }
        foreach (UnitBehavior ub in enemyTeam)
        {
            ub.battleManager = this;
        }
        yield return new WaitForSeconds(0.5f);
        SetHud();
        SetHp();
        foreach (UnitBehavior ub in playerTeam)
        {
            List<UnitBehavior> attackerTeam;
            List<UnitBehavior> targetTeam;
            skillsInUse.Clear();
            skillsInUse.AddRange(ub.skills);
            if (ub.enemy)
            {
                attackerTeam = enemyTeam;
                targetTeam = playerTeam;
            }
            else
            {
                attackerTeam = playerTeam;
                targetTeam = enemyTeam;
            }
            if (ub.classSkill != null)
            {
                skillsInUse.Add(ub.classSkill);
            }
            if (ub.personalSkill != null)
            {
                skillsInUse.Add(ub.personalSkill);
            }
            if (ub.Weapon != null && ub.Weapon.skill != null)
            {
                skillsInUse.Add(ub.Weapon.skill);
            }
            if (ub.Accesory != null && ub.Accesory.skill != null)
            {
                skillsInUse.Add(ub.Accesory.skill);
            }
            foreach (string skill in skillsInUse)
            {
                ub.SkillManager = ub.GetComponentInParent<SkillManager>();
                ub.SkillManager.MatchStartProc(skill, ub, targetTeam[StandardTargeting(ub, enemyTeam)], attackerTeam, targetTeam);
            }
        }
        foreach (UnitBehavior ub in enemyTeam)
        {
            List<UnitBehavior> attackerTeam;
            List<UnitBehavior> targetTeam;
            skillsInUse.Clear();
            skillsInUse.AddRange(ub.skills);
            if (ub.enemy)
            {
                attackerTeam = enemyTeam;
                targetTeam = playerTeam;
            }
            else
            {
                attackerTeam = playerTeam;
                targetTeam = enemyTeam;
            }
            if (ub.classSkill != null)
            {
                skillsInUse.Add(ub.classSkill);
            }
            if (ub.personalSkill != null)
            {
                skillsInUse.Add(ub.personalSkill);
            }
            if (ub.Weapon != null && ub.Weapon.skill != null)
            {
                skillsInUse.Add(ub.Weapon.skill);
            }
            if (ub.Accesory != null && ub.Accesory.skill != null)
            {
                skillsInUse.Add(ub.Accesory.skill);
            }
            foreach (string skill in skillsInUse)
            {
                ub.SkillManager = ub.GetComponentInParent<SkillManager>();
                ub.SkillManager.MatchStartProc(skill, ub, targetTeam[StandardTargeting(ub, enemyTeam)], attackerTeam, targetTeam);
            }
        }

        if (playerTeam[0].Weapon.damageType == 0)
        {

            unitStats[8].text = (playerTeam[0].str + playerTeam[0].Weapon.power).ToString();
        }
        else
        {
            unitStats[8].text = (playerTeam[0].mag + playerTeam[0].Weapon.power).ToString();
        }
        unitStats[9].text = (playerTeam[0].Weapon.hit + (playerTeam[0].dex * 3) + playerTeam[0].luck + playerTeam[0].hit).ToString();
        unitStats[10].text = ((playerTeam[0].speed * 2) - playerTeam[0].luck - playerTeam[0].avoid).ToString();
        unitStats[11].text = (playerTeam[0].Weapon.crit + playerTeam[0].dex).ToString();
        playerTeam[0].hp = playerTeam[0].maxhp;
        if(gameManager.team.Count > 1)
        { 
        playerTeam[1].hp = playerTeam[1].maxhp;
        }
        if (gameManager.team.Count > 2)
        {
            playerTeam[2].hp = playerTeam[2].maxhp;
        }
        state = BattleState.Wait;
    }
    void SetHud()
    {
        int c = 0;
        foreach (Slider sl in playerHpSlider)
        {
            sl.maxValue = playerTeam[c].maxhp;
        }
        c = 0;
        foreach (Slider sl in enemyHpSlider)
        {
            sl.maxValue = enemyTeam[c].maxhp;
            c++;
        }
        c = 0;
        foreach (Slider sl in PlayerSoulBar)
        {
            sl.maxValue = playerTeam[c].maxsoul;
            c++;
        }
        c = 0;
        foreach (Slider sl in EnemySoulBar)
        {
            sl.maxValue = enemyTeam[c].maxsoul;
            c++;
        }

    }
    //manuzeia as barras de hp, alma e turno
    public void HudUpdate()
    {
        int c = 0;
        foreach (Slider sl in playerHpSlider)
        {
            sl.maxValue = playerTeam[c].maxhp;
            c++;
        }
        c = 0;
        foreach (Slider sl in enemyHpSlider)
        {
            sl.maxValue = enemyTeam[c].maxhp;
            c++;
        }
        c = 0;
        foreach (Slider sl in PlayerSoulBar)
        {
            sl.value = playerTeam[c].soul;
            c++;

        }
        c = 0;
        foreach (Slider sl in EnemySoulBar)
        {
            sl.value = enemyTeam[c].soul; c++;

        }
        c = 0;
        foreach (Slider sl in PlayerActionBar)
        {
            sl.value = PlayerBars[c];
            c++;

        }
        c = 0;
        foreach (Slider sl in EnemyActionBar)
        {
            sl.value = EnemyBars[c]; c++;
        }
    }
    void SetHp()
    {
        int c = 0;
        foreach (Slider sl in playerHpSlider)
        {
            sl.value = playerTeam[c].hp;
        }
        c = 0;
        foreach (Slider sl in enemyHpSlider)
        {
            sl.value = enemyTeam[c].hp;
        }
    }
    IEnumerator Wait()
    {
        waiting = true;
        if (playerTeam[0].hp > 0 && state == BattleState.Wait) { PlayerBar += Time.fixedUnscaledDeltaTime * (playerTeam[0].speed * CheckWeight(playerTeam[0])) * battleSpeed; PlayerBars[0] = PlayerBar;
        }
        else if (playerTeam[0].UnitName != "") { playerTeam[0].animator.SetTrigger("UnitDie"); }
        if (playerTeam[1].hp > 0 && state == BattleState.Wait) { PlayerBar2 += Time.fixedUnscaledDeltaTime * (playerTeam[1].speed * CheckWeight(playerTeam[0])) * battleSpeed; PlayerBars[1] = PlayerBar2;
        }
        else if (playerTeam[1].UnitName != "") { playerTeam[1].animator.SetTrigger("UnitDie"); }
        if (playerTeam[2].hp > 0 && state == BattleState.Wait) { PlayerBar3 += Time.fixedUnscaledDeltaTime * (playerTeam[2].speed * CheckWeight(playerTeam[0])) * battleSpeed; PlayerBars[2] = PlayerBar3;
        }
        else if (playerTeam[2].UnitName != "") { playerTeam[2].animator.SetTrigger("UnitDie"); }
        if (enemyTeam[0].hp > 0 && state == BattleState.Wait) { EnemyBar += Time.fixedUnscaledDeltaTime * (enemyTeam[0].speed * CheckWeight(enemyTeam[0])) * battleSpeed; EnemyBars[0] = EnemyBar;
        }
        else if (enemyTeam[0].UnitName != "") { enemyTeam[0].animator.SetTrigger("UnitDie"); }
        if (enemyTeam[1].hp > 0 && state == BattleState.Wait) { EnemyBar2 += Time.fixedUnscaledDeltaTime * (enemyTeam[1].speed * CheckWeight(enemyTeam[1])) * battleSpeed; EnemyBars[1] = EnemyBar2;
        }
        else if (enemyTeam[1].UnitName != "") { enemyTeam[1].animator.SetTrigger("UnitDie"); }
        if (enemyTeam[2].hp > 0 && state == BattleState.Wait) { EnemyBar3 += Time.fixedUnscaledDeltaTime * (enemyTeam[2].speed * CheckWeight(enemyTeam[2])) * battleSpeed; EnemyBars[2] = EnemyBar3;
        }
        else if (enemyTeam[2].UnitName != "") { enemyTeam[2].animator.SetTrigger("UnitDie"); }
        int c = 0;
        foreach (Slider sl in PlayerActionBar)
        {
            sl.value = PlayerBars[c];
            c++;
        }
        c = 0;
        foreach (Slider sl in EnemyActionBar)
        {
            sl.value = EnemyBars[c];
            c++;
        }
        if (PlayerBar >= 100 & state == BattleState.Wait)
        {
            state = BattleState.PlayerTurn;

            StartCoroutine(Attack(playerTeam[0], enemyTeam[PickTargeting(playerTeam[0],enemyTeam)]));

            PlayerBar = 0;
            PlayerBars[0] = 0;
        }
        else if (PlayerBar2 >= 100 & state == BattleState.Wait)
        {
            state = BattleState.PlayerTurn;

            StartCoroutine(Attack(playerTeam[1], enemyTeam[PickTargeting(playerTeam[1], enemyTeam)]));
            PlayerBar2 = 0;
            PlayerBars[1] = 0;
        }
        else if (PlayerBar3 >= 100 & state == BattleState.Wait)
        {
            state = BattleState.PlayerTurn;

            StartCoroutine(Attack(playerTeam[2], enemyTeam[PickTargeting(playerTeam[2], enemyTeam)]));
            PlayerBar3 = 0;
            PlayerBars[2] = 0;
        }
        else if (EnemyBar >= 100 & state == BattleState.Wait)
        {
            state = BattleState.EnemyTurn;

            StartCoroutine(Attack(enemyTeam[0], playerTeam[StandardTargeting(enemyTeam[0],playerTeam)]));
            EnemyBar = 0;
            EnemyBars[0] = 0;
        }
        else if (EnemyBar2 >= 100 & state == BattleState.Wait)
        {
            state = BattleState.EnemyTurn;

            StartCoroutine(Attack(enemyTeam[1], playerTeam[StandardTargeting(enemyTeam[1],playerTeam)]));
            EnemyBar2 = 0;
            EnemyBars[1] = 0;
        }
        else if (EnemyBar3 >= 100 & state == BattleState.Wait)
        {
            state = BattleState.EnemyTurn;

            StartCoroutine(Attack(enemyTeam[2], playerTeam[StandardTargeting(enemyTeam[2],playerTeam)]));
            EnemyBar3 = 0;
            EnemyBars[2] = 0;
        }
        else
        {
            yield return new WaitForSeconds((float)0.01);
            waiting = false;
        }
    }

    public int PickTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        int x = 9999;        
            switch (ub.target)
            {
                case "LowestStat": x = LowestStatTargeting(ub, unitList, ub.targetStat);break ;
                case "HighestStat": x = HighestStatTargeting(ub, unitList, ub.targetStat); break;
                case "LeastHp": x = LeastHpTargeting(ub, unitList); break;
                case "ClassID": x = ClassIDTarget(ub, unitList, ub.targetStat); break;
                case "Weapon": x = WeaponIDTarget(ub, unitList, ub.targetStat); break;
                case "mais longe": x = ReverseTargeting(ub, unitList); break;
                case "primeira posição": x = FirstTargeting(ub, unitList); break;
                case "segunda posição": x = SecondTargeting(ub, unitList); break;
                case "terceira posição": x = TerceiroTargeting(ub, unitList); break;
            default:break;
            }
        if (x != 9999)
        {
            return x;
        }
        else
        {
            x = PickTargeting2(ub, unitList);
            if (x == 9999)
            {
                return StandardTargeting(ub, unitList);
            }
            else
            {
                return x;
            }
        }
    }
    public int PickTargeting2(UnitBehavior ub, List<UnitBehavior> unitList)
    {

        switch (ub.target2)
        {
            case "LowestStat": return LowestStatTargeting(ub, unitList, ub.targetStat2);
            case "HighestStat": return HighestStatTargeting(ub, unitList, ub.targetStat2);
            case "LeastHp": return LeastHpTargeting(ub, unitList);
            case "ClassID": return ClassIDTarget(ub, unitList, ub.targetStat2);
            case "Weapon": return WeaponIDTarget(ub, unitList, ub.targetStat2);
            case "mais longe": return ReverseTargeting(ub, unitList);
            case "primeira posição": return FirstTargeting(ub, unitList);
            case "segunda posição": return SecondTargeting(ub, unitList);
            case "terceira posição": return TerceiroTargeting(ub, unitList);
            default: return StandardTargeting(ub, unitList);
        }
    }
    public int StandardTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        if (unitList[0].hp >= 1)
        {
            return 0;
        }
        else if (unitList[1].hp >= 1)
        {
            return 1;
        }
        else if (unitList[2].hp >= 1)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
    public int ReverseTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        if (unitList[2].hp >= 1)
        {
            return 2;
        }
        else if (unitList[1].hp >= 1)
        {
            return 1;
        }
        else if (unitList[0].hp >= 1)
        {
            return 0;
        }
        else
        {
            return 9999;
        }
    }
    public int FirstTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        if (unitList[0].hp >= 1)
        {
            return 0;
        }
        else
        {
            return 9999;
        }
    }
    public int SecondTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        if (unitList[1].hp >= 1)
        {
            return 1;
        }
        else
        {
            return 9999;
        }
    }
    public int TerceiroTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        if (unitList[2].hp >= 1)
        {
            return 2;
        }
        else
        {
            return 9999;
        }
    }
    public int LowestStatTargeting(UnitBehavior ub, List<UnitBehavior> unitList, int stat)
    {
        List<int> unitList0stat = new() { unitList[0].hp, unitList[0].str, unitList[0].mag, unitList[0].dex, (int)unitList[0].speed, unitList[0].def, unitList[0].mdef, unitList[0].luck };
        List<int> unitList1stat = new() { unitList[1].hp, unitList[1].str, unitList[1].mag, unitList[1].dex, (int)unitList[1].speed, unitList[1].def, unitList[1].mdef, unitList[1].luck };
        List<int> unitList2stat = new() { unitList[2].hp, unitList[2].str, unitList[2].mag, unitList[2].dex, (int)unitList[2].speed, unitList[2].def, unitList[2].mdef, unitList[2].luck };

        if (unitList0stat[stat] > unitList1stat[stat] && unitList0stat[stat] > unitList2stat[stat])
        {
        return 0;
        }
        if (unitList1stat[stat] > unitList0stat[stat] && unitList1stat[stat] > unitList2stat[stat])
        {
            return 1;
        }
        if (unitList2stat[stat] > unitList0stat[stat] && unitList2stat[stat] > unitList1stat[stat])
        {
            return 2;
        }
        else
        {
            return 9999;
        }
    }
    public int HighestStatTargeting(UnitBehavior ub, List<UnitBehavior> unitList, int stat)
    {
        List<int> unitList0stat = new() { unitList[0].hp, unitList[0].str, unitList[0].mag, unitList[0].dex, (int)unitList[0].speed, unitList[0].def, unitList[0].mdef, unitList[0].luck };
        List<int> unitList1stat = new() { unitList[1].hp, unitList[1].str, unitList[1].mag, unitList[1].dex, (int)unitList[1].speed, unitList[1].def, unitList[1].mdef, unitList[1].luck };
        List<int> unitList2stat = new() { unitList[2].hp, unitList[2].str, unitList[2].mag, unitList[2].dex, (int)unitList[2].speed, unitList[2].def, unitList[2].mdef, unitList[2].luck };

        if (unitList0stat[stat] < unitList1stat[stat] && unitList0stat[stat] < unitList2stat[stat])
        {
            return 0;
        }
        if (unitList1stat[stat] < unitList0stat[stat] && unitList1stat[stat] < unitList2stat[stat])
        {
            return 1;
        }
        if (unitList2stat[stat] < unitList0stat[stat] && unitList2stat[stat] < unitList1stat[stat])
        {
            return 2;
        }
        else
        {
            return 9999;
        }
    }
    public int LeastHpTargeting(UnitBehavior ub, List<UnitBehavior> unitList)
    {
        Debug.Log("leastHpTargeting" + unitList);
        if (unitList[0].hp >= 1 && unitList[0].hp < unitList[1].hp &&  unitList[0].hp < unitList[2].hp)
        {
            return 0;
        }
        else if (unitList[1].hp >= 1 && unitList[1].hp < unitList[0].hp && unitList[1].hp < unitList[2].hp)
        {
            return 1;
        }
        else if (unitList[2].hp >= 1 && unitList[2].hp < unitList[1].hp && unitList[2].hp < unitList[0].hp)
        {
            return 2;
        }
        else
        {
            return 9999;
        }
    }
    public int ClassIDTarget(UnitBehavior ub, List<UnitBehavior> unitList, int stat)
    {
        int c = 0;
        foreach(UnitBehavior ul in unitList)
        {
            if(ul.classId == stat)
            {
                return c;
            }
            c++;
        }
        return 9999;
    }
    public int WeaponIDTarget(UnitBehavior ub, List<UnitBehavior> unitList, int stat)
    {
        int c = 0;
        Item.Weapontype weaponType = new();
        switch (stat)
        {
            case 1:weaponType = Item.Weapontype.Sword; break;
            case 2:weaponType = Item.Weapontype.Axe; break;
            case 3:weaponType = Item.Weapontype.Lance; break;
            case 4:weaponType = Item.Weapontype.Tome; break;
            case 5:weaponType = Item.Weapontype.Receptacle; break;
            case 6:weaponType = Item.Weapontype.Bow; break;
            default:weaponType = Item.Weapontype.Sword; break;
        }

        foreach (UnitBehavior ul in unitList)
        {
            if(ul.Weapon.weapontype == weaponType)
            {
                return c;
            }
            c++;
        }
        return 9999;
    }
    public virtual IEnumerator Attack(UnitBehavior attacker, UnitBehavior Target)
    {
        AttackSetup(attacker, Target);
        if (attacker.hp > attacker.maxhp) { attacker.hp = attacker.maxhp; }
        if (Target.hp > Target.maxhp) { Target.hp = Target.maxhp; }
        attacker.animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        if (attacker.Weapon.weapontype != Item.Weapontype.Bow && attacker.Weapon.weapontype != Item.Weapontype.Receptacle && attacker.Weapon.weapontype != Item.Weapontype.Tome)
        {
            Debug.Log(attacker.name + " dashed to " + Target.name);
            StartCoroutine(DashToTarget(attacker, Target.animator.transform.position));
        }
        attacker.animator.SetTrigger("UnitAdvance");
        List<UnitBehavior> attackerTeam;
        List<UnitBehavior> targetTeam;
        if (attacker.enemy)
        {
            state = BattleState.EnemyTurn;
        }
        else
        {
            state = BattleState.PlayerTurn; }
        if (attacker.Weapon.damageType == 0)
        {
            attacker.power = attacker.str + attacker.Weapon.power;
        }
        else
        {
            attacker.power = attacker.mag + attacker.Weapon.power;
        }
        //Pskill = 0;
        attacker.SkillManager.currentDamageBonus = 0;
        int attackerDamage = attacker.power - (Target.defenses[attacker.Weapon.damageType] + Target.damagereduction);
        Debug.Log(attackerDamage);
        if (attackerDamage <= 0) { attackerDamage = 1; }
        skillsInUse.Clear();
        skillsInUse.AddRange(attacker.skills);
        if (attacker.enemy)
        {
            attackerTeam = enemyTeam;
            targetTeam = playerTeam;
        }
        else
        {
            attackerTeam = playerTeam;
            targetTeam = enemyTeam;
        }
        if (attacker.classSkill != null)
        {
            skillsInUse.Add(attacker.classSkill);
        }
        if (attacker.personalSkill != null)
        {
            skillsInUse.Add(attacker.personalSkill);
        }
        if (attacker.Weapon != null && attacker.Weapon.skill != null)
        {
            skillsInUse.Add(attacker.Weapon.skill);
        }
        if (attacker.Accesory != null && attacker.Accesory.skill != null)
        {
            skillsInUse.Add(attacker.Accesory.skill);
        }
        attacker.soul += 15 + attacker.soulgain;
        if (attacker.soul <= attacker.maxsoul && Random.Range(0, 101) <= Phit)
        {
            StartCoroutine(AttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam));
        }
        else if (attacker.soul >= attacker.maxsoul && attacker.equippedSoulIsAttack)
        {
            if (Random.Range(0, 101) <= Phit)
            {
                StartCoroutine(AttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam));
            }
        }
        else if (attacker.soul >= attacker.maxsoul && !attacker.equippedSoulIsAttack)
        {
            attacker.soul -= attacker.maxsoul;
            StartCoroutine(attacker.SkillManager.NaSoulproc(attacker.equipedSoul, attacker, Target, attackerTeam, targetTeam));
        }
        else
        {
            Target.animator.SetTrigger("UnitDodge");
        }
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(DashToTarget(attacker, attacker.startingPosition, 0.01f));
        //tempo em frames para o proximo turno ate 25 turnos
        Dictionary<string, float> repeatTurns = new();
        for (int i = 1; i < 26; i++)
        {
            repeatTurns.Add($"player1ToTurn{i}", ((100 * i) - PlayerBar) / playerTeam[0].speed);
            repeatTurns.Add($"player2ToTurn{i}", ((100 * i) - PlayerBar2) / playerTeam[1].speed);
            repeatTurns.Add($"player3ToTurn{i}", ((100 * i) - PlayerBar3) / playerTeam[2].speed);
            repeatTurns.Add($"enemy1ToTurn{i}", ((100 * i) - EnemyBar) / enemyTeam[0].speed);
            repeatTurns.Add($"enemy2ToTurn{i}", ((100 * i) - EnemyBar2) / enemyTeam[1].speed);
            repeatTurns.Add($"enemy3ToTurn{i}", ((100 * i) - EnemyBar3) / enemyTeam[2].speed);
        }
        //organiza os turnos em mais proximo a acontecer
        var sortedTurnsrepeatTurns = from entry in repeatTurns orderby entry.Value ascending select entry;
        int loop = 0;
        foreach (KeyValuePair<string, float> i in sortedTurnsrepeatTurns)
        {
            foreach (KeyValuePair<string, float> i2 in repeatTurns)
            {
                if (speedList.transform.Find(i.Key) != null && i.Key == i2.Key) { speedList.transform.Find(i.Key).SetSiblingIndex(loop); }
            }
            loop++;
        }
        //inimigo morre
        if (Target.hp <= 0)
        {
            Target.animator.SetTrigger("UnitDie");
        }
        if (enemyTeam[0].hp <= 0 && enemyTeam[1].hp <= 0 && enemyTeam[2].hp <= 0)
        {
            StartCoroutine(PlayerWin());
        }
        else if (playerTeam[0].hp <= 0 && playerTeam[1].hp <= 0 && playerTeam[2].hp <= 0)
        {
            gameManager.storyBattle = false;
            Destroy(GameObject.Find("Hover Object"));
            Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("Main Canvas"));
            Destroy(GameObject.Find("[Debug Updater]"));
            gameManager.SceneLoader("MainMenu");
        }
        else
        {
            state = BattleState.Wait;
            waiting = false;
        }
    }
    public virtual IEnumerator ExtraAttack(UnitBehavior attacker, UnitBehavior Target, float DamageMultiplier = 1, float lifeSteal = 0)
    {
        List<UnitBehavior> attackerTeam;
        List<UnitBehavior> targetTeam;
        if (attacker.enemy)
        {
            state = BattleState.EnemyTurn;
        }
        else { state = BattleState.PlayerTurn; }
        attacker.power = attacker.str + attacker.Weapon.power;
        //Pskill = 0;
        attacker.SkillManager.currentDamageBonus = 0;
        int attackerDamage = attacker.power - Target.defenses[attacker.Weapon.damageType];
        if (attackerDamage <= 0) { attackerDamage = 1; }
        EAskillsInUse.Clear();
        EAskillsInUse.AddRange(attacker.skills);
        if (attacker.enemy)
        {
            attackerTeam = enemyTeam;
            targetTeam = playerTeam;
        }
        else
        {
            attackerTeam = playerTeam;
            targetTeam = enemyTeam;
        }
        if (attacker.classSkill != null)
        {
            EAskillsInUse.Add(attacker.classSkill);
        }
        if (attacker.personalSkill != null)
        {
            EAskillsInUse.Add(attacker.personalSkill);
        }
        if (attacker.Weapon != null && attacker.Weapon.skill != null)
        {
            EAskillsInUse.Add(attacker.Weapon.skill);
        }
        if (attacker.Accesory != null && attacker.Accesory.skill != null)
        {
            EAskillsInUse.Add(attacker.Accesory.skill);
        }
        if (Random.Range(0, 101) <= Phit)
        {
            StartCoroutine(ExtraAttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam, DamageMultiplier, lifeSteal));
        }
        else
        {
            Debug.Log("extra attack errou :(");
        }
        if (Target.hp <= 0)
        {
            Target.animator.SetTrigger("UnitDie");
        }
        yield return new WaitForSeconds(1f);
        //inimigo morre
        if (enemyTeam[0].hp <= 0 && enemyTeam[1].hp <= 0 && enemyTeam[2].hp <= 0)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            state = BattleState.Wait;
        }
    }
    public IEnumerator PlayerWin()
    {
        Debug.Log($"name:{cl1.unitName}, damage done:{cl1.damage}, damage taken:{cl1.damageTaken}");
        Debug.Log($"name:{cl2.unitName}, damage done:{cl2.damage}, damage taken:{cl2.damageTaken}");
        Debug.Log($"name:{cl3.unitName}, damage done:{cl3.damage}, damage taken:{cl3.damageTaken}");

        UnitBehavior RealCharacter1 = gameManager.team[0].GetComponent<UnitBehavior>();
        UnitBehavior DisplayCharacter1 = gameManager.teamPostPreBattle[0].GetComponent<UnitBehavior>();
        UnitBehavior RealCharacter2 = null;
        UnitBehavior RealCharacter3 = null;
        UnitBehavior RealCharacter4 = null;
        UnitBehavior DisplayCharacter2 = null;
        UnitBehavior DisplayCharacter3 = null;
        UnitBehavior DisplayCharacter1Real = null;
        UnitBehavior DisplayCharacter2Real = null;
        UnitBehavior DisplayCharacter3Real = null;
        
        foreach (GameObject go in gameManager.team)
            {
                if (DisplayCharacter1.UnitName == go.GetComponent<UnitBehavior>().UnitName)
                {
                    DisplayCharacter1Real = go.GetComponent<UnitBehavior>();
                }
            }
       

        if (gameManager.team.Count > 1)
        {
            RealCharacter2 = gameManager.team[1].GetComponent<UnitBehavior>();
            DisplayCharacter2 = gameManager.teamPostPreBattle[1].GetComponent<UnitBehavior>();
            Debug.Log(RealCharacter2.name + RealCharacter2.currentExp);
            foreach (GameObject go in gameManager.team)
            {
                if (DisplayCharacter2.UnitName == go.GetComponent<UnitBehavior>().UnitName)
                {
                    DisplayCharacter2Real = go.GetComponent<UnitBehavior>();
                }
            }
            StatsBreakdown(DisplayCharacter2.position, DisplayCharacter2Real);
        }
        
        if (gameManager.team.Count > 2)
        {
            RealCharacter3 = gameManager.team[2].GetComponent<UnitBehavior>();
            DisplayCharacter3 = gameManager.teamPostPreBattle[2].GetComponent<UnitBehavior>();
            Debug.Log(RealCharacter3.name + RealCharacter3.currentExp);
            foreach (GameObject go in gameManager.team)
            {
                if (DisplayCharacter3.UnitName == go.GetComponent<UnitBehavior>().UnitName)
                {
                    DisplayCharacter3Real = go.GetComponent<UnitBehavior>();
                }
            }
            StatsBreakdown(DisplayCharacter3.position, DisplayCharacter3Real);
        }
        
        if (gameManager.team.Count > 3)
        {
            RealCharacter4 = gameManager.team[3].GetComponent<UnitBehavior>();
            Debug.Log(RealCharacter4.name + RealCharacter4.currentExp);
        }
        StatsBreakdown(DisplayCharacter1.position, DisplayCharacter1Real);
        state = BattleState.PlayerWon;
        if(gameManager.day % 4 ==  0)
        {
            gameManager.money += 2000;
        }
        else
        {
        gameManager.money += 500;
        }
        expSliderP1.value = RealCharacter1.currentExp;
        if (gameManager.team.Count > 1)
        {
            expSliderP2.value = RealCharacter2.currentExp;
        }
        if (gameManager.team.Count > 2)
        {
            expSliderP3.value = RealCharacter3.currentExp;
        }
        int exp1 = (int)(50 - 5 * (playerBehavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * playerBehavior.expmarkplier);
        if (exp1 <= 0) { exp1 = 1; }
        int exp2 = (int)(50 - 5 * (player2Behavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * player2Behavior.expmarkplier);
        if (exp2 <= 0) { exp2 = 1; }
        int exp3 = (int)(50 - 5 * (player3Behavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * player3Behavior.expmarkplier);
        if (exp3 <= 0) { exp3 = 1; }
        int exp4 = (int)(50 - 5 * (playerBehavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * playerBehavior.expmarkplier);
        if (exp4 <= 0) { exp1 = 1; }
        yield return new WaitForSeconds(1);
        if (gameManager.team.Count > 3)
        {
        RealCharacter4.currentExp += exp4;
            if (RealCharacter4.currentExp >= 100)
            {
                LevelUp(RealCharacter4);
            }
        }
        RealCharacter1.currentExp += exp1;
        expSliderP1.value = RealCharacter1.currentExp;

        if (RealCharacter3 != null)
        {
            RealCharacter3.currentExp += exp3;
            if (RealCharacter3.currentExp >= 100) { LevelUp(RealCharacter3); StatsBreakdown(DisplayCharacter3.position, DisplayCharacter3Real);
            }
        }
        if (gameManager.team.Count > 2)
        {
            expSliderP3.value = RealCharacter3.currentExp;
        }


        if (RealCharacter2 != null) {
            RealCharacter2.currentExp += exp2;
            if (RealCharacter2.currentExp >= 100) { LevelUp(RealCharacter2); StatsBreakdown(DisplayCharacter2.position, DisplayCharacter2Real);
            }
        }
        if (gameManager.team.Count > 1)
        {
            expSliderP2.value = RealCharacter2.currentExp;
        }

        if (RealCharacter1.currentExp >= 100) 
        { 
            LevelUp(RealCharacter1);
            StatsBreakdown(DisplayCharacter1.position, DisplayCharacter1Real);
        }
        yield return new WaitForSeconds(4);
        gameManager.BossBattleID = 0;
        gameManager.storyBattle = false;
        gameManager.teamPostPreBattle.Clear();
        gameManager.enemyTeamPostPreBattle.Clear();
        hoverObject.SetActive(false);

        postMatch.SetActive(true);
        postMatchTexts[0].text = cl1.damage.ToString();
    }

    public void PrepScreen()
    {
        gameManager.PrepScreen();
    }
    //randomiza growths
    public void LevelUp(UnitBehavior character)
    {
        character.currentLevel += 1;
        character.currentExp -= 100;
        //aprender Skills
        switch (character.currentLevel, character.currentRank)
        {
            case (5, 1):
                if (character.skill1 != null)
                {
                    character.skillInventory.Add(character.skill1);
                }
                break;
            case (10, 1):
                if (character.skill2 != null)
                {
                    character.skillInventory.Add(character.skill2);
                }
                break;
        }
        //aprender alma
        switch (character.currentLevel, character.currentRank)
        {
            case (10, 1):
                if (character.soul1 != null)
                {
                    character.soulInventory.Add(character.soul1);
                }
                break;
        }
        for (int i = 0; i < 8; i++)
        {
            int r = Random.Range(0, 101);
            if (r <= character.growths[i])
            {
                switch (i)
                {
                    case 0: character.maxhp +=5; break;
                    case 1: character.str++; break;
                    case 2: character.mag++; break;
                    case 3: character.dex++; break;
                    case 4: character.def++; break;
                    case 5: character.mdef++; break;
                    case 6: character.speed++; break;
                    case 7: character.luck++; break;
                }
            }
            //Debug.Log("roll = " + r + "\n growth = " + character.growths[i]);
        };
    }
    public void AttackSetup(UnitBehavior Attacker, UnitBehavior Target)
    {
        Phit = (int)(Attacker.Weapon.hit + (Attacker.dex * 3) + Attacker.luck + Attacker.hit - (Target.speed * 2) - Target.luck - Target.avoid);
        if (Phit < 30)
        {
            Phit = 30;
        }
    }

    public IEnumerator AttackHit(UnitBehavior attacker, UnitBehavior Target, int attackerDamage, List<UnitBehavior> attackerTeam, List<UnitBehavior> targetTeam)
    {
        //Pskill = 0;
        Debug.Log(attacker.name + " hit " + Target.name);
        attacker.SkillManager.currentDamageBonus = 0;
        int tempPskill = attacker.SkillManager.currentDamageBonus;
        foreach (string skill in skillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += +attacker.SkillManager.SkillProc(skill, attacker, Target, attackerTeam, targetTeam);
        }
        int PskillPostSkillProc = attacker.SkillManager.currentDamageBonus;
        if (attacker.soul >= attacker.maxsoul && attacker.equippedSoulIsAttack)
        {
            attacker.soul -= attacker.maxsoul;
            attacker.SkillManager.currentDamageBonus += attacker.SkillManager.SoulProc(attacker.equipedSoul, attacker, Target, attackerTeam, targetTeam);
            yield return new WaitForSeconds(1);
        }
        if (attacker.soul >= attacker.maxsoul && !attacker.equippedSoulIsAttack)
        {
            attacker.soul -= attacker.maxsoul;
            attacker.SkillManager.NaSoulproc(attacker.equipedSoul, attacker, Target, attackerTeam, targetTeam);
        }
        int PskillPostHealthlChange = attacker.SkillManager.currentDamageBonus;
        foreach (string skill in Target.skills)
        {
            Target.SkillManager.currentDamageBonus += Target.SkillManager.PostHealthChange(skill, Target, attacker, targetTeam, attackerTeam);
        }
        Target.SkillManager.currentDamageBonus += Target.SkillManager.PostHealthChange(Target.Weapon.skill, Target, attacker, targetTeam, attackerTeam);
        if (Target.Accesory != null)
        {
            Target.SkillManager.currentDamageBonus += Target.SkillManager.PostHealthChange(Target.Accesory.skill, Target, attacker, targetTeam, attackerTeam);
        }
        int PskillPostTargetPostHealthChange = attacker.SkillManager.currentDamageBonus;
        Debug.Log(attacker.UnitName + " Pskill progression\n" +
            "Pskill dps de skill proc" + PskillPostSkillProc
            + "\nPskill dps de healthChange" + PskillPostHealthlChange
            + "\nPskill dps de target soul proc" + PskillPostTargetPostHealthChange);
        HudUpdate();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 101) <= (int)(attacker.dex / 2) + attacker.Weapon.crit)
        {
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus) * 2;

            ParticleHit(Target);
            hitAudio[1].Play();
            if (attackerDamage + attacker.SkillManager.currentDamageBonus <= 0)
            {
                damageDone = 2;
            }
            CheckDamage(attacker, Target, damageDone);

            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));
            HudUpdate();
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += damageDone * attacker.lifesteal;
                StartCoroutine(FadeOutText(attacker.damageTMP, (damageDone * attacker.lifesteal), true));
            }
            Target.soul += damageDone / 5;
            Debug.Log(attacker.UnitName + " Critou " + Target.UnitName + " " + ((attackerDamage + attacker.SkillManager.currentDamageBonus) * 2) + " de dano\n"
                    + attacker.UnitName + " base power: " + attacker.power + "\n"
                    + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
                    + Target.UnitName + " enemy defense: " + Target.def + "\n"
                    + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                                        + "acerto critico, dano dobrado"
                );
            int c = 0;
            foreach (Slider sl in enemyHpSlider)
            {
                sl.value = enemyTeam[c].hp;
                c++;
            }
            c = 0;
            foreach (Slider sl in playerHpSlider)
            {
                sl.value = playerTeam[c].hp;
                c++;
            }
        }
        else
        {
            ParticleHit(Target);
            hitAudio[0].Play();
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus);
            CheckDamage(attacker, Target, damageDone);
            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));


            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
                StartCoroutine(FadeOutText(attacker.damageTMP, (damageDone * attacker.lifesteal), true));
            }
            Target.soul += ((damageDone) / 5) * Target.damageSoulGain;
            Debug.Log(attacker.UnitName + " atacou " + Target.UnitName + " " + (damageDone) + " de dano\n"
                    + attacker.UnitName + " base power: " + attacker.power + "\n"
                    + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
                    + Target.UnitName + " enemy defense: " + Target.def + "\n"
                    + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                );
            int c = 0;
            foreach (Slider sl in enemyHpSlider)
            {
                sl.value = enemyTeam[c].hp;
                c++;
            }
            c = 0;
            foreach (Slider sl in playerHpSlider)
            {
                sl.value = playerTeam[c].hp;
                c++;
            }
        }        
    }
    public IEnumerator ExtraAttackHit(UnitBehavior attacker, UnitBehavior Target, int attackerDamage, List<UnitBehavior> attackerTeam, List<UnitBehavior> targetTeam, float DamageMultiplier = 1, float lifeSteal =0)
    {
        AttackSetup(attacker, Target);
        attacker.SkillManager.currentDamageBonus = 0;
        Debug.Log("extra attack");
        foreach (string skill in Target.skills)
        {
            attacker.SkillManager.currentDamageBonus += +Target.SkillManager.PostHealthChange(skill, Target, attacker, targetTeam, attackerTeam);
        }
        attacker.SkillManager.currentDamageBonus += +Target.SkillManager.PostHealthChange(Target.Weapon.skill, Target, attacker, targetTeam, attackerTeam);
        if (Target.Accesory != null)
        {
            attacker.SkillManager.currentDamageBonus += +Target.SkillManager.PostHealthChange(Target.Accesory.skill, Target, attacker, targetTeam, attackerTeam);
        }
        HudUpdate();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 101) <= Pcrit)
        {
            ParticleHit(Target);
            hitAudio[1].Play();
            int damageDone = (int)((attackerDamage + attacker.SkillManager.currentDamageBonus) * 2 * DamageMultiplier);

            Target.hp -= damageDone;
            CheckDamage(attacker, Target, damageDone);

            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));

            Debug.Log(attacker.UnitName + " Critou " + Target.UnitName + " " + damageDone + " de dano\n"
        + attacker.UnitName + " base power: " + attacker.power + "\n"
        + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
        + Target.UnitName + " enemy defense: " + Target.def + "\n"
        + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                            + "acerto critico, dano dobrado" + " Foi um ataque extra"
    );
            Debug.Log(lifeSteal);
            if (attacker.lifesteal >= 0.01 | lifeSteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
                attacker.hp += (int)((damageDone) * lifeSteal);
                Debug.Log("life stolen");
                StartCoroutine(FadeOutText(attacker.damageTMP, (damageDone * attacker.lifesteal), true));
            }
            Target.soul += (damageDone * 2) / 5;
            yield return new WaitForSeconds(1);
            int c = 0;
            foreach (Slider sl in enemyHpSlider)
            {
                sl.value = enemyTeam[c].hp;
                c++;
            }
            c = 0;
            foreach (Slider sl in playerHpSlider)
            {
                sl.value = playerTeam[c].hp;
                c++;
            }
        }
        else
        {
            int damageDone = (int)((attackerDamage + attacker.SkillManager.currentDamageBonus) * DamageMultiplier);
            ParticleHit(Target);
            hitAudio[0].Play();
            Target.hp -= damageDone;
            CheckDamage(attacker, Target, damageDone);
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));

            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
                Debug.Log("life stolen");
                StartCoroutine(FadeOutText(attacker.damageTMP, (damageDone * attacker.lifesteal), true));
            }
            if (lifeSteal >= 0.01)
            {
                attacker.hp += (int)((damageDone) * lifeSteal);
                Debug.Log("life stolen");
                StartCoroutine(FadeOutText(attacker.damageTMP, (int)(damageDone * lifeSteal), true));
            }
            Debug.Log(attacker.UnitName + " atacou " + Target.UnitName + " " + damageDone + " de dano\n"
                    + attacker.UnitName + " base power: " + attacker.power + "\n"
                    + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
                    + Target.UnitName + " enemy defense: " + Target.def + "\n"
                    + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                    + "\n Foi um ataque extra"
                );
            Target.soul += damageDone / 5;
            int c = 0;
            foreach (Slider sl in enemyHpSlider)
            {
                sl.value = enemyTeam[c].hp;
                c++;
            }
            c = 0;
            foreach (Slider sl in playerHpSlider)
            {
                sl.value = playerTeam[c].hp;
                c++;
            }
        }
    }

    public void CheckDamage(UnitBehavior attacker, UnitBehavior target, int damageDone = 0)
    {
        if (playerBehavior.UnitName == attacker.UnitName)
        {
            cl1.damage += damageDone;
        }
        else if (player2Behavior.UnitName == attacker.UnitName)
        {
            cl2.damage += damageDone;
        }
        else if(player3Behavior.UnitName == attacker.UnitName)
        {
            cl3.damage += damageDone;
        }

        if (playerBehavior.UnitName == target.UnitName)
        {
            cl1.damageTaken += damageDone;
        }
        else if (player2Behavior.UnitName == target.UnitName)
        {
            cl2.damageTaken += damageDone;
        }
        else if (player3Behavior.UnitName == target.UnitName)
        {
            cl3.damageTaken += damageDone;
        }

    }
     
    public IEnumerator FadeOutText(TextMeshProUGUI tmpOG, int damageDone = 0, bool heal = false)
    {
        TextMeshProUGUI tmp = Instantiate(tmpOG, tmpOG.gameObject.transform);
        tmp.rectTransform.localPosition = new Vector3(0, 0, 0);
        tmp.text = damageDone.ToString();
        tmp.gameObject.SetActive(true);
        for (float f = 1f; f >= -0.05; f -= 0.05f)
        {
            tmp.rectTransform.position += new Vector3(0, 0.07f, 0);
            Color c = tmp.color;
            c.a = f;
            tmp.color = c;
            if (heal)
            {
                tmp.color = Color.green;
            }
            yield return new WaitForSeconds(0.05f);
        }
        tmp.gameObject.SetActive(false);
        Destroy(tmp.gameObject);
    }
    IEnumerator DashToTarget(UnitBehavior attacker, Vector3 target, float tickRate = 0.04f)
    {
        int c = 0;
        float dashSpeed = 30 + (2 * attacker.speed);
        while (c <= 13)
        {
            attacker.animator.transform.position = Vector3.Lerp(attacker.animator.transform.position, target, dashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(tickRate);
            c++;
        }
        attacker.animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        if (tickRate == 0.01f)
        {
            attacker.animator.transform.position = target;
        }
    }
    public void hoverStatUpdate(int character)
    {
        if (character < 3)
        {
            hoverName.text = playerTeam[character].UnitName;
            int c = 0;
            foreach (TextMeshProUGUI skill in hoverSkillnames)
            {
                skill.text = "";
            }
            foreach (string skill in playerTeam[character].skills)
            {
                if(c <4)
                {
                hoverSkillnames[c].text = playerTeam[character].skills[c];
                }
                c++;
            }
            soulName.text = playerTeam[character].equipedSoul;
            weaponStats[0].text = playerTeam[character].Weapon.power.ToString();
            weaponStats[1].text = playerTeam[character].Weapon.hit.ToString();
            weaponStats[2].text = playerTeam[character].Weapon.crit.ToString();
            unitStats[0].text = playerTeam[character].hp.ToString();
            unitStats[1].text = playerTeam[character].str.ToString();
            unitStats[2].text = playerTeam[character].mag.ToString();
            unitStats[3].text = playerTeam[character].dex.ToString();
            unitStats[4].text = playerTeam[character].speed.ToString();
            unitStats[5].text = playerTeam[character].def.ToString();
            unitStats[6].text = playerTeam[character].mdef.ToString();
            unitStats[7].text = playerTeam[character].luck.ToString();
            unitStats[8].text = (playerTeam[character].str + playerTeam[character].Weapon.power).ToString();
            unitStats[9].text = (playerTeam[character].Weapon.hit + (playerTeam[character].dex * 3) + playerTeam[character].luck + playerTeam[character].hit).ToString();
            unitStats[10].text = ((playerTeam[character].speed * 2) - playerTeam[character].luck - playerTeam[character].avoid).ToString();
            unitStats[11].text = ((playerTeam[character].dex / 2) + playerTeam[character].Weapon.crit).ToString();
            InventoryManager im = FindAnyObjectByType<InventoryManager>(findObjectsInactive: FindObjectsInactive.Include);
            WeaponImage.sprite = im.EquipableImages.Where(obj => obj.name == playerTeam[character].Weapon.ItemName).SingleOrDefault();
            Mugshot.sprite = im.playableMugShots.Where(obj => obj.name == playerTeam[character].UnitName + " mugshot").SingleOrDefault();
            if (im.EquipableImages.Where(obj => obj.name == playerTeam[character].Weapon.name).SingleOrDefault() != null)
            {
                WeaponImage.sprite = im.EquipableImages.Where(obj => obj.name == playerTeam[character].Weapon.name).SingleOrDefault();
            }
            if (playerTeam[character].Accesory != null)
            {
            AccessoryImage.sprite = im.EquipableImages.Where(obj => obj.name == playerTeam[character].Accesory.ItemName).SingleOrDefault();
                if (im.EquipableImages.Where(obj => obj.name == playerTeam[character].Accesory.name).SingleOrDefault() != null)
                {
                    AccessoryImage.sprite = im.EquipableImages.Where(obj => obj.name == playerTeam[character].Accesory.name).SingleOrDefault();
                }
            }
            else
            {
                AccessoryImage.sprite = null;
            }
        }
        else
        {
            hoverName.text = enemyTeam[character - 3].UnitName;
            int c = 0;
            foreach (TextMeshProUGUI skill in hoverSkillnames)
            {
                skill.text = "";
            }
            foreach (string skill in enemyTeam[character -3].skills)
            {
                hoverSkillnames[c].text = enemyTeam[character -3].skills[c];
                c++;
            }
            soulName.text = enemyTeam[character -3].equipedSoul;      
            weaponStats[0].text = enemyTeam[character - 3].Weapon.power.ToString();
            weaponStats[1].text = enemyTeam[character - 3].Weapon.hit.ToString();
            weaponStats[2].text = enemyTeam[character - 3].Weapon.crit.ToString();
            unitStats[0].text = enemyTeam[character - 3].hp.ToString();
            unitStats[1].text = enemyTeam[character - 3].str.ToString();
            unitStats[2].text = enemyTeam[character - 3].mag.ToString();
            unitStats[3].text = enemyTeam[character - 3].dex.ToString();
            unitStats[4].text = enemyTeam[character - 3].speed.ToString();
            unitStats[5].text = enemyTeam[character - 3].def.ToString();
            unitStats[6].text = enemyTeam[character - 3].mdef.ToString();
            unitStats[7].text = enemyTeam[character - 3].luck.ToString();
            unitStats[8].text = (enemyTeam[character - 3].str + enemyTeam[character - 3].Weapon.power).ToString();
            unitStats[9].text = (enemyTeam[character - 3].Weapon.hit + (enemyTeam[character - 3].dex * 3) + enemyTeam[character - 3].luck + enemyTeam[character - 3].hit).ToString();
            unitStats[10].text = ((enemyTeam[character - 3].speed * 2) - enemyTeam[character - 3].luck - enemyTeam[character - 3].avoid).ToString();
            unitStats[11].text = ((enemyTeam[character - 3].dex / 2) + enemyTeam[character - 3].Weapon.crit).ToString();
            InventoryManager im = FindAnyObjectByType<InventoryManager>(findObjectsInactive: FindObjectsInactive.Include);
            WeaponImage.sprite = im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Weapon.ItemName).SingleOrDefault();
            Mugshot.sprite = im.playableMugShots.Where(obj => obj.name == enemyTeam[character - 3].UnitName + " mugshot").SingleOrDefault();
            if (im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Weapon.name).SingleOrDefault() != null)
            {
                WeaponImage.sprite = im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Weapon.name).SingleOrDefault();
            }
            if (enemyTeam[character - 3].Accesory != null)
            {
                AccessoryImage.sprite = im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Accesory.ItemName).SingleOrDefault();
                if (im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Accesory.name).SingleOrDefault() != null)
                {
                    AccessoryImage.sprite = im.EquipableImages.Where(obj => obj.name == enemyTeam[character - 3].Accesory.name).SingleOrDefault();
                }
            }
            else
            {
                AccessoryImage.sprite = null;
            }
        }
    }

    private void StatsBreakdown(int Unit1, UnitBehavior displayedUB)
    {
        switch (Unit1) 
       {
            case 1: 
        
        LUPObjects[0].SetActive(true);

        levelUpUnitStatsP1[0].text = displayedUB.maxhp.ToString();
        levelUpUnitStatsP1[1].text = displayedUB.str.ToString();
        levelUpUnitStatsP1[2].text = displayedUB.mag.ToString();    
        levelUpUnitStatsP1[3].text = displayedUB.dex.ToString();
        levelUpUnitStatsP1[4].text = displayedUB.speed.ToString();
        levelUpUnitStatsP1[5].text = displayedUB.def.ToString();
        levelUpUnitStatsP1[6].text = displayedUB.mdef.ToString();
        levelUpUnitStatsP1[7].text = displayedUB.luck.ToString();
        LevelUpNameTextP1.text = displayedUB.UnitName;
        levelTextP1.text = $"lvl: {displayedUB.currentLevel.ToString()}";
        unitLUPAnimatorP1.runtimeAnimatorController = animators[0].runtimeAnimatorController;
        expSliderP1.value = displayedUB.currentExp;
                break;
            case 2:
                if (gameManager.team.Count >= 2)
        {
            LUPObjects[1].SetActive(true);

            levelUpUnitStatsP2[0].text = displayedUB.maxhp.ToString();
            levelUpUnitStatsP2[1].text = displayedUB.str.ToString();
            levelUpUnitStatsP2[2].text = displayedUB.mag.ToString();
            levelUpUnitStatsP2[3].text = displayedUB.dex.ToString();
            levelUpUnitStatsP2[4].text = displayedUB.speed.ToString();
            levelUpUnitStatsP2[5].text = displayedUB.def.ToString();
            levelUpUnitStatsP2[6].text = displayedUB.mdef.ToString();
            levelUpUnitStatsP2[7].text = displayedUB.luck.ToString();
            LevelUpNameTextP2.text = displayedUB.UnitName;
            levelTextP2.text = $"lvl: {displayedUB.currentLevel.ToString()}";
            unitLUPAnimatorP2.runtimeAnimatorController = animators[1].runtimeAnimatorController;
            expSliderP2.value = displayedUB.currentExp;
        }
                break;
            case 3:
                if (gameManager.team.Count >= 3)
        {
            LUPObjects[2].SetActive(true);

            levelUpUnitStatsP3[0].text = displayedUB.maxhp.ToString();
            levelUpUnitStatsP3[1].text = displayedUB.str.ToString();
            levelUpUnitStatsP3[2].text = displayedUB.mag.ToString();
            levelUpUnitStatsP3[3].text = displayedUB.dex.ToString();
            levelUpUnitStatsP3[4].text = displayedUB.speed.ToString();
            levelUpUnitStatsP3[5].text = displayedUB.def.ToString();
            levelUpUnitStatsP3[6].text = displayedUB.mdef.ToString();
            levelUpUnitStatsP3[7].text = displayedUB.luck.ToString();
            LevelUpNameTextP3.text = displayedUB.UnitName;
            levelTextP3.text = $"lvl: {displayedUB.currentLevel.ToString()}";
            unitLUPAnimatorP3.runtimeAnimatorController = animators[2].runtimeAnimatorController;
            expSliderP3.value = displayedUB.currentExp;
        }break;
        default:break;
    }
}

    public Sprite ClassIconPicker(int classID)
    {
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>(FindObjectsInactive.Include);

        switch (classID)
        {
            case 101: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Espadachim").SingleOrDefault();
            case 102: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Guerreiro").SingleOrDefault();
            case 103: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Soldado").SingleOrDefault();
            case 104: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Feiticeiro").SingleOrDefault();
            case 105: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Mistico").SingleOrDefault();
            case 106: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Arqueiro").SingleOrDefault();
            case 107: return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Prisioneiro").SingleOrDefault();
            default:
                return inventoryManager.ClassIcons.Where(obj => obj.name == "Icone_Espadachim").SingleOrDefault();
        }
    }
    private float CheckWeight(UnitBehavior ub)
    {
        if(ub.Weapon.weight == Item.WeaponWeight.Heavy)
        {
            return (float)0.7;
        }
        if (ub.Weapon.weight == Item.WeaponWeight.Medium)
        {
            return (float)0.85;
        }
        return 1;
    } 

    private void ParticleHit(UnitBehavior targetUB)
    {

        ParticleSystem currentparticle = damageParticlesPlayer1;

        if (targetUB.position == 1 && targetUB.enemy == false) { currentparticle = damageParticlesPlayer1; };
        if (targetUB.position == 2 && targetUB.enemy == false) { currentparticle = damageParticlesPlayer2; };
        if (targetUB.position == 3 && targetUB.enemy == false) { currentparticle = damageParticlesPlayer3; };
        if (targetUB.position == 1 && targetUB.enemy == true) { currentparticle = damageParticlesEnemy1; };
        if (targetUB.position == 2 && targetUB.enemy == true) { currentparticle = damageParticlesEnemy2; };
        if (targetUB.position == 3 && targetUB.enemy == true) { currentparticle = damageParticlesEnemy3; };
        currentparticle.Emit(10);
    }       
}
