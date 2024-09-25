using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SkillManager : MonoBehaviour
{
    private bool espadaCurtaBoost = false;
    private bool arcodasorteboost = false;
    private bool livroarriscadoboost = false;
    private bool machadodeguerraboost = false;
    private bool presençainabalavel = false;
    private int DanoAscendenteMeter = 0;
    private int concentraçãodefeiticeiroboost = 0;

    //Skills que ativam no Dano
    public int SkillProc(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "homer":
                Debug.Log("simpon");
                return 10;

            case "Dano Ascendente":
                {
                    if (DanoAscendenteMeter == 3)
                    {
                        user.power += 1;
                        DanoAscendenteMeter = 0;
                    }
                    else
                    {
                        DanoAscendenteMeter = +1;
                    }

                }
                return 0;
            //rapha lembra de trocar isso ai valeu https://youtu.be/4TZmLE0Pqv0?si=wRvI7GzpHgW1zUhe
            case "Ataque Rapido":
                
                    if (!arcodasorteboost & Random.Range(0, 101) <= user.dex)
                    {
                        //StartCoroutine(Extraattack);
                    }

                
                return 0;
            case "Pancada":

                if (Random.Range(0, 101) <= user.dex)
                {
                    return (int)(user.def * 0.3);
                }
                else
                {
                    return 0;
                }
            case "Magia Destrutiva":

                if (Random.Range(0, 101) <= user.dex)
                {
                    return (int)(user.mag * 0.4);
                }
                else
                {
                    return 0;
                }


            case "Precisão Mortal":

                return user.hit/10;

            default: return 0;

        }

    }
    //pos alma
    public int PostSoulProc(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            default: return 0;
        }
    }
    //inicio da batalha
    public int MatchStartProc(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "Espada Curta":
                if (!espadaCurtaBoost)
                {
                    Debug.Log("boost");
                    user.avoid += 15;
                    espadaCurtaBoost = true;
                }
                return 0;

            case "Arco da Sorte":

                if (!arcodasorteboost & Random.Range(0, 101) <= user.luck)
                {
                    user.power += 5;
                    user.hit += 5;
                    user.crit += 5;
                    arcodasorteboost = true;
                }
                return 0;

            case "Machado Cortado":

                if (user.hp < user.maxhp * 0.8)
                {
                    user.hit += 20;
                    user.crit += 10;
                }
                return 0;

            case "Concentração de Feiticeiro":

                if (user.hp == user.maxhp)
                {
                    user.mag += (int)(user.mag * 0.15);
                    concentraçãodefeiticeiroboost = (int)(user.mag * 0.15);
                }
                return 0;

            case "Livro Arriscado":
                {
                    if (!livroarriscadoboost)
                    {
                        user.soulgain += 5;
                        livroarriscadoboost = true;
                    }


                    return 0;

                }

            case "Machado de Guerra":
                {
                    if (!machadodeguerraboost)
                    {
                        user.speed -= (float)(user.speed * 0.25);
                        machadodeguerraboost = true;
                    }
                }
                return 0;

            case "Foco":
                {
                    StartCoroutine(Foco(user));
                }
                return 0;
            default: return 0;

            case "Durão":
                {
                    user.maxhp += user.maxhp/4;
                    user.hp += user.hp/4;
                }
                return 0;

            case "Sabedoria Arcana":
                {
                    user.expmarkplier += (int) 0.25;
                }
                return 0;

        }
    }

    //as almas
    public int SoulProc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (SoulName)
        {
            case "Poder Oculto":
                Debug.Log("poderOcultado");
                return 0;
            default: return 0;
        }
    }

    public int PostHealthChange(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "Machado Cortado":

                if (user.hp > user.maxhp * 0.8)
                {
                    user.hit -= 20;
                    user.crit -= 10;
                }
                return 0;
            case "Presença Inabalável":
                
                    if (user.hp< user.maxhp * 0.5 & !presençainabalavel)
                {
                    user.def += user.def / 5;
                    user.mdef += user.mdef / 5;
                    presençainabalavel = true;

                }
                    if (user.hp > user.maxhp * 0.5 & presençainabalavel)
                {
                    user.def -= user.def / 5;
                    user.mdef -= user.mdef / 5;
                    presençainabalavel = true;
                }

                return 0;

            case "Concentração de Feiticeiro":

                if (user.hp < user.maxhp)
                {
                    user.mag -= (concentraçãodefeiticeiroboost);
                    
                }
                return 0;
            default: return 0;
        }
    }
    IEnumerator Foco(UnitBehavior user)
    {
        user.hit += 20;
        user.avoid += 20;
        yield return new WaitForSeconds(15);
        user.hit -= 20;
        user.avoid -= 20;
    }
}