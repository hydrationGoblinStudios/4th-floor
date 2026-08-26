using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public List<RuntimeAnimatorController> Animations;
    public Animator[] animators;
    public BattleManager battleManager;

    public bool wait = true;
    void Start()
    {
       SetClass();
    }
    public void SetClass()
    {
        DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Animations");
        DirectoryInfo[] subDirInfo = dirInfo.GetDirectories();

        foreach (DirectoryInfo subDireInf in subDirInfo)
        {
            FileInfo[] fileinf = subDireInf.GetFiles("*.controller");
            string tempFi = "";
            foreach (FileInfo fi in fileinf)
            {
                Animations.Add(Resources.Load<RuntimeAnimatorController>($"Animations/{fi.Directory.Name}/{fi.Name.Replace(".controller", "")}"));
                DirectoryInfo weapondirInfo = new DirectoryInfo($"Assets/Resources/Animations/{fi.Directory.Name}");
                tempFi = fi.Directory.Name;
            }
            DirectoryInfo[] weaponSubDirInfo = subDireInf.GetDirectories();
            foreach (DirectoryInfo wfdi in weaponSubDirInfo)
            {
                FileInfo[] weaponfileinf = wfdi.GetFiles("*.controller");
                foreach (FileInfo wfi in weaponfileinf)
                {
                    Animations.Add(Resources.Load<RuntimeAnimatorController>($"Animations/{wfi.Directory.Parent.Name}/{wfi.Directory.Name}/{wfi.Name.Replace(".controller", "")}"));
                }
            }
        }
        battleManager.playerTeam[0].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.playerTeam[0].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.playerTeam[0].GetComponent<UnitBehavior>())).SingleOrDefault();
        battleManager.playerTeam[1].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.playerTeam[1].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.playerTeam[1].GetComponent<UnitBehavior>())).SingleOrDefault();
        battleManager.playerTeam[2].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.playerTeam[2].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.playerTeam[2].GetComponent<UnitBehavior>())).SingleOrDefault();
        battleManager.enemyTeam[0].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.enemyTeam[0].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.enemyTeam[0].GetComponent<UnitBehavior>())).SingleOrDefault();
        battleManager.enemyTeam[1].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.enemyTeam[1].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.enemyTeam[1].GetComponent<UnitBehavior>())).SingleOrDefault();
        battleManager.enemyTeam[2].animator.runtimeAnimatorController = Animations.Where(obj => obj.name == battleManager.enemyTeam[2].GetComponent<UnitBehavior>().classId.ToString() + WeaponSelect(battleManager.enemyTeam[2].GetComponent<UnitBehavior>())).SingleOrDefault();
    }
    public string WeaponSelect(UnitBehavior ub)
    {
        string weaponType = "";
        if (ub.GetComponent<UnitBehavior>().UsableWeaponTypes.Count > 1)
        {
            switch (ub.GetComponent<UnitBehavior>().Weapon.weapontype)
            {
                case Item.Weapontype.Sword: weaponType = "Sword"; break;
                case Item.Weapontype.Axe: weaponType = "Axe"; break;
                case Item.Weapontype.Lance: weaponType = "Lance"; break;
                case Item.Weapontype.Bow: weaponType = "Bow"; break;
                case Item.Weapontype.Tome: weaponType = "Tome"; break;
                case Item.Weapontype.Receptacle: weaponType = "Receptacle"; break;
                default: break;
            }
        }
        return weaponType;
    }
}
