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
    public int SkillProc (string skillName,UnitBehavior user, UnitBehavior target, UnitBehavior[] team, UnitBehavior[] enemyTeam)
    {
        switch (skillName)
        {
            case"homer":
                Debug.Log("simpon");
                return 10;
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


            default: return 0;

        }

    }
    //pos alma
    public int PostSoulProc(string skillName, UnitBehavior user, UnitBehavior target, UnitBehavior[] team, UnitBehavior[] enemyTeam)
    {
        switch (skillName)
        {
            default: return 0;
        }
    }
    //inicio da batalha
    public int MatchStartProc(string skillName, UnitBehavior user, UnitBehavior target, UnitBehavior[] team, UnitBehavior[] enemyTeam)
    {
        switch (skillName)
        {
            default: return 0;
        }
    }

    //as almas
    public int SoulProc(string skillName, UnitBehavior user, UnitBehavior target, UnitBehavior[] team, UnitBehavior[] enemyTeam)
    {
        switch (skillName)
        {
            default: return 0;
        }
    }

}
