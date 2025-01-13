using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BattleManager : MonoBehaviour
{
    public AudioSource[] hitAudio;
    public Animator animator;
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
    public TextMeshPro playerName;
    public TextMeshPro playerName2;
    public TextMeshPro playerName3;
    public TextMeshPro enemyName;
    public TextMeshPro enemyName2;
    public TextMeshPro enemyName3;
    //UI sliders
    public List<Slider> playerHpSlider;
    public List<Slider> enemyHpSlider;
    public List<Slider> PlayerActionBar;
    public List<Slider> EnemyActionBar;
    public List<Slider> PlayerSoulBar;
    public List<Slider> EnemySoulBar;
    //Ui elements
    public TextMeshPro battleText;
    public TextMeshPro playerstats;
    public TextMeshPro enemystats;
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
        playerGo.GetComponent<UnitBehavior>().Icon = GameObject.FindGameObjectWithTag("Player Icon 1");
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
        enemyTeam[0].position = 1;
        enemyTeam[1].position = 2;
        enemyTeam[2].position = 3;
        foreach(UnitBehavior ub in playerTeam)
        {
            ub.battleManager = this;
        }
        foreach (UnitBehavior ub in enemyTeam)
        {
            ub.battleManager = this;
        }
        playerTeam[0].Icon = GameObject.FindGameObjectWithTag("Player Icon 1");
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
        playerName.text = playerBehavior.UnitName;
        enemyName.text = enemyBehavior.UnitName;
        battleText.text = "Que comece a batalha";
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
        playerstats.text = $"dmg:{Pdamage} \nhit: {Phit} \ncrit:{Pcrit}";
        enemystats.text = $"dmg:{Edamage} \nhit: {Ehit} \ncrit:{Ecrit}";
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
        playerstats.text = $"dmg:{Pdamage} \nhit: {Phit} \ncrit:{Pcrit}";
        enemystats.text = $"dmg:{Edamage} \nhit: {Ehit} \ncrit:{Ecrit}";
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
        PlayerBar += Time.deltaTime * playerBehavior.speed * 20;
        PlayerBar2 += Time.deltaTime * player2Behavior.speed * 20;
        PlayerBar3 += Time.deltaTime * player3Behavior.speed * 20;
        EnemyBar += Time.deltaTime *enemyBehavior.speed * 20;
        EnemyBar2 += Time.deltaTime * enemy2Behavior.speed * 20;
        EnemyBar3 = Time.deltaTime* enemy3Behavior.speed *20;
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
        List<UnitBehavior> attackerTeam;
        List<UnitBehavior> targetTeam;
        if (attacker.enemy)
        {
            state = BattleState.EnemyTurn;

        }
        else { state = BattleState.PlayerTurn; }
        attacker.power = attacker.str +attacker.Weapon.power;
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
            battleText.text = (attacker.UnitName + " errou");
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
    public virtual IEnumerator ExtraAttack(UnitBehavior attacker, UnitBehavior Target)
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
            StartCoroutine(ExtraAttackHit(attacker, Target, attackerDamage, attackerTeam, targetTeam));
        }
        else
        {
            battleText.text = (attacker.UnitName + " errou");
            Debug.Log("extra attack errou :(");
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
   /* public virtual IEnumerator PlayerExtraAttack(string texto)
    {
        battleText.text = texto;
        yield return new WaitForSeconds((float)3.01);
        state = BattleState.PlayerTurn;
        if (Random.Range(0, 101) <= Pcrit)
        {
            hitAudio[1].Play();
            enemyBehavior.hp -= (Pdamage + Pskill) * 2;
            battleText.text = $"{playerBehavior.UnitName} causa um acerto critico!!!";
            yield return new WaitForSeconds(1);
            battleText.text = $"{enemyBehavior.UnitName} perdeu {(Pskill + Pdamage) * 2} hp";
            enemyHpSlider.value = enemyBehavior.hp;
        }
        else
        {
            hitAudio[0].Play();
            enemyBehavior.hp -= Pskill + Pdamage;
            battleText.text = $"{enemyBehavior.UnitName} perdeu {Pdamage + Pskill} hp";
            enemyHpSlider.value = enemyBehavior.hp;
        }
        HudUpdate();
        yield return new WaitForSeconds(1f);
        if (enemyBehavior.hp <= 0 && enemyBehavior.Eendure == false)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            state = BattleState.Wait;
        }
    }
    public virtual IEnumerator EnemyExtraAttack(string texto)
    {
        battleText.text = texto;
        yield return new WaitForSeconds((float)3.01);
        state = BattleState.EnemyTurn;
        if (Random.Range(0, 101) <= Ecrit)
        {
            playerBehavior.hp -= (Edamage + Eskill) * 2;
            battleText.text = $"{enemyBehavior.UnitName} causa um acerto critico!!!";
            yield return new WaitForSeconds(1);
            battleText.text = $"{playerBehavior.UnitName} perdeu {(Eskill + Edamage) * 2} hp";
            playerHpSlider.value = playerBehavior.hp;
        }
        else
        {
            playerBehavior.hp -= Eskill + Edamage;
            battleText.text = $"{playerBehavior.UnitName} perdeu {Edamage + Eskill} hp";
            playerHpSlider.value = playerBehavior.hp;
        }
        HudUpdate();
        yield return new WaitForSeconds(1f);
        if (enemyBehavior.hp <= 0 && enemyBehavior.Eendure == false)
        {
            gameManager.PrepScreen();
        }
        else
        {
            state = BattleState.Wait;
        }
    }*/
    //rpha por favor muda esse codigo para individualmente para os 3 personagens
    public IEnumerator PlayerWin()
    {
        battleText.text = (playerBehavior.UnitName + " ganhou");
        state = BattleState.PlayerWon;
        yield return new WaitForSeconds(1);
        gameManager.money += 50;
        battleText.text = ("voce recebe 50 de ouro");
        yield return new WaitForSeconds(1);
        int exp = (30 - 5 * (playerBehavior.currentLevel - enemyBehavior.currentLevel)* playerBehavior.expmarkplier);
        if (exp <= 0) { exp = 1; }
        UnitBehavior RealCharacter = gameManager.team[0].GetComponent<UnitBehavior>();
        RealCharacter.currentExp += (exp * RealCharacter.expmarkplier) ;
        battleText.text = ("voce recebe " + exp + " de experiencia");
        yield return new WaitForSeconds(1);
        if (RealCharacter.currentExp >= 100) { LevelUp(RealCharacter); }
        gameManager.storyBattle = false;
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
        if (Random.Range(0, 101) <= Pcrit)
        {
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus) * 2;

               hitAudio[1].Play();
            if(attackerDamage + attacker.SkillManager.currentDamageBonus <= 0)
            {
                damageDone = 2;
            }

            Target.hp -= damageDone;
            
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += damageDone * attacker.lifesteal;
 
            }
            Target.soul += damageDone / 5;
            battleText.text = $"{attacker.UnitName} causa um acerto critico!!!";
            yield return new WaitForSeconds(1);
            battleText.text = $"{Target.UnitName} perdeu {damageDone} hp";
            Debug.Log(attacker.UnitName + " atacou " + Target.UnitName + " " + ((attackerDamage + attacker.SkillManager.currentDamageBonus) * 2) + " de dano\n"
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
            
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += (damageDone) * attacker.lifesteal;
            }
            Target.soul += (damageDone) / 5;
            battleText.text = $"{Target.UnitName} perdeu {damageDone} hp";
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
    public IEnumerator ExtraAttackHit(UnitBehavior attacker, UnitBehavior Target, int attackerDamage, List<UnitBehavior> attackerTeam, List<UnitBehavior> targetTeam)
    {
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
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus) * 2;

            Target.hp -= damageDone;
            Debug.Log(attacker.UnitName + " atacou " + Target.UnitName + " " + (attackerDamage + attacker.SkillManager.currentDamageBonus) + " de dano\n"
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
            battleText.text = $"{attacker.UnitName} causa um acerto critico!!!";
            yield return new WaitForSeconds(1);
            battleText.text = $"{Target.UnitName} perdeu {damageDone * 2} hp";
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
            int damageDone = (attackerDamage + attacker.SkillManager.currentDamageBonus);
            hitAudio[0].Play();
            Target.hp -= damageDone;
            if (attacker.lifesteal >= 0.01)
            {
                attacker.hp += damageDone * attacker.lifesteal;
            }
            battleText.text = $"{Target.UnitName} perdeu {damageDone} hp";
            Debug.Log(attacker.UnitName + " atacou " + Target.UnitName + " " + (attackerDamage + attacker.SkillManager.currentDamageBonus) + " de dano\n"
                    + attacker.UnitName + " base power: " + attacker.power + "\n"
                    + attacker.UnitName + " added by skills: " + attacker.SkillManager.currentDamageBonus + "\n"
                    + Target.UnitName + " enemy defense: " + Target.def + "\n"
                    + Target.UnitName + " enemy magic defense: " + Target.mdef + "\n"
                    + "\n Foi um ataque extra"
                );
            Target.soul += damageDone / 5;
            battleText.text = $"{Target.UnitName} perdeu {attackerDamage + attacker.SkillManager.currentDamageBonus } hp";
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
        public void StatChange()
    {
        Pdamage = playerBehavior.str - enemyBehavior.def;
        if (Pdamage < 1) { Pdamage = 1; }
        Phit = (playerBehavior.hit - enemyBehavior.avoid) + (playerBehavior.dex * 3) - (enemyBehavior.dex * 2) + enemyBehavior.luck;

        Pcrit = playerBehavior.crit + playerBehavior.dex - enemyBehavior.luck;
        if (Pcrit < 0) { Pcrit = 0; }
        if (Pcrit > 100) { Pcrit = 100; }
        if (playerBehavior.Weapon != null)
        {
            Pdamage += playerBehavior.Weapon.str;
            Phit += playerBehavior.Weapon.hit;
            Pcrit += playerBehavior.Weapon.crit;
        }
        if (Phit < 30)
        {
            Phit = 30;
        }
        if (Phit > 100)
        {
            Phit = 100;
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
