using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class BattleManager : MonoBehaviour
{
    public AudioSource[] hitAudio;
    public Animator[] animators;
    public UnitBehavior endure;
    public List<string> skillsInUse;
    public List<string> EAskillsInUse;
    //Player1
    public int Pdamage;
    public int Phit;
    public int Pcrit;
    public float Pspeed;
    public int Pskill;
    //Player2
    public int P2damage;
    //Enemy1
    public int Edamage;
    public int Ehit;
    public int Ecrit;
    public float Espeed;
    public int Eskill;

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

    [Header ("UI")]
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

    public enum BattleState {BattleStart,Wait,PlayerTurn,EnemyTurn,PlayerWon,EnemyWon}
    public BattleState state;
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
    }
    private void Update()
    {
        if(state == BattleState.Wait)
        {
            Wait();
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
        enemyTeam[0].animator =animators[3];
        enemyTeam[1].animator =animators[4];
        enemyTeam[2].animator =animators[5];
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
        playerTeam[0].startingPosition = playerTeam[0].animator.transform.position ;
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
        StatChange();
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
                ub.SkillManager.MatchStartProc(skill, ub, targetTeam[StandardTargeting(enemyTeam)],attackerTeam,targetTeam);
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
                ub.SkillManager.MatchStartProc(skill, ub, targetTeam[StandardTargeting(enemyTeam)], attackerTeam, targetTeam);
            }
        }

        state = BattleState.Wait;
    }
    void SetHud()
    {
        int c = 0;
        foreach(Slider sl in playerHpSlider)
        {
        sl.maxValue = playerTeam[c].maxhp;
        }
        c = 0;
        foreach (Slider sl in enemyHpSlider)
        {
            sl.maxValue = enemyTeam[c].maxhp;
            c++;
        }
        c=0;
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
    void Wait()
    {
        if (playerTeam[0].hp > 0) { PlayerBar += Time.deltaTime * playerBehavior.speed * 20; }

        if (playerTeam[1].hp > 0) {PlayerBar2 += Time.deltaTime * player2Behavior.speed * 20; }
        if (playerTeam[2].hp > 0) { PlayerBar3 += Time.deltaTime * player3Behavior.speed * 20; }
        if (enemyTeam[0].hp > 0){EnemyBar += Time.deltaTime * enemyBehavior.speed * 20;}
        if (enemyTeam[1].hp > 0){EnemyBar2 += Time.deltaTime * enemy2Behavior.speed * 20;}
        if (enemyTeam[2].hp > 0){EnemyBar3 += Time.deltaTime * enemy3Behavior.speed * 20;}
        PlayerBars[0] = PlayerBar;
        PlayerBars[1] = PlayerBar2;
        PlayerBars[2] = PlayerBar3;
        EnemyBars[0] = EnemyBar;
        EnemyBars[1] = EnemyBar2;
        EnemyBars[2] = EnemyBar3;
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
            PlayerBar = 0;
            StartCoroutine(Attack(playerTeam[0], enemyTeam[StandardTargeting(enemyTeam)]));
        }
        if (PlayerBar2 >= 100 & state == BattleState.Wait)
        {
            PlayerBar2 = 0;
            StartCoroutine(Attack(playerTeam[1], enemyTeam[StandardTargeting(enemyTeam)]));
        }
        if (PlayerBar3 >= 100 & state == BattleState.Wait)
        {
            PlayerBar3 = 0;
            StartCoroutine(Attack(playerTeam[2], enemyTeam[StandardTargeting(enemyTeam)]));
        }
        if (EnemyBar >= 100 & state == BattleState.Wait)
            {
                EnemyBar = 0;
                StartCoroutine(Attack(enemyTeam[0], playerTeam[StandardTargeting(playerTeam)]));
            }
        if (EnemyBar2 >= 100 & state == BattleState.Wait)
        {
            EnemyBar2 = 0;
            StartCoroutine(Attack(enemyTeam[1], playerTeam[StandardTargeting(playerTeam)]));
        }
        if (EnemyBar3 >= 100 & state == BattleState.Wait)
        {
            EnemyBar3 = 0;
            StartCoroutine(Attack(enemyTeam[2], playerTeam[StandardTargeting(playerTeam)]));
        }
    }
    public int StandardTargeting(List<UnitBehavior> unitList)
    {
        if(unitList[0].hp >= 1)
        {
            return 0;
        }
        if(unitList[1].hp >= 1)
        {
            return 1;
        }
        if(unitList[2].hp >= 1)
        {
            return 2;
        }
        return 0;
    }
    public int LeastHpTargeting(List<UnitBehavior> unitList)
    {
        int leastHp = 0;
        if (unitList[0].hp >= 1)
        {
         leastHp = unitList[0].hp;
        }
        if (unitList[1].hp >=1  && unitList[1].hp > unitList[0].hp)
        {
             leastHp = unitList[1].hp;
        }
        if (unitList[2].hp >= 1 && unitList[2].hp > unitList[1].hp)
        {
             leastHp = unitList[1].hp;
        }
        return leastHp;
    }
    public virtual IEnumerator Attack(UnitBehavior attacker, UnitBehavior Target)
    {
        AttackSetup(attacker, Target);
        attacker.animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        if(attacker.Weapon.weapontype != Item.Weapontype.Bow&& attacker.Weapon.weapontype != Item.Weapontype.Receptacle&& attacker.Weapon.weapontype != Item.Weapontype.Tome)
        {
          StartCoroutine(DashToTarget(attacker, Target.animator.transform.position));
        }
        attacker.animator.SetTrigger("UnitAdvance");
        List<UnitBehavior> attackerTeam;
        List<UnitBehavior> targetTeam;
        if (attacker.enemy)
        {
            state = BattleState.EnemyTurn;

        }
        else { state = BattleState.PlayerTurn; }
        if(attacker.Weapon.damageType == 0)
        {
        attacker.power = attacker.str +attacker.Weapon.power;
        }
        else
        {
            attacker.power = attacker.mag + attacker.Weapon.power;
        }
        //Pskill = 0;
        attacker.SkillManager.currentDamageBonus = 0;
        int attackerDamage = attacker.power - (Target.defenses[attacker.Weapon.damageType] + Target.damagereduction);
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
        if(attacker.personalSkill != null)
        {
            skillsInUse.Add(attacker.personalSkill);
        }
        if(attacker.Weapon != null && attacker.Weapon.skill != null)
        {
            skillsInUse.Add(attacker.Weapon.skill);
        }
        if(attacker.Accesory!= null  && attacker.Accesory.skill != null)
        {
            skillsInUse.Add(attacker.Accesory.skill);
        }
        attacker.soul += 15 + attacker.soulgain;
        Debug.Log(attacker.UnitName + " hit:" + Phit);
        if (attacker.soul < attacker.maxsoul && Random.Range(0, 101) <= Phit)
        {
            StartCoroutine(AttackHit(attacker,Target,attackerDamage, attackerTeam, targetTeam));
        }
        else if(attacker.soul >= attacker.maxsoul && attacker.equippedSoulIsAttack)
        {
            if (Random.Range(0, 101) <= Phit)
            { 
            StartCoroutine(AttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam));
            }
        }
        else if (attacker.soul >= attacker.maxsoul && !attacker.equippedSoulIsAttack)
        {
            attacker.soul -= attacker.maxsoul;
            StartCoroutine(attacker.SkillManager.NaSoulproc(attacker.equipedSoul,attacker,Target,attackerTeam,targetTeam));
        }
        else       
        {
            Target.animator.SetTrigger("UnitDodge");
        }
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(DashToTarget(attacker, attacker.startingPosition,0.01f));
        //inimigo morre
        if (Target.hp <= 0)
        {
            Target.animator.SetTrigger("UnitDie");
        }
        if (enemyTeam[0].hp <= 0 && enemyTeam[1].hp <= 0 && enemyTeam[2].hp <= 0)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            state = BattleState.Wait;
        }
    }
    public virtual IEnumerator ExtraAttack(UnitBehavior attacker, UnitBehavior Target, float DamageMultiplier = 1)
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
            StartCoroutine(ExtraAttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam, DamageMultiplier));
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
        if (enemyTeam[0].hp <=0 && enemyTeam[1].hp <= 0 && enemyTeam[2].hp <= 0)
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
        state = BattleState.PlayerWon;
        yield return new WaitForSeconds(1);
        gameManager.money += 50;
        yield return new WaitForSeconds(1);
        int exp1 = (30 - 5 * (playerBehavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * playerBehavior.expmarkplier);
        if (exp1 <= 0) { exp1 = 1; }
        int exp2 = (30 - 5 * (player2Behavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * player2Behavior.expmarkplier);
        if (exp2 <= 0) { exp2 = 1; }
        int exp3 = (30 - 5 * (player3Behavior.currentLevel - ((enemyBehavior.currentLevel + enemy2Behavior.currentLevel + enemy3Behavior.currentLevel) / 3)) * player3Behavior.expmarkplier);
        if (exp3 <= 0) { exp3 = 1; }
        UnitBehavior RealCharacter2 = null;
        UnitBehavior RealCharacter3 = null;
        UnitBehavior RealCharacter1 = gameManager.team[0].GetComponent<UnitBehavior>();
        if (gameManager.team.Count > 1)
        {
            RealCharacter2 = gameManager.team[1].GetComponent<UnitBehavior>();
        }
        if (gameManager.team.Count > 2) { 
         RealCharacter3 = gameManager.team[2].GetComponent<UnitBehavior>(); }
        RealCharacter1.currentExp += exp1;
        if(RealCharacter2 != null) {
            if (RealCharacter2.currentExp >= 100) { LevelUp(RealCharacter2); }
            RealCharacter2.currentExp += exp2;
        }
        if (RealCharacter3 != null)
        {
            RealCharacter3.currentExp += exp3;
            if (RealCharacter3.currentExp >= 100) { LevelUp(RealCharacter3); }
        }
        yield return new WaitForSeconds(1);
        if (RealCharacter1.currentExp >= 100) { LevelUp(RealCharacter1); }
        gameManager.BossBattleID = 0;
        gameManager.storyBattle = false;
        gameManager.teamPostPreBattle.Clear();
        gameManager.enemyTeamPostPreBattle.Clear();
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
            case(10,1):
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
                if(character.soul1 != null)
                {
                    character.soulInventory.Add(character.soul1);
                }
                break;
        }
        for (int i = 0; i < 8; i++)
        {
            int r = Random.Range(0, 100);
            if(r <= character.growths[i])
            {
                switch (i)
                {
                    case 0: character.maxhp++; break;
                    case 1: character.str++; break;
                    case 2: character.mag++; break;
                    case 3: character.dex++; break;
                    case 4: character.def++; break;
                    case 5: character.mdef++; break;
                    case 6: character.speed++; break;
                    case 7: character.luck++; break;
                }
            }
            Debug.Log("roll = " + r + "\n growth = " + character.growths[i]);
        };
    }
    public void AttackSetup(UnitBehavior Attacker, UnitBehavior Target)
    {     
        Phit = (int)(Attacker.Weapon.hit + (Attacker.dex *3) + Attacker.luck + Attacker.hit - (Target.speed * 2) - Target.luck -Target.avoid);
        if (Phit < 30)
        {
            Phit = 30;
        }
    }

    public IEnumerator AttackHit(UnitBehavior attacker, UnitBehavior Target, int attackerDamage, List<UnitBehavior> attackerTeam, List<UnitBehavior> targetTeam)
    {
        //Pskill = 0;
        attacker.SkillManager.currentDamageBonus = 0;
        int tempPskill = attacker.SkillManager.currentDamageBonus;
        foreach (string skill in skillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += + attacker.SkillManager.SkillProc(skill, attacker, Target, attackerTeam, targetTeam);
        }
        int PskillPostSkillProc = attacker.SkillManager.currentDamageBonus;
        if (attacker.soul >= attacker.maxsoul && attacker.equippedSoulIsAttack)
        {
            Debug.Log("soul esta sendo diminuida");
            attacker.soul -= attacker.maxsoul;
            attacker.SkillManager.currentDamageBonus += attacker.SkillManager.SoulProc(attacker.equipedSoul, attacker, Target, attackerTeam, targetTeam);
            yield return new WaitForSeconds(1);
        }
        if (attacker.soul >= attacker.maxsoul && !attacker.equippedSoulIsAttack)
        {
            Debug.Log("soul esta sendo diminuida");
            attacker.soul -= attacker.maxsoul;
            attacker.SkillManager.NaSoulproc(attacker.equipedSoul, attacker, Target, attackerTeam, targetTeam);
        }
        int PskillPostSoullProc = attacker.SkillManager.currentDamageBonus;
        foreach (string skill in skillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += +attacker.SkillManager.PostHealthChange(skill, attacker, Target, attackerTeam, targetTeam);
        }
        int PskillPostHealthlChange = attacker.SkillManager.currentDamageBonus;
        foreach (string skill in skillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += +Target.SkillManager.PostHealthChange(skill, Target, attacker, targetTeam, attackerTeam);
        }
        int PskillPostTargetPostHealthChange = attacker.SkillManager.currentDamageBonus;
        Debug.Log(attacker.UnitName +" Pskill progression\n" +
            "Pskill dps de skill proc" + PskillPostSkillProc
            + "\nPskill dps de soul proc" + PskillPostSoullProc
            + "\nPskill dps de healthChange" + PskillPostHealthlChange
            + "\nPskill dps de target soul proc" + PskillPostTargetPostHealthChange);
        HudUpdate();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 101) <= (int)(attacker.dex / 2) + attacker.Weapon.crit)
        {
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus) * 2;

               hitAudio[1].Play();
            if(attackerDamage + attacker.SkillManager.currentDamageBonus <= 0)
            {
                damageDone = 2;
            }

            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));
            
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += damageDone * attacker.lifesteal;
 
            }
            Target.soul += damageDone / 5;
            yield return new WaitForSeconds(1);
            Debug.Log(attacker.UnitName + " Critou " + Target.UnitName + " " + ((attackerDamage + attacker.SkillManager.currentDamageBonus) * 2) + " de dano\n"
                    + attacker.UnitName + " base power: " + attacker.power +"\n"
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
            hitAudio[0].Play();
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus);
            if (damageDone <= 0)
            {
                damageDone = 1;
            }
           
            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP,damageDone));


            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
            }
            Target.soul += (damageDone) / 5;
            Debug.Log( attacker.UnitName + " atacou " + Target.UnitName + " " + (damageDone) + " de dano\n"
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
    public IEnumerator ExtraAttackHit(UnitBehavior attacker, UnitBehavior Target, int attackerDamage, List<UnitBehavior> attackerTeam, List<UnitBehavior> targetTeam, float DamageMultiplier = 1)
    {
        AttackSetup(attacker, Target);
        attacker.SkillManager.currentDamageBonus = 0;
        Debug.Log("extra attack");
        foreach (string skill in EAskillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += +attacker.SkillManager.PostHealthChange(skill, attacker, Target, attackerTeam, targetTeam);
        }
        foreach (string skill in EAskillsInUse)
        {
            attacker.SkillManager.currentDamageBonus += +Target.SkillManager.PostHealthChange(skill, Target, attacker, targetTeam, attackerTeam);
        }
        HudUpdate();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 101) <= Pcrit)
        {
            hitAudio[1].Play();
            int damageDone = (int)((attackerDamage + attacker.SkillManager.currentDamageBonus) * 2 * DamageMultiplier);

            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));

            Debug.Log(attacker.UnitName + " Critou " + Target.UnitName + " " + damageDone + " de dano\n"
        + attacker.UnitName + " base power: " + attacker.power + "\n"
        + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
        + Target.UnitName + " enemy defense: " + Target.def + "\n"
        + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                            + "acerto critico, dano dobrado" +" Foi um ataque extra"
    );
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
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
            hitAudio[0].Play();
            Target.hp -= damageDone;
            StartCoroutine(FadeOutText(Target.damageTMP, damageDone));

            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += damageDone * attacker.lifesteal;
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
    public IEnumerator FadeOutText(TextMeshProUGUI tmpOG, int damageDone = 0)
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
        if(tickRate == 0.01f)
        {
            attacker.animator.transform.position = target;
        }
       
    }
    public void StatChange()
    {
        Pdamage = playerBehavior.str - enemyBehavior.def;
        if (Pdamage < 1) { Pdamage = 1; }

        Pcrit = playerBehavior.crit + playerBehavior.dex - enemyBehavior.luck;
        if (Pcrit < 0) { Pcrit = 0; }
        if (Pcrit > 100) { Pcrit = 100; }
        if (playerBehavior.Weapon != null)
        {
            Pdamage += playerBehavior.Weapon.str;
            Pcrit += playerBehavior.Weapon.crit;
        }
        Edamage = enemyBehavior.str - playerBehavior.def;
        if (Edamage < 1) { Edamage = 1; }
        Ehit = (enemyBehavior.hit - playerBehavior.avoid) + (enemyBehavior.dex * 3) - (playerBehavior.dex * 2) + playerBehavior.luck;

        Ecrit = enemyBehavior.crit + enemyBehavior.dex - playerBehavior.luck;
        if (Ecrit < 0) { Ecrit = 0; }
        if (Ecrit > 100) { Ecrit = 100; }
        if (enemyBehavior.Weapon != null)
        {
            Edamage += enemyBehavior.Weapon.str;
            Ehit += enemyBehavior.Weapon.hit;
            Ecrit += enemyBehavior.Weapon.crit;
        }
        if (Ehit < 30)
        {
            Ehit = 30;
        }
        if (Ehit > 100)
        {
            Ehit = 100;
        }
        if (playerBehavior.Accesory != null)
        {
            pAccSpeed = playerBehavior.Accesory.speed;
        }
        if(enemyBehavior.Accesory != null)
        {
            eAccSpeed = enemyBehavior.Accesory.speed;
        }
        if ((playerBehavior.speed + playerBehavior.Weapon.speed + pAccSpeed) >= (enemyBehavior.speed + enemyBehavior.Weapon.speed + eAccSpeed))
        {
            Pspeed = (playerBehavior.speed + playerBehavior.Weapon.speed + pAccSpeed) / (enemyBehavior.speed + enemyBehavior.Weapon.speed + eAccSpeed);
            Espeed = 1;
        }
        else
        {
            Espeed = (enemyBehavior.speed + enemyBehavior.Weapon.speed + eAccSpeed) / (playerBehavior.speed + playerBehavior.Weapon.speed + pAccSpeed);
            Pspeed = 1;
        }
        if (playerBehavior.Weapon != null)
        {

        if (playerBehavior.Weapon.weight == Item.WeaponWeight.Medium)
            {
                Pspeed = (float)(Pspeed * 0.80);
            }
            else if (playerBehavior.Weapon.weight == Item.WeaponWeight.Heavy)
            {
                Pspeed = (float)(Pspeed * 0.65);
            }
            if (enemyBehavior.Weapon.weight == Item.WeaponWeight.Medium)
            {
                Espeed = (float)(Pspeed * 0.80);
            }
            else if (enemyBehavior.Weapon.weight == Item.WeaponWeight.Heavy)
            {
                Espeed = (float)(Pspeed * 0.65);
            }
        }
    }
}