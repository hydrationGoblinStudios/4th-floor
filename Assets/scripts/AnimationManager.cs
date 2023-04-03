using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public AnimatorController[] Animations;
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
        switch (battleManager.playerBehavior.GetType().Name)
        {
            case "Prisioneiro":
                Panimator.runtimeAnimatorController = Animations[4];
                break;

            case "Espadachim":
                Panimator.runtimeAnimatorController = Animations[0];
                break;

            case "Guerreiro":
                Panimator.runtimeAnimatorController = Animations[1];
                break;

            case "Soldado":
                Panimator.runtimeAnimatorController = Animations[2];
                break;

            case "Arqueiro":
                Panimator.runtimeAnimatorController = Animations[3];
                break;
        }
    }
    void Update()
    {
       if(battleManager.state == BattleManager.BattleState.PlayerTurn & wait)
        {
            Panimator.SetTrigger("Pattack");
            wait = false; 
        }
        if (battleManager.state == BattleManager.BattleState.Wait)
        {
            wait = true;
        }
    }
}
