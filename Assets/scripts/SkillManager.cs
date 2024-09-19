using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private bool espadaCurtaBoost = false;
        public int SkillProc(string skillName,UnitBehavior user, UnitBehavior target)
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
            case "":
                return 0;
            default: return 0;

        }

    }
}
