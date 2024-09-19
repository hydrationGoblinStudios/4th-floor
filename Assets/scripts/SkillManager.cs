using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int SkillProc(int skillID)
    {
        switch (skillID)
        {
            case 0:return 0;
            case 1: return 10;
            default: return 0;
        }

    }
}
