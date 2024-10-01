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
    private bool presençainabalavel = false;
    private bool apostadoraboost = false;
    private bool vendavalboost = false;
    private bool lancadajustica1 = false;
    private bool lancadajustica2 = false;
    private bool lancadajustica3 = false;
    private bool frigidiboost = false;
    private int DanoAscendenteMeter = 0;
    private int concentraçãodefeiticeiroboost = 0;
    private int DisparodeGelohit;
    private int DisparodeGeloavoid;
    private int arcodarapidezboost;
    private int parfritboost;
    private int frigidistatsdef;
    private int frigidistatsmdef;
    private int Receptaculoamaldiçoadospeed;
    private int Receptaculoamaldiçoadopower;
    private int Ataqueinspiradorpower1;
    private int Ataqueinspiradorpower2;
    private int Ataqueinspiradorpower3;
    private int Ataqueinspiradorspeed1;
    private int Ataqueinspiradorspeed2;
    private int Ataqueinspiradorspeed3;


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
                        DanoAscendenteMeter += 1;
                    }

                }
                return 0;
            //rapha lembra de trocar isso ai valeu https://youtu.be/4TZmLE0Pqv0?si=wRvI7GzpHgW1zUhe
            case "Ataque Rapido":

                if (Random.Range(0, 101) <= user.dex)
                {
                    user.battleManager.ExtraAttack(user, target);
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

                return user.hit / 10;


            case "Espada Amaldiçoada":
                if (Random.Range (0,101) >= 66 - user.luck * 2)
                {
                    user.hp -= user.power;
                    PostHealthChange(skillName,user, target, team, enemyTeam);
                }
                return 0;

            case "Espada Maldita":
                target.maxhp -= 10;
                return 0;

                //vai dar problema se o ataque for magico
            case "Lança da Justiça":
                if (target.position == 1 && lancadajustica1 == false)
                {
                    lancadajustica1 = true;
                    return user.power += user.power / 4 + target.def;
                }
                if (target.position == 2 && lancadajustica2 == false)
                {
                    lancadajustica2 = true;
                    return user.power += user.power / 4 + target.def;
                }
                if (target.position == 3 && lancadajustica3 == false)
                {
                    lancadajustica3 = true;
                    return user.power += user.power / 4 + target.def;
                }
                return 0;

            case "Livro Curativo":
                if (team[0].hp <= team[1].hp & team[0].hp <= team[2].hp)
                {
                    team[0].hp += user.mag / 2 + 2;
                }
                if (team[1].hp <= team[0].hp & team[1].hp <= team[2].hp)
                {
                    team[1].hp += user.mag / 2 + 2;
                }
                if (team[2].hp <= team[1].hp & team[2].hp <= team[0].hp)
                {
                    team[2].hp += user.mag / 2 + 2;
                }
                return 0;

            case "Receptaculo Amaldiçoado":

                StartCoroutine(ReceptaculoAmaldiçoado(target));
                return 0;

            case "Ídolo Manchado":

                if (Random.Range(0, 101) <= user.luck)
                {
                    target.soul -= 15;

                }

                return 0;


            default: return 0;



        }

    }
    //pos alma
    public int PostSoulProc(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "Apostadora":

                if (Random.Range(0, 101) <= user.luck + 7 && apostadoraboost == false)
                {
                    user.hit += 77;
                    apostadoraboost = true;
                }
                return 0;

            case "Arco da Rapidez":

                StartCoroutine(ArcodaRapidez(user));
                return 0;

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

            case "Maldição":

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

            case "Durão":
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
            case "Lutador Versátil":

                if (user.position == 1 || user.position == 2)
                {
                    user.damagereduction += 3;

                }
                if (user.position == 3)
                {
                    user.power += 3;
                }

                return 0;

            case "Persistencia":

                user.speed += user.maxhp - user.hp / 10;

                return 0;

            case "Técnica Improvisada":

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


            case "Indestrutível":

                user.damagereduction += 3;
                StartCoroutine(Indestrutivel(user));
                return 0;

            case "Parfrit":

                parfritboost = user.dex;
                if (parfritboost >= 23)
                {
                    parfritboost = 23;
                }
                user.power += parfritboost;
                return 0;

            case "Arco do Gigante":

                user.maxhp += user.maxhp / 2;
                return 0;


            default: return 0;

        }
    }

    //as almas
    public int SoulProc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (SoulName)
        {
            case "Golpe Triplo":
                user.power -= user.power / 2;
                user.battleManager.ExtraAttack(user, target);
                user.battleManager.ExtraAttack(user, target);
                user.power += user.power / 2;

                return -user.power / 2;

            case "Golpe Focado":

                //if attack = Crit {user.hp += user.power/5}

                return user.power / 2;

            case "Tiro Certeiro":

                //sure shot

                return user.power / 2;


            case "Rajada de Flechas":

                user.power -= (int)(user.power * 0.4);
                user.battleManager.ExtraAttack(user, enemyTeam[0]);
                user.battleManager.ExtraAttack(user, enemyTeam[1]);
                user.battleManager.ExtraAttack(user, enemyTeam[2]);
                user.power += (int)(user.power * 0.4);

                return 0;

            case "Trovoada":

                return (int)(target.maxhp * 0.3) - user.power;

            case "Disparo de Gelo":

                StartCoroutine(DisparodeGelo(user, target));

                return 0;

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
            case "Técnica Improvisada":

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
            case "Presença Inabalável":

                if (user.hp < user.maxhp * 0.5 & !presençainabalavel)
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

            case "Persistencia":

                user.speed += user.maxhp - user.hp / 10;

                return 0;

            case "Vendaval":

                if (user.hp <=  user.maxhp * 0.4 && vendavalboost == false)
                {
                    user.avoid += 30;
                    vendavalboost = true;
                }

                if (user.hp >= user.maxhp * 0.4 && vendavalboost == true)
                {
                    user.avoid -= 30;
                    vendavalboost = false;
                }
                return 0;

            case "Frigidi":
                if (user.hp <= user.maxhp/2 && frigidiboost == false)
                {
                    user.def += user.def / 2;
                    user.mdef += user.mdef / 2;
                    frigidistatsdef = user.def += user.def / 2;
                    frigidistatsmdef = user.mdef += user.mdef / 2;

                    frigidiboost = true;
                }
                if (user.hp >= user.maxhp / 2 && frigidiboost == true)
                {
                    user.def += frigidistatsdef;
                    user.mdef += frigidistatsmdef;
                    frigidiboost = true;
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
    IEnumerator DisparodeGelo(UnitBehavior user, UnitBehavior target)
    {
        DisparodeGelohit = target.hit / 10 + user.mag / 10;
        DisparodeGeloavoid = target.avoid / 10 + user.mag / 10;
        target.hit -= target.hit / 10 + user.mag / 10;
        target.avoid -= target.avoid / 10 + user.mag / 10;

        yield return new WaitForSeconds(15);
        target.hit += DisparodeGelohit;
        target.avoid += DisparodeGeloavoid;
    }
    IEnumerator AtaqueInspirador (List <UnitBehavior> team)
    {
        Ataqueinspiradorpower1 = team[0].power / 5;
        Ataqueinspiradorspeed1 = (int) team[0].speed / 5;
        Ataqueinspiradorpower2 = team[1].power / 5;
        Ataqueinspiradorspeed2 = (int) team[1].speed / 5;
        Ataqueinspiradorpower3 = team[2].power / 5;
        Ataqueinspiradorspeed3 = (int) team[2].speed / 5;
        team[0].power += team[0].power / 5;
        team[0].speed += team[0].speed / 5;
        team[1].power += team[1].power / 5;
        team[1].speed += team[1].speed / 5;
        team[2].power += team[2].power / 5;
        team[2].speed += team[2].speed / 5;
        yield return new WaitForSeconds(10);
        team[0].power -= Ataqueinspiradorpower1;
        team[0].speed -= Ataqueinspiradorspeed1;
        team[1].power -= Ataqueinspiradorpower2;
        team[1].speed -= Ataqueinspiradorspeed2;
        team[2].power -= Ataqueinspiradorpower3;
        team[2].speed -= Ataqueinspiradorspeed3;

    }
    IEnumerator Indestrutivel(UnitBehavior user)
    {
        user.damagereduction += 25;
        yield return new WaitForSeconds(12);
        user.damagereduction += 25;

    }
    IEnumerator ArcodaRapidez(UnitBehavior user)
    {
        user.speed += user.speed/5;
        arcodarapidezboost = (int) (user.speed += user.speed / 5);
        yield return new WaitForSeconds(10);
        user.speed -= arcodarapidezboost;

    }
    IEnumerator ReceptaculoAmaldiçoado(UnitBehavior target)
    {
        Receptaculoamaldiçoadospeed = (int)target.speed / 10;
        Receptaculoamaldiçoadopower = target.power / 10;
        target.speed -= target.speed / 10;
        target.power -= target.power / 10;
        
        
        yield return new WaitForSeconds(15);
        target.speed += Receptaculoamaldiçoadospeed;
        target.power += Receptaculoamaldiçoadopower;
    }


    public IEnumerator NaSoulproc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {

        switch (SoulName)
        {
            case "Revigoramento":

                if (user.hp == user.maxhp)
                {
                    user.power += (int)(user.maxhp * 0.15);
                    user.battleManager.ExtraAttack(user, target);
                    user.power -= (int)(user.maxhp * 0.15);

                }
                else
                {

                    user.hp += (int)(user.maxhp * 0.3);
                }

                break;


            case "Golpe Poderoso":

                user.hit -= 20;
                user.battleManager.ExtraAttack(user, target);
                user.hit += 20;

                break;

            case "Fortalecimento":

                user.def += (int)(user.def * 0.15);

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
