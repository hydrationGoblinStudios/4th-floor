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
    private bool TecnicaImprovisadaboost = false;
    private bool presen�ainabalavel = false;
    private int DanoAscendenteMeter = 0;
    private int concentra��odefeiticeiroboost = 0;
    private int DisparodeGelohit;
    private int DisparodeGeloavoid;

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


            case "Precis�o Mortal":

                return user.hit / 10;

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
            case "Encantamento":

                if (user.position == 2)
                {
                    team[0].speed += (int)(team[0].speed * 0.15);
                }
                if (user.position == 3)
                {
                    team[1].speed += (int)(team[1].speed * 0.15);
                }
                return 0;

            case "Maldi��o":

                if (user.position == 1)
                {
                    enemyTeam[0].speed += (int)(enemyTeam[0].speed * 0.15);
                }
                if (user.position == 2)
                {
                    enemyTeam[1].speed += (int)(enemyTeam[1].speed * 0.15);
                }
                if (user.position == 3)
                {
                    enemyTeam[3].speed += (int)(enemyTeam[3].speed * 0.15);
                }
                return 0;

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

            case "Concentra��o de Feiticeiro":

                if (user.hp == user.maxhp)
                {
                    user.mag += (int)(user.mag * 0.15);
                    concentra��odefeiticeiroboost = (int)(user.mag * 0.15);
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

            case "Dur�o":
                {
                    user.maxhp += user.maxhp / 4;
                    user.hp += user.hp / 4;
                }
                return 0;

            case "Sabedoria Arcana":
                {
                    user.expmarkplier += (int)0.25;
                }
                return 0;
            case "Lutador Vers�til":

                if (user.position == 1 || user.position == 2)
                {
                    user.def += 3;
                    user.mdef += 3;

                }
                if (user.position == 3)
                {
                    user.power += 3;
                }

                return 0;

            case "Persistencia":

                user.speed += user.maxhp - user.hp / 10;

                return 0;

            case "T�cnica Improvisada":

                if (user.position == 1 & user.hp <= user.maxhp / 2)
                {
                    user.hit += 20;
                    user.avoid += 20;

                }

                if (user.position == 2)
                {
                    user.power += 2;
                    user.def += 2;
                    user.mdef += 2;
                }

                if (user.position == 3 & user.hp >= user.maxhp * 0.9)
                {
                    user.power += 5;
                    user.crit += 5;
                }

                return 0;

        }
    }

    //as almas
    public int SoulProc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (SoulName)
        {
            case "Golpe Triplo":
                //extraattack   -user.power/2
                //extraattack   -user.power/2

                return -user.power / 2;

            case "Golpe Focado":

                //if attack = Crit {user.hp += user.power/5}

                return user.power / 2;

            case "Tiro Certeiro":

                //sure shot

                return user.power / 2;


            case "Rajada de Flechas":

                //sure shot

                return (int)(user.power * 0.6);

            case "Trovoada":

                return (int)(target.maxhp * 0.3) - user.power;

            case "Disparo de Gelo":

                StartCoroutine(DisparodeGelo(user, target));

                return 0;

            case "Bola de Fogo":


                return user.power/4 + target.mdef/2;

            case "Ataque Inspirador":

                StartCoroutine(AtaqueInspirador(team));

                return user.power / 4;




            case "Poder Oculto":
                Debug.Log("poderOcultado");
                return 0;

            case "Genki Dama":

                return (team[0].power + team[1].power + team[2].power) * 2;




            default: return 0;
        }
    }

    public int PostHealthChange(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "T�cnica Improvisada":

                if (user.position == 1 & user.hp <= user.maxhp / 2 & !TecnicaImprovisadaboost)
                {
                    user.hit += 20;
                    user.avoid += 20;
                    TecnicaImprovisadaboost = true;

                }

                if (user.position == 1 & user.hp >= user.maxhp / 2 & TecnicaImprovisadaboost)
                {
                    user.hit -= 20;
                    user.avoid -= 20;
                    TecnicaImprovisadaboost = false;

                }

                if (user.position == 3 & user.hp <= user.maxhp * 0.9)
                {
                    user.power -= 5;
                    user.crit -= 5;
                }
                return 0;

            case "Machado Cortado":

                if (user.hp > user.maxhp * 0.8)
                {
                    user.hit -= 20;
                    user.crit -= 10;
                }
                return 0;
            case "Presen�a Inabal�vel":

                if (user.hp < user.maxhp * 0.5 & !presen�ainabalavel)
                {
                    user.def += user.def / 5;
                    user.mdef += user.mdef / 5;
                    presen�ainabalavel = true;

                }
                if (user.hp > user.maxhp * 0.5 & presen�ainabalavel)
                {
                    user.def -= user.def / 5;
                    user.mdef -= user.mdef / 5;
                    presen�ainabalavel = true;
                }

                return 0;

            case "Concentra��o de Feiticeiro":

                if (user.hp < user.maxhp)
                {
                    user.mag -= (concentra��odefeiticeiroboost);

                }
                return 0;

            case "Persistencia":

                user.speed += user.maxhp - user.hp / 10;

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
    IEnumerator DisparodeGelo(UnitBehavior user, UnitBehavior target)
    {
        target.hit -= target.hit / 10 + user.mag / 10;
        target.avoid -= target.avoid / 10 + user.mag / 10;
        DisparodeGelohit = target.hit / 10 + user.mag / 10;
        DisparodeGeloavoid = target.avoid / 10 + user.mag / 10;
        yield return new WaitForSeconds(15);
        target.hit += DisparodeGelohit;
        target.avoid += DisparodeGeloavoid;
    }
    IEnumerator AtaqueInspirador (List <UnitBehavior> team)
    {
        team[0].power += team[0].power / 5;
        team[0].speed += team[0].speed / 5;
        team[1].power += team[1].power / 5;
        team[1].speed += team[1].speed / 5;
        team[2].power += team[2].power / 5;
        team[2].speed += team[2].speed / 5;
        yield return new WaitForSeconds(10);
        team[0].power -= team[0].power / 5;
        team[0].speed -= team[0].speed / 5;
        team[1].power -= team[1].power / 5;
        team[1].speed -= team[1].speed / 5;
        team[2].power -= team[2].power / 5;
        team[2].speed -= team[2].speed / 5;

    }
    IEnumerator NaSoulproc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (SoulName)
        {
            case "Revigoramento":

                if (user.hp == user.maxhp) 
                { 
                    //extraatack += (int) (user.maxhp * 0.15);

                }
                else
                {

                    user.hp += (int)(user.maxhp * 0.3);
                }

                break;


            case "Golpe Poderoso":
                //extraattack user.hit =- 20

                break;

            case "Fortalecimento":

                user.def += (int) (user.def * 0.15);

                break;

            case "Restaura��o Espiritual":
                if (team[0].hp <= team[1].hp & team[0].hp <= team[2].hp)
                {
                    team[0].hp += 10 + user.mag / 5;
                }
                if (team[1].hp <= team[0].hp & team[1].hp <= team[2].hp)
                {
                    team[1].hp += 10 + user.mag / 5;
                }
                if (team[2].hp <= team[1].hp & team[2].hp <= team[0].hp)
                {
                    team[2].hp += 10 + user.mag / 5;
                }
                break;

            case "Ben��o dos Ventos":

                team[0].avoid += team[0].avoid / 10 + user.mag / 5;
                team[1].avoid += team[1].avoid / 10 + user.mag / 5;
                team[2].avoid += team[2].avoid / 10 + user.mag / 5;
                yield return new WaitForSeconds(20);
                team[0].avoid -= team[0].avoid / 10 + user.mag / 5;
                team[1].avoid -= team[1].avoid / 10 + user.mag / 5;
                team[2].avoid -= team[2].avoid / 10 + user.mag / 5;

                break;
                


            default: break;

        }
        yield return new WaitForSeconds(0);
    }
}