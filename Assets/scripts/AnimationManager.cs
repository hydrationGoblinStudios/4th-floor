using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public RuntimeAnimatorController[] Animations;
    public Animator Panimator;
    public Animator Eanimator;
    public GameObject PanimatorOBJ;
    public GameObject EanimatorOBJ;
    public BattleManager battleManager;

    public bool wait = true;
    void Start()
    {
        PanimatorOBJ = GameObject.FindGameObjectWithTag("Psprite");
        EanimatorOBJ = GameObject.FindGameObjectWithTag("Esprite");
        Panimator = PanimatorOBJ.GetComponent<Animator>();
        Eanimator = EanimatorOBJ.GetComponent<Animator>();
        Panimator.runtimeAnimatorController = battleManager.playerBehavior.GetType().Name switch
        {
            "Prisioneiro" => Animations[4],
            "Espadachim" => Animations[0],
            "Guerreiro" => Animations[1],
            "Soldado" => Animations[2],
            "Arqueiro" => Animations[3],
            _ => Animations[0],
        };
        Eanimator.runtimeAnimatorController = battleManager.enemyBehavior.GetType().Name switch
        {
            "Espadachim" => Animations[5],
            "Guerreiro" => Animations[6],
            "Soldado" => Animations[7],
            "Arqueiro" => Animations[8],
            _ => Animations[5],
        };
    }
}
