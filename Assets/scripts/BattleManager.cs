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
    //Player1
    public int Pdamage;
    public int Phit;
    public int Pcrit;
    public float Pspeed;
    public int Psoul;
    public int Pskill;
    //Player2
    public int P2damage;
    public int P2hit;
    public int P2crit;
    public float P2speed;
    public int P2soul;
    public int P2skill;
    //Player3
    public int P3damage;
    public int P3hit;
    public int P3crit;
    public float P3speed;
    public int P3soul;
    public int P3skill;
    //Enemy1
    public int Edamage;
    public int Ehit;
    public int Ecrit;
    public float Espeed;
    public int Esoul;
    public int Eskill;
    //Enemy2
    public int E2damage;
    public int E2hit;
    public int E2crit;
    public float Es2peed;
    public int E2soul;
    public int E2skill;
    //Enemy3
    public int E3damage;
    public int E3hit;
    public int E3crit;
    public float E3speed;
    public int E3soul;
    public int E3skill;

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
    public UnitBehavior enemyBehavior;
    public UnitBehavior enemy2Behavior;
    public UnitBehavior enemy3Behavior;
    [Header ("UI")]
    //names
    public TextMeshPro playerName;
    public TextMeshPro playerName2;
    public TextMeshPro playerName3;
    public TextMeshPro enemyName;
    public TextMeshPro enemyName2;
    public TextMeshPro enemyName3;
    //UI sliders
    public Slider playerHpSlider;
    public Slider playerHpSlider2;
    public Slider playerHpSlider3;
    public Slider enemyHpSlider;
    public Slider enemyHpSlider2;
    public Slider enemyHpSlider3;
    public Slider PlayerActionBar;
    public Slider EnemyActionBar;
    public Slider PlayerSoulBar;
    public Slider EnemySoulBar;
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

    public enum BattleState {BattleStart,Wait,PlayerTurn,EnemyTurn,PlayerWon,EnemyWon}
    public BattleState state;
    void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        playerUnit = gameManager.team[0];
        playerUnit2 = gameManager.team[1];
        playerUnit3 = gameManager.team[2];
        enemyUnit = enemyList[Random.Range(0, enemyList.Length)];
        enemyUnit2 = enemyList[Random.Range(0, enemyList.Length)];
        enemyUnit3 = enemyList[Random.Range(0, enemyList.Length)];
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
       Psoul = 0;
       P2soul = 0;
       P3soul = 0;
       Esoul = 0;
       E2soul = 0;
       E3soul = 0;
       playerGo = Instantiate(playerUnit, playerBattleStation);
       playerGo2 = Instantiate(playerUnit2, playerBattleStation2);
       playerGo3 = Instantiate(playerUnit3, playerBattleStation3);
       GameObject enemyGo = Instantiate(enemyUnit, enemyBattleStation);
       GameObject enemyGo2 = Instantiate(enemyUnit2, enemyBattleStation2);
       GameObject enemyGo3 = Instantiate(enemyUnit3, enemyBattleStation3);
       playerBehavior = playerGo.GetComponent<UnitBehavior>();
       player2Behavior = playerGo2.GetComponent<UnitBehavior>();
       player3Behavior = playerGo3.GetComponent<UnitBehavior>();
       enemyBehavior = enemyGo.GetComponent<UnitBehavior>();
       enemy2Behavior = enemyGo2.GetComponent<UnitBehavior>();
       enemy3Behavior = enemyGo3.GetComponent<UnitBehavior>();
       enemyBehavior.enemy = true;
       yield return new WaitForSeconds(0.5f);
        StatChange();
        SetHud();
        SetHp();

      state = BattleState.Wait;
    }
    void SetHud()
    {
        playerName.text = playerBehavior.UnitName;
        enemyName.text = enemyBehavior.UnitName;
        battleText.text = "Que comece a batalha";
        playerHpSlider.maxValue = playerBehavior.maxhp;
        enemyHpSlider.maxValue = enemyBehavior.maxhp;
        playerstats.text = $"dmg:{Pdamage} \nhit: {Phit} \ncrit:{Pcrit}";
        enemystats.text = $"dmg:{Edamage} \nhit: {Ehit} \ncrit:{Ecrit}";
    }
    void HudUpdate()
    {
        playerHpSlider.maxValue = playerBehavior.maxhp;
        enemyHpSlider.maxValue = enemyBehavior.maxhp;
        playerstats.text = $"dmg:{Pdamage} \nhit: {Phit} \ncrit:{Pcrit}";
        enemystats.text = $"dmg:{Edamage} \nhit: {Ehit} \ncrit:{Ecrit}";
        PlayerSoulBar.value = Psoul;
        EnemySoulBar.value = Esoul;
    }
    void SetHp()
    {
        playerHpSlider.value = playerBehavior.hp;
        enemyHpSlider.value = enemyBehavior.hp;
    }
    void Wait()
    {
        PlayerBar += Time.deltaTime * Pspeed * 20;
        PlayerBar2 += Time.deltaTime * Pspeed * 20;
        PlayerBar3 += Time.deltaTime * Pspeed * 20;
        EnemyBar += Time.deltaTime *Espeed * 20;
        PlayerActionBar.value = PlayerBar;
        EnemyActionBar.value = EnemyBar;
        if (PlayerBar >= 100 & state == BattleState.Wait)
        {
            PlayerBar = 0;
            StartCoroutine(Attack(playerBehavior,enemyBehavior));
        }
        if (PlayerBar2 >= 100 & state == BattleState.Wait)
        {
            PlayerBar2 = 0;
            StartCoroutine(Attack(player2Behavior, enemyBehavior));
        }
        if (PlayerBar3 >= 100 & state == BattleState.Wait)
        {
            PlayerBar3 = 0;
            StartCoroutine(Attack(player3Behavior, enemyBehavior));
        }
        if (EnemyBar >= 100 & state == BattleState.Wait)
            {
                EnemyBar = 0;
                StartCoroutine(Attack(enemyBehavior,playerBehavior));
            }
        if (EnemyBar2 >= 100 & state == BattleState.Wait)
        {
            EnemyBar2 = 0;
            StartCoroutine(Attack(enemyBehavior, playerBehavior));
        }
        if (EnemyBar3 >= 100 & state == BattleState.Wait)
        {
            EnemyBar3 = 0;
            StartCoroutine(Attack(enemyBehavior, playerBehavior));
        }
    }
    public virtual IEnumerator Attack(UnitBehavior attacker, UnitBehavior Target)
    {
        state = BattleState.PlayerTurn;
        attacker.power = attacker.str +attacker.Weapon.power;
        Debug.Log(attacker.power + " " + attacker.UnitName + "\n target defense " +Target.defenses[attacker.Weapon.damageType]);    

        int attackerDamage = attacker.power - Target.defenses[attacker.Weapon.damageType];
        Debug.Log(attackerDamage);
        if (attackerDamage <= 0) { attackerDamage = 1; }
            
        if (Random.Range(0, 101) <= Phit)
        {
            Pskill = attacker.Proc(attackerDamage);
            attacker.soul += 10;
            if (attacker.soul >= 100)
            {
                attacker.soul = 0;
                Pskill += attacker.Soul(attackerDamage);
                yield return new WaitForSeconds(1);
            }
            HudUpdate();
            yield return new WaitForSeconds(1);
            if (Random.Range(0, 101) <= Pcrit)
            {
                hitAudio[1].Play();
                Target.hp -= (attackerDamage + Pskill) * 2;
                battleText.text = $"{attacker.UnitName} causa um acerto critico!!!";
                yield return new WaitForSeconds(1);
                battleText.text = $"{Target.UnitName} perdeu {(Pskill + attackerDamage) *2} hp";
                enemyHpSlider.value = Target.hp;
            }
            else
            {
                hitAudio[0].Play();
                Target.hp -= Pskill + attacker.power;
                battleText.text = $"{Target.UnitName} perdeu {attackerDamage + Pskill} hp";
                enemyHpSlider.value = Target.hp;
            }    
        }
        else
        {
            Pskill = attacker.Proc(0);
            battleText.text = (attacker.UnitName + " errou");
        }
        yield return new WaitForSeconds(1f);
        //inimigo morre
        if (Target.hp <= 0 && Target.Eendure == false)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            Debug.Log("else atingido");
            state = BattleState.Wait;
        }
    }
    public virtual IEnumerator PlayerExtraAttack(string texto)
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
    IEnumerator EnemyAttack()
    {
        state = BattleState.EnemyTurn;
        if (Random.Range(0, 101) <= Ehit)
        {
            Eskill = enemyBehavior.Proc(Edamage);
            Esoul += 1;
            if (Esoul >= 3)
            {
                Esoul = 0;
                Eskill += enemyBehavior.Soul(Edamage);
                yield return new WaitForSeconds(1);
            }
            HudUpdate();
            yield return new WaitForSeconds(1);
            if (Random.Range(0, 101) <= Ecrit)
            {
                hitAudio[1].Play();
                playerBehavior.hp -= (Edamage + Eskill) * 2;
                battleText.text = $"{enemyBehavior.UnitName} causa um acerto critico!!!";
                yield return new WaitForSeconds(1);
                battleText.text = $"{playerBehavior.UnitName} perdeu {(Eskill + Edamage) * 2} hp";
                playerHpSlider.value = playerBehavior.hp;
            }
            else
            {
                hitAudio[0].Play();
                playerBehavior.hp -= Eskill + Edamage;
                battleText.text = $"{playerBehavior.UnitName} perdeu {Edamage + Eskill} hp";
                playerHpSlider.value = playerBehavior.hp;
            }
        }
        else
        {
            Eskill = enemyBehavior.Proc(0);
            battleText.text = (playerBehavior.UnitName + " errou");
        }
        yield return new WaitForSeconds(1f);
        if (playerBehavior.hp <= 0 && playerBehavior.Eendure == false)
        {
            gameManager.PrepScreen();
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
    }
    public IEnumerator PlayerWin()
    {
        battleText.text = (playerBehavior.UnitName + " ganhou");
        state = BattleState.PlayerWon;
        yield return new WaitForSeconds(1);
        gameManager.money += 50;
        battleText.text = ("voce recebe 50 de ouro");
        yield return new WaitForSeconds(1);
        int exp = 30 - 5 * (playerBehavior.level - enemyBehavior.level);
        if (exp <= 0) { exp = 1; }
        UnitBehavior RealCharacter = gameManager.team[0].GetComponent<UnitBehavior>();
        RealCharacter.exp += exp;
        battleText.text = ("voce recebe " + exp + " de experiencia");
        yield return new WaitForSeconds(1);
        if (RealCharacter.exp >= 100) { RandomGrowths(RealCharacter); }
        gameManager.PrepScreen();
    }
    //randomiza growths, adicionar growths por personagem
    public void RandomGrowths(UnitBehavior character)
    {
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
