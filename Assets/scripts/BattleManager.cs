using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public int Pdamage;
    public int Phit;
    public int Pcrit;
    public float Pspeed;
    public int Psoul;
    public int Pskill;
   
    public int Edamage;
    public int Ehit;
    public int Ecrit;
    public float Espeed;
    public int Esoul;
    [Header("Game objects")]
    public GameManager gameManager;
    public GameObject playerUnit;
    public GameObject enemyUnit;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    [Header ("UI")]
    public TextMeshPro playerName;
    public TextMeshPro enemyName;
    public Slider playerHpSlider;
    public Slider enemyHpSlider;
    public Slider PlayerActionBar;
    public Slider EnemyActionBar;
    public Slider PlayerSoulBar;
    public Slider EnemySoulBar;
    public TextMeshPro battleText;
    public TextMeshPro playerstats;
    public TextMeshPro enemystats;
    [Header("UI")]
    public float PlayerBar;
    public float EnemyBar;


    public enum BattleState {BattleStart,Wait,PlayerTurn,EnemyTurn,PlayerWon,EnemyWon}
    public BattleState state;
    void Start()
    {
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        gameManager = GMobject.GetComponent<GameManager>();
        playerUnit = gameManager.playerUnitInstance;
        enemyUnit = gameManager.enemyUnitInstance;
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
        Esoul = 0;
       GameObject playerGo = Instantiate(playerUnit, playerBattleStation);
       GameObject enemyGo = Instantiate(enemyUnit, enemyBattleStation);
       playerBehavior = playerGo.GetComponent<UnitBehavior>();
       enemyBehavior = enemyGo.GetComponent<UnitBehavior>();
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
        battleText.text = "Its Bors time";
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
        EnemyBar += Time.deltaTime *Espeed * 20;
        PlayerActionBar.value = PlayerBar;
        EnemyActionBar.value = EnemyBar;
        if (PlayerBar >= 100 & state == BattleState.Wait)
        {
            PlayerBar = 0;
            StartCoroutine(PlayerAttack());
        }
        if(EnemyBar >= 100 & state == BattleState.Wait)
            {
                EnemyBar = 0;
                StartCoroutine(EnemyAttack());
            }

    }
    public virtual IEnumerator PlayerAttack()
    {
        state = BattleState.PlayerTurn;
        if (Random.Range(0, 101) <= Phit)
        {
            Pskill = playerBehavior.Proc(Pdamage);
            Psoul += 1;
            if (Psoul >= 3)
            {
                Psoul = 0;
                Pskill += playerBehavior.Soul(Pdamage);
                yield return new WaitForSeconds(1);
            }
            HudUpdate();
            yield return new WaitForSeconds(1);
            if (Random.Range(0, 101) <= Pcrit)
            {
                enemyBehavior.hp -= (Pdamage +Pskill) *2;
                battleText.text = $"{playerBehavior.UnitName} crits!!!";
                yield return new WaitForSeconds(1);
                battleText.text = $"{enemyBehavior.UnitName} lost {(Pskill + Pdamage)*2} hp";
                enemyHpSlider.value = enemyBehavior.hp;
            }
            else
            {
                enemyBehavior.hp -= Pskill + Pdamage;
                battleText.text = $"{enemyBehavior.UnitName} lost {Pdamage + Pskill} hp";
                enemyHpSlider.value = enemyBehavior.hp;
            }
            
        }
        else
        {
            Pskill = playerBehavior.Proc(0);
            battleText.text = (playerBehavior.UnitName + " missed");
        }
        yield return new WaitForSeconds(1f);
        if (enemyBehavior.hp <= 0)
        {
            battleText.text = (playerBehavior.UnitName + " won");
            state = BattleState.PlayerWon;
            yield return new WaitForSeconds(1);
            gameManager.money += 50;
            battleText.text = ("you win 50 gold");
            yield return new WaitForSeconds(1);
            gameManager.PrepScreen();
        }
        else
        {
            state = BattleState.Wait;
        }
    }
    IEnumerator EnemyAttack()
    {
        state = BattleState.PlayerTurn;
        if (Random.Range(0, 101) <= Ehit)
        {
            if (Random.Range(0, 101) <= Ecrit)
            {
                playerBehavior.hp -= Edamage * 2;
                battleText.text = $"{enemyBehavior.UnitName} crits!!!";
                yield return new WaitForSeconds(1);
                battleText.text = $"{playerBehavior.UnitName} lost {Edamage * 2} hp";
                playerHpSlider.value = playerBehavior.hp;
            }
            else
            {
                playerBehavior.hp -= Edamage;
                battleText.text = $"{playerBehavior.UnitName} lost {Edamage} hp";
                playerHpSlider.value = playerBehavior.hp;
            }
            Esoul += 1;
            if(Esoul >= 5){
                Esoul = 0;
                enemyBehavior.Soul(Edamage);
            }
            HudUpdate();
        }
        else
        {
            battleText.text = (enemyBehavior.UnitName + " missed");
        }
        yield return new WaitForSeconds(1f);
        if (playerBehavior.hp <= 0)
        {
            battleText.text = (enemyBehavior.UnitName + " won");
            state = BattleState.EnemyWon;
        }
        else
        {
            state = BattleState.Wait;
        }
    }
    public void StatChange()
    {
        Pdamage = playerBehavior.atk - enemyBehavior.def;
        if (Pdamage < 1) { Pdamage = 1; }
        Phit = playerBehavior.hit + (playerBehavior.dex * 3) - enemyBehavior.dex + enemyBehavior.luck;
        if (Phit > 100) { Phit = 100; }
        Pcrit = playerBehavior.crit + playerBehavior.dex - enemyBehavior.luck;
        if (Pcrit < 0) { Pcrit = 0; }
        if(playerBehavior.Weapon != null)
        {
            Pdamage += playerBehavior.Weapon.atk;
            Phit+=  playerBehavior.Weapon.hit;
            Pcrit += playerBehavior.Weapon.crit;
        }
        Edamage = enemyBehavior.atk - playerBehavior.def;
        if (Edamage < 1) { Edamage = 1; }
        Ehit = enemyBehavior.hit + (enemyBehavior.dex * 3) - playerBehavior.dex + playerBehavior.luck;
        if (Ehit > 100) { Ehit = 100; }
        Ecrit = enemyBehavior.crit + enemyBehavior.dex - playerBehavior.luck;
        if (Ecrit < 0) { Ecrit = 0; }
        if (playerBehavior.Weapon != null)
        {
            Edamage += enemyBehavior.Weapon.atk;
            Ehit += enemyBehavior.Weapon.hit;
            Ecrit += enemyBehavior.Weapon.crit;
        }
        if (playerBehavior.speed >= enemyBehavior.speed)
        {
            Pspeed = playerBehavior.speed / enemyBehavior.speed;
            Espeed = 1;
        }
        else
        {

            Espeed = enemyBehavior.speed / playerBehavior.speed;
            Pspeed = 1;
        }
    }
}
