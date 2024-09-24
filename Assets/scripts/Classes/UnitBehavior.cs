using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental;

public class UnitBehavior : MonoBehaviour
{
    public int classId;
    public Item Weapon;
    public Item Accesory;
    public UnitBehavior playerBehavior;
    public UnitBehavior enemyBehavior;
    public BattleManager battleManager;
    public bool enemy;
    public GameObject battleManagerOBJ;
    [Header("Parameters")]
    public string UnitName;
    public int currentLevel;
    public int expmarkplier = 1;
    public int currentRank;
    public int currentExp;
    public int[] ClassID;
    public int[] ClassLevel;
    public int hit;
    public int avoid;
    public int crit;
    [Header("Stats")]
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
    public List<string> skills;
    public List<string> skillInventory;
    public string equipedSoul;
    public List<string> soulInventory;
    public int soul;
    public int maxsoul;
    public int soulgain;
    public int cooking;
    public bool Pendure;
    public bool Eendure;
    public TextMeshPro battleText;
    public GameObject battleTextObj;
    public List<int> growths;
    public string baseSkill;
    public string skill1;
    public string skill2;
    public string skill3;
    public string baseSoul;
    public string soul1;
    public string soul2;
    public string soul3;
    public string description = "classe n?o valida";
    void Start()
    {   //lembrar de manter o objeto de player acima do objeto do inimigo
         playerBehavior = GetComponent<UnitBehavior>();
        enemyBehavior = GetComponent<UnitBehavior>();
        Weapon.holder = this;
        battleTextObj = GameObject.FindGameObjectWithTag("Battle Text");
        battleText = battleTextObj.GetComponent<TextMeshPro>();
        battleManagerOBJ = GameObject.FindGameObjectWithTag("Battle Manager");
        Pendure = false;
        Eendure = false;
        defenses = new int[2]; defenses[0] = def;defenses[1] = mdef;
        if (battleManagerOBJ != null)
        {
            battleManager = battleManagerOBJ.GetComponent<BattleManager>();
        }
        if (enemy)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
    }
    public virtual int Soul(int damage)
    {
        Debug.Log("Not Valid Class");
        return 0;
    }
    public virtual int Proc(int damage)
    {
        Debug.Log("skill proc");
        return 0;
    }
}
