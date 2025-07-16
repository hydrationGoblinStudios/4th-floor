using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    public Sprite[] Icones;
    //statos base
    public int maxhp;
    public int hp;
    public int power;
    public int str;
    public int mag;
    public int dex;
    public int def;
    public int mdef;
    public int[] defenses;
    public int luck;
    public float speed;
    private float  PersistenciaSpeedbase;
    //flag de uso
    private bool espadaCurtaBoost = false;
    private bool arcodasorteboost = false;
    private bool livroarriscadoboost = false;
    private bool machadodeguerraboost = false;
    private bool TecnicaImprovisadaboost = false;
    private bool TecnicaImprovisada3 = false;
    private bool presençainabalavel = false;
    private bool apostadoraboost = false;
    private bool vendavalboost = false;
    private bool lancadajustica1 = false;
    private bool lancadajustica2 = false;
    private bool lancadajustica3 = false;
    private bool frigidiboost = false;
    private bool poçãodevidause = false;
    private bool poçãodevelocidadeuse = false;
    private bool poçãodeforçause = false;
    private bool moedamagicaactive = false;
    private bool machadocortadoboost = false;
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
    private int poçãodeforçastacks;
    

    public int currentDamageBonus;


    //Skills que ativam no Dano
    public int SkillProc(string skillName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (skillName)
        {
            case "homer":
                Debug.Log(user.UnitName);
                StartCoroutine(IconPopup(user.Icon, "homer"));
                Debug.Log("simpon");
                return 10;

            case "Dano Ascendente":
                {
                    if (DanoAscendenteMeter == 3)
                    {
                        Debug.Log("dano ascendeu");
                        StartCoroutine(IconPopup(user.Icon, "Dano Ascendente"));
                        user.power += 1;
                        user.damagereduction += 1;
                        DanoAscendenteMeter = 0;
                    }
                    else
                    {
                        DanoAscendenteMeter += 1;
                    }

                }
                return 0;

            case "Ataque Rápido":

                if (Random.Range(0, 101) <= user.dex)
                {
                    StartCoroutine(IconPopup(user.Icon, "Ataque Rápido"));
                    StartCoroutine(user.battleManager.ExtraAttack(user, target));
                }


                return 0;

            case "Pancada":

                if (Random.Range(0, 101) <= user.dex)
                {
                    StartCoroutine(IconPopup(user.Icon, "Pancada"));
                    return (int)(user.def * 0.3);
                }
                else
                {
                    return 0;
                }

            case "Magia Destrutiva":

                if (Random.Range(0, 101) <= user.dex)
                {
                    StartCoroutine(IconPopup(user.Icon, "Magia Destrutiva"));
                    return (int)(user.mag * 0.4);
                }
                else
                {
                    return 0;
                }


            case "Precisão Mortal":

                int x = (int)(user.Weapon.hit + (user.dex * 3) + user.luck + user.hit - (target.speed * 2) - target.luck - target.avoid);
                x -= 100;
                if (x < 0)
                {
                    x = 0;
                }
                Debug.Log("x: " + x);
                int y = 0;

                if (user.Weapon.damageType == 0)
                {
                    y = user.Weapon.power + user.str;
                }
                else
                {
                    y = (int)(user.Weapon.power + user.mag);
                }
                Debug.Log("y: " + y);
                int z = y - target.defenses[user.Weapon.damageType];
                if (z < 1)
                {
                    z = 1;
                }
                Debug.Log("z: " + z);
                Debug.Log("pricisão mortal: "+(int)((float)z / 100 * x));
                return (int)((float)z / 100 * x);


            case "Espada Amaldiçoada":
                if (Random.Range (0,101) >= 66 - user.luck * 2)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Espadachim"));
                    user.hp -= user.power;
                    PostHealthChange(skillName,user, target, team, enemyTeam);
                    user.battleManager.HudUpdate();
                }
                return 0;

            case "Espada Maldita":
                target.maxhp -= 10;
                return 0;

            case "Lança da Justiça":
                if (target.position == 1 && lancadajustica1 == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Soldado"));
                    lancadajustica1 = true;
                    return user.power += power / 4 + target.defenses[user.Weapon.damageType];
                }
                if (target.position == 2 && lancadajustica2 == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Soldado"));
                    lancadajustica2 = true;
                    return user.power += power / 4 + target.defenses[user.Weapon.damageType];
                }
                if (target.position == 3 && lancadajustica3 == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Soldado"));
                    lancadajustica3 = true;
                    return user.power += power / 4 + target.defenses[user.Weapon.damageType];
                }
                return 0;

            case "Livro Curativo":
                if (team[0].hp <= team[1].hp & team[0].hp <= team[2].hp && team[0].hp >0)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                    Debug.Log(team[0].hp + " hp slot 1");
                    team[0].hp += user.mag / 2 + 2;                   
                    Debug.Log(team[0].hp + " hp slot 1");
                }
                if (team[1].hp <= team[0].hp & team[1].hp <= team[2].hp && team[1].hp > 0)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                    Debug.Log(team[1].hp + " hp slot 2");
                    team[1].hp += user.mag / 2 + 2;
                    Debug.Log(team[1].hp + " hp slot 2");

                }
                if (team[2].hp <= team[1].hp & team[2].hp <= team[0].hp && team[2].hp > 0)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                    Debug.Log(team[2].hp + " hp slot 3");
                    team[2].hp += user.mag / 2 + 2;
                    Debug.Log(team[2].hp + " hp slot 3");
                }

                PostHealthChange(skillName, user, target, team, enemyTeam);
                user.battleManager.HudUpdate();

                return 0;

            case "Receptaculo Amaldiçoado":

                StartCoroutine(ReceptaculoAmaldiçoado(target));
                return 0;

            case "Ídolo Manchado":

                if (Random.Range(0, 101) <= user.luck)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                    target.soul -= 15;

                }

                return 0;


            case "Poção de Foco":
                if (poçãodeforçastacks >= 1)
                {
                    poçãodeforçastacks -= 1;
                    if (poçãodeforçastacks == 0)
                    {
                        StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                        user.power -= 5;
                    }
                }

                return 0;


            case "Moeda Magica":
                if (moedamagicaactive == true)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                    moedamagicaactive = false;
                    return (int)(user.power * 0.75);
                    
                }
                else
                {
                    return 0;
                }

            case "Prego Enferrujado":
                target.dex -= 1;
                target.speed -= 1;

                return 0;

            case "Pólen Dourado":
                user.hp += (int) ((user.maxhp - user.hp) * 0.15);

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
                    StartCoroutine(IconPopup(user.Icon, "Icone_Guerreiro"));
                    user.hit += 77;
                    apostadoraboost = true;
                }
                return 0;

            case "Arco da Rapidez":

                StartCoroutine(IconPopup(user.Icon, "Icone_Arqueiro"));
                StartCoroutine(ArcodaRapidez(user));
                return 0;

            case "Moeda Magica":
                moedamagicaactive = true;

                return 0;

            case "Tomo Apoteótico":
                StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                user.power += 2;

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

                StartCoroutine(IconPopup(user.Icon, "Encantamento"));

                if (user.position == 2)
                {

                    if ((int)(team[0].speed * 0.15) <= 1)
                    {
                        team[0].speed += 1;
                    }
                    else
                    {
                        team[0].speed += (int)(team[0].speed * 0.15);
                    }


                }
                if (user.position == 3)
                {
                    if ((int)(team[1].speed * 0.15) <= 1)
                    {
                        team[1].speed += 1;
                    }
                    else
                    {
                        team[1].speed += (int)(team[1].speed * 0.15);
                    }
                }
                return 0;

            case "Maldição":

                StartCoroutine(IconPopup(user.Icon, "Maldição"));
                if (user.position == 1)
                {
                    if ((int)(enemyTeam[0].speed * 0.15) <= 1)
                    {
                        team[0].speed += 1;
                    }
                    else
                    {
                        enemyTeam[0].speed += (int)(enemyTeam[0].speed * 0.15);
                    }
                }
                if (user.position == 2)
                {
                    if ((int)(enemyTeam[0].speed * 0.15) <= 1)
                    {
                        team[1].speed += 1;
                    }
                    else
                    {
                        enemyTeam[1].speed += (int)(enemyTeam[1].speed * 0.15);
                    }
                }
                if (user.position == 3)
                {
                    if ((int)(enemyTeam[2].speed * 0.15) <= 1)
                    {
                        team[2].speed += 1;
                    }
                    else
                    {
                        enemyTeam[2].speed += (int)(enemyTeam[2].speed * 0.15);
                    }
                }
                return 0;

            case "Espada Curta":
                if (!espadaCurtaBoost)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Espadachim"));
                    Debug.Log("boost");
                    user.avoid += 15;
                    espadaCurtaBoost = true;
                }
                return 0;

            case "Arco da Sorte":

                if (!arcodasorteboost & Random.Range(0, 101) <= user.luck)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Arqueiro"));
                    user.power += 7;
                    user.hit += 7;
                    user.crit += 7;
                    arcodasorteboost = true;
                }
                return 0;

            case "Machado Cortado":

                if (user.hp >= user.maxhp * 0.8 & machadocortadoboost == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Guerreiro"));
                    user.hit += 20;
                    user.crit += 10;
                    machadocortadoboost = true;
                }

                return 0;

            case "Concentração de Feiticeiro":

                if (user.hp == user.maxhp)
                {
                    StartCoroutine(IconPopup(user.Icon, "Concentração de Feiticeiro"));
                    concentraçãodefeiticeiroboost = (int)(user.mag * 0.15);
                    user.mag += (int)(user.mag * 0.15);
;
                }
                return 0;

            case "Livro Arriscado":
                {
                    if (!livroarriscadoboost)
                    {
                        StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                        user.soulgain += 5;
                        livroarriscadoboost = true;
                    }


                    return 0;

                }

            case "Machado de Guerra":
                {
                    if (!machadodeguerraboost)
                    {
                        StartCoroutine(IconPopup(user.Icon, "Icone_Guerreiro"));
                        user.speed -= (float)(user.speed * 0.25);
                        machadodeguerraboost = true;
                    }
                }
                return 0;

            case "Foco":
                {
                    StartCoroutine(IconPopup(user.Icon, "Foco"));
                    StartCoroutine(Foco(user));
                }
                return 0;

            case "Durão":
                {
                    StartCoroutine(IconPopup(user.Icon, "Durão"));
                    user.maxhp += user.maxhp / 4;
                    user.hp += user.hp / 4;
                }
                return 0;

            case "Sabedoria Arcana":
                {
                    StartCoroutine(IconPopup(user.Icon, "Sabedoria Arcana"));
                    user.expmarkplier += (float)0.25;
                }
                return 0;
            case "Lutador Versátil":

                StartCoroutine(IconPopup(user.Icon, "Lutador Versátil"));

                if (user.position == 1 || user.position == 2)
                {
                    user.damagereduction += 3;

                }
                if (user.position == 3)
                {
                    user.Weapon.power += 3;
                }

                return 0;

            case "Técnica Improvisada":


                StartCoroutine(IconPopup(user.Icon, "Técnica Improvisada"));

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

                StartCoroutine(IconPopup(user.Icon, "Icone_Guerreiro"));
                user.damagereduction += 3;
                StartCoroutine(Indestrutivel(user));
                return 0;

            case "Parfrit":

                StartCoroutine(IconPopup(user.Icon, "Icone_Arqueiro"));

                parfritboost = user.dex;
                if (parfritboost >= 23)
                {
                    parfritboost = 23;
                }
                user.power += parfritboost;
                return 0;

            case "Arco do Gigante":

                StartCoroutine(IconPopup(user.Icon, "Icone_Arqueiro"));
                user.maxhp += user.maxhp / 2;
                return 0;

            case "Poção de Foco":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                user.soul += (int)(user.maxsoul * 0.15);

                return 0;

            case "Poção de Força":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                user.power += 5;
                poçãodeforçastacks = 2;
                poçãodeforçause = true;

                return 0;

            case "Poção de Velocidade":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                StartCoroutine(PoçãodeVelocidade(user));
                poçãodevelocidadeuse = true;

                return 0;

            case "Anel Vampírico":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                user.lifesteal += (int) 0.1;
                return 0;

            case "Anel do Pacto Real":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                user.maxhp += user.maxhp / 5;
                user.luck += user.luck / 5;
                user.speed += user.speed / 5;

                return 0;

            case "Língua de Porco":

                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));

                user.str += (int) (user.maxhp * 0.02);

                return 0;

            case "Lâmina Fulgurante":

                StartCoroutine(IconPopup(user.Icon, "Icone_Espadachim"));
                user.def += user.str / 3;
                user.mdef += user.mag / 3;

                return 0;

            case "Maestria da Posição 1":

                if (user.position == 1)
                {
                    StartCoroutine(IconPopup(user.Icon, "Maestria da Posição 1"));
                    user.damagereduction += 3;


                    
                }

                return 0;

            case "Maestria da Posição 2":

                if (user.position == 2)
                {
                    StartCoroutine(IconPopup(user.Icon, "Maestria da Posição 2"));
                    user.avoid += 15;
                    user.hit += 15;


                }

                return 0;

            case "Maestria da Posição 3":

                if (user.position == 3)
                {
                    StartCoroutine(IconPopup(user.Icon, "Maestria da Posição 3"));
                    user.power += 3;
                 


                }

                return 0;

            case "Começo Afortunado":

                StartCoroutine(IconPopup(user.Icon, "Começo Afortunado"));
                user.soul += 15;
                StartCoroutine(ComeçoAfortunado(user));
                

                return 0;

            case "Gênio":

                StartCoroutine(IconPopup(user.Icon, "Gênio"));
                user.expmarkplier += (float) 0.25;

                user.maxhp -= user.maxhp / 5;
                user.str -= user.str / 5;
                user.mag -= user.mag / 5;
                user.dex -= user.dex / 5;
                user.speed -= user.speed / 5;
                user.def -= user.def / 5;
                user.mdef -= user.mdef / 5;
                user.luck -= user.luck / 5;

                return 0;

            case "Azar Contagiante":

                StartCoroutine(IconPopup(user.Icon, "Azar Contagiante"));
                user.luck -= 15;

                enemyTeam[0].luck -= 10;
                enemyTeam[1].luck -= 10;
                enemyTeam[2].luck -= 10;


                return 0;


            case "Ídolo Quebrado":
                StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                user.lifesteal += (int) 0.35;
                user.soulgain *= 0;

                return 0;

            //Skills de preparo
            case "Golpe sujo":
                if (user.position == 1)
                {
                    user.battleManager.PlayerBar += 100;
                }
                else if(user.position == 2)
                {
                    user.battleManager.PlayerBar2 += 100;
                }
                else
                {
                    user.battleManager.PlayerBar3 += 100;
                }

                return 0;

            case "Força de Vontade Aumentada":
                user.damageSoulGain += (float)0.2;
                return 0;

            case "Reforçar Armadura":
                StartCoroutine(ReforçarArmadura(user));
                return 0;
            case "Carregar Alma":
                user.soul += user.maxsoul / 2;
                return 0;
            case "Encantamento Benevolente":
                StartCoroutine(EncantamentoBenevolente(user));
                return 0;
            case "Encantamento Malevolente":
                StartCoroutine(EncantamentoMalevolente(user));
                return 0;
            default: return 0;

        }
    }

    //as almas
    public int SoulProc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {
        switch (SoulName)
        {
           
            case "Golpe Focado":

                StartCoroutine(IconPopup(user.Icon, "Golpe Focado"));

                //if attack = Crit {user.hp += user.power/5}

                return user.power / 2;

            case "Tiro Certeiro":

                StartCoroutine(IconPopup(user.Icon, "Tiro Certeiro"));
                Debug.Log("Tiro Certeirado");
                //sure shot

                return user.power / 2;


            case "Trovoada":

                StartCoroutine(IconPopup(user.Icon, "Trovoada"));
                return (int)(target.maxhp * 0.25) - power;

            case "Disparo de Gelo":

                StartCoroutine(IconPopup(user.Icon, "Disparo de Gelo"));
                StartCoroutine(DisparodeGelo(user, target));

                return 0;

            case "Golpe Atordoante":

                StartCoroutine(IconPopup(user.Icon, "Golpe Atordoante"));

                target.soul -= 30;

                return user.power / 2;

            case "Ataque Inspirador":

                StartCoroutine(IconPopup(user.Icon, "Disparo de Gelo"));
                StartCoroutine(AtaqueInspirador(team));

                return user.power / 4;


            case "Genki Dama":

                StartCoroutine(IconPopup(user.Icon, "Goku"));
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
                    StartCoroutine(IconPopup(user.Icon, "Tecnica Improvisada"));
                    user.hit += 20;
                    user.avoid += 20;
                    TecnicaImprovisadaboost = true;

                }

                if (user.position == 1 & user.hp > user.maxhp / 2 & TecnicaImprovisadaboost)
                {
                    StartCoroutine(IconPopup(user.Icon, "Tecnica Improvisada"));
                    user.hit -= 20;
                    user.avoid -= 20;
                    TecnicaImprovisadaboost = false;

                }

                if (user.position == 3 & user.hp <= user.maxhp * 0.9 & TecnicaImprovisada3)
                {
                    StartCoroutine(IconPopup(user.Icon, "Tecnica Improvisada"));
                    user.power -= 5;
                    user.crit -= 5;
                    TecnicaImprovisada3 = true;
                }
                return 0;

            case "Machado Cortado":

                if (user.hp < user.maxhp * 0.8 & machadocortadoboost == true)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Guerreiro"));
                    user.hit -= 20;
                    user.crit -= 10;
                    machadocortadoboost = false;
                }

                return 0;


            case "Presença Inabalável":

                if (user.hp < user.maxhp * 0.5 & !presençainabalavel)
                {
                    StartCoroutine(IconPopup(user.Icon, "Presença Inabalável"));
                    user.def += user.def / 5;
                    user.mdef += user.mdef / 5;
                    presençainabalavel = true;

                }
                if (user.hp > user.maxhp * 0.5 & presençainabalavel)
                {
                    StartCoroutine(IconPopup(user.Icon, "Presença Inabalável"));
                    user.def -= user.def / 5;
                    user.mdef -= user.mdef / 5;
                    presençainabalavel = false;
                }

                return 0;

            case "Concentração de Feiticeiro":

                if (user.hp < user.maxhp)
                {
                    StartCoroutine(IconPopup(user.Icon, "Concentração de Feiticeiro"));
                    user.mag -= (concentraçãodefeiticeiroboost);

                }
                return 0;

            case "Persistência":
                StartCoroutine(IconPopup(user.Icon, "Persistência"));
                user.speed = speed + ((user.maxhp - user.hp) / 10);

                return 0;

            case "Vendaval":


                if (user.hp <=  user.maxhp * 0.4 && vendavalboost == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Espadachim"));
                    user.avoid += 30;
                    vendavalboost = true;
                }

                if (user.hp >= user.maxhp * 0.4 && vendavalboost == true)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Espadachim"));
                    user.avoid -= 30;
                    vendavalboost = false;
                }
                return 0;

            case "Frigidi":
                if (user.hp <= user.maxhp/2 && frigidiboost == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                    user.def += user.def / 2;
                    user.mdef += user.mdef / 2;
                    frigidistatsdef = user.def += user.def / 2;
                    frigidistatsmdef = user.mdef += user.mdef / 2;

                    frigidiboost = true;
                }
                if (user.hp >= user.maxhp / 2 && frigidiboost == true)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Feiticeiro"));
                    user.def += frigidistatsdef;
                    user.mdef += frigidistatsmdef;
                    frigidiboost = true;
                }
                return 0;


            case "Poção de Vida":
                if (user.hp <= user.maxhp * 0.3 && poçãodevidause == false)
                {
                    StartCoroutine(IconPopup(user.Icon, "Icone_Mistico"));
                    user.hp += (int)(user.maxhp * 0.2);

                    poçãodevidause = true;

                }

                return 0;
            default: return 0;
        } 
    }

    IEnumerator ReforçarArmadura(UnitBehavior user)
    {
        user.def += (int)(def / 5);
        yield return new WaitForSeconds(20);
        user.def -= (int)(def / 5);
    }
    IEnumerator EncantamentoBenevolente(UnitBehavior user)
    {
        user.speed += (int)(speed / 6.66);
        user.hit += 10;
        user.avoid += 10;
        yield return new WaitForSeconds(10);
        user.hit -= 10;
        user.avoid -= 10;
        user.speed -= (int)(speed / 6.66);
    }
    IEnumerator EncantamentoMalevolente(UnitBehavior user)
    {
        user.speed -= (int)(speed / 10);
        user.hit -= 10;
        user.avoid -= 10;
        yield return new WaitForSeconds(20);
        user.hit += 10;
        user.avoid += 10;
        user.speed -= (int)(speed / 10);
    }
    IEnumerator Foco(UnitBehavior user)
    {
        user.hit += 20;
        user.avoid += 20;
        yield return new WaitForSeconds(15);
        user.hit -= 20;
        user.avoid -= 20;
    }

    IEnumerator ComeçoAfortunado(UnitBehavior user)
    {
        user.hit += 12;
        user.avoid += 12;
        yield return new WaitForSeconds(16);
        user.hit -= 12;
        user.avoid -= 12;
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
        if (target.Weapon.damageType == 0)
        {
            Receptaculoamaldiçoadopower = (target.Weapon.power + target.str) / 10;
        }
        else
        {
            Receptaculoamaldiçoadopower = (target.Weapon.power + target.mag) / 10;
        }
        Debug.Log(target.speed + " speed antes");
        Debug.Log(target.power + " power antes");
        target.speed -= target.speed / 10;
        target.Weapon.power -= Receptaculoamaldiçoadopower;

        Debug.Log(target.speed + " speed nerfada");
        Debug.Log(target.power + " power nerfado");


        yield return new WaitForSeconds(15);
        target.speed += Receptaculoamaldiçoadospeed;
        target.Weapon.power += Receptaculoamaldiçoadopower;
        Debug.Log(target.speed + " speed voltando");
        Debug.Log(target.power + " power voltando");
    }
    IEnumerator PoçãodeVelocidade(UnitBehavior user)
    {
        user.speed += 7;
        yield return new WaitForSeconds(10);
        user.speed -= 7;

    }

    //almas que não atacam (geralmente)

    public IEnumerator NaSoulproc(string SoulName, UnitBehavior user, UnitBehavior target, List<UnitBehavior> team, List<UnitBehavior> enemyTeam)
    {

        switch (SoulName)
        {

            case "Poder Oculto":

                StartCoroutine(IconPopup(user.Icon, "Poder Oculto"));

                user.lifesteal += (int) 0.25;
                StartCoroutine(user.battleManager.ExtraAttack(user, target, (float)0.5));
                yield return new WaitForSeconds(1.05f);
                user.lifesteal -= (int) 0.25;
                StartCoroutine(user.battleManager.ExtraAttack(user, target, (float)0.5));
                yield return new WaitForSeconds(1.05f);
                target.soul -= 30;

                break;


            case "Revigoramento":

                StartCoroutine(IconPopup(user.Icon, "Revigoramento"));

                if (user.hp == user.maxhp)
                {
                    user.power += (int)(user.maxhp * 0.15);
                    StartCoroutine(user.battleManager.ExtraAttack(user, target));
                    yield return new WaitForSeconds(1);
                    user.power -= (int)(user.maxhp * 0.15);

                }
                else
                {

                    user.hp += (int)(user.maxhp * 0.3);
                    foreach (string skill in user.skills)
                    {
                            PostHealthChange(skill, user, target, team, enemyTeam);
                    }
                    user.battleManager.HudUpdate();

                }

                break;


                //Golpe poderoso tem q estar que porque o hit precisa voltar para o usuario depois

            case "Golpe Poderoso":

                StartCoroutine(IconPopup(user.Icon, "Golpe Poderoso"));

                Debug.Log("Golpe Poderosado");
                user.hit -= 25;
                Debug.Log(user.power + "poder");
                Debug.Log(user.hit + "hit");
                StartCoroutine(user.battleManager.ExtraAttack(user, target,2));
                user.hit += 25;
                Debug.Log(user.power + "poder");
                Debug.Log(user.hit + "hit");

                break;

                //Mesma coisa do Golpe Poderoso

            case "Rajada de Flechas":

                StartCoroutine(IconPopup(user.Icon, "Rajada de Flechas"));

                StartCoroutine(user.battleManager.ExtraAttack(user, enemyTeam[0], (float)0.6));
                StartCoroutine(user.battleManager.ExtraAttack(user, enemyTeam[1], (float)0.6));
                StartCoroutine(user.battleManager.ExtraAttack(user, enemyTeam[2], (float)0.6));
                break;

            case "Golpe Triplo":

                StartCoroutine(IconPopup(user.Icon, "Golpe Triplo"));

                StartCoroutine(user.battleManager.ExtraAttack(user, target, (float)0.5));
                StartCoroutine(user.battleManager.ExtraAttack(user, target, (float)0.5));
                StartCoroutine(user.battleManager.ExtraAttack(user, target, (float)0.5));

                break;

            case "Fortalecimento":

                StartCoroutine(IconPopup(user.Icon, "Fortalecimento"));

                user.def += (int)(user.def * 0.15);

                break;

            case "Restauração Espiritual":

                StartCoroutine(IconPopup(user.Icon, "Restauração Espiritual"));

                if (team[0].hp <= team[1].hp & team[0].hp <= team[2].hp && team[0].hp > 0)
                {
                    team[0].hp += 10 + user.mag / 5;
                }
                if (team[1].hp <= team[0].hp & team[1].hp <= team[2].hp && team[1].hp > 0)
                {
                    team[1].hp += 10 + user.mag / 5;
                }
                if (team[2].hp <= team[1].hp & team[2].hp <= team[0].hp && team[2].hp > 0)
                {
                    team[2].hp += 10 + user.mag / 5;
                }
                break;

            case "Benção dos Ventos":

                StartCoroutine(IconPopup(user.Icon, "Benção dos Ventos"));

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

    public IEnumerator IconPopup(GameObject IconGOOG, string SkillName)
    {

       GameObject IconGO = Instantiate(IconGOOG, IconGOOG.transform);
        IconGO.transform.localPosition = Vector3.zero;
        IconGO.transform.localScale = Vector3.one;
        SpriteRenderer icon = IconGO.GetComponent<SpriteRenderer>();
        SpriteRenderer square = IconGO.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Vector3 originalPosition = icon.transform.position;
        icon.sprite = Icones.Where(obj => obj.name == SkillName).SingleOrDefault();
        TextMeshProUGUI tmp = IconGO.GetComponent<TextMeshProUGUI>();
        tmp.text = SkillName;
        IconGO.SetActive(true);
        icon.color = new Color(255, 255, 255, 255);
        square.color = new Color(255, 255, 255, 255);
        tmp.color = new Color(255, 255, 255, 255);
        StartCoroutine(FadeOut(icon, tmp, square));

        yield return new WaitForSeconds((float)2);
        IconGO.SetActive(false);
        Destroy(IconGO);
        icon.transform.position = originalPosition;

    }
    public IEnumerator FadeOut(SpriteRenderer sr, TextMeshProUGUI tmp, SpriteRenderer square)
    {
        for (float f = 1f; f >= -0.05; f -= 0.05f)
        {
            sr.transform.position += new Vector3(0,0.07f,0);
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            square.material.color = c;
            tmp.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void GetBaseStats()
    {
      UnitBehavior ub =   GetComponentInParent<UnitBehavior>();
        maxhp = ub.maxhp; hp = ub.hp; str = ub.str; mag = ub.mag; dex = ub.dex;
        def = ub.def; mdef = ub.mdef; defenses = ub.defenses; luck = ub.luck; speed = ub.speed;
        power = ub.Weapon.power + ub.str;
        if(ub.Weapon.damageType == 1)
        {
            power = ub.Weapon.power + ub.mag;
        }
    }
}
