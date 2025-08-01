using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitBehavior : MonoBehaviour
{
    public int classId;
    public Item Weapon;
    public List<Item.Weapontype> UsableWeaponTypes;
    public Item Accesory;
    public BattleManager battleManager;
    public bool enemy;
    public GameObject battleManagerOBJ;
    public string activity;
    public int fortalecerStat = 0;
    public int currentLearnigClassID;
    [Header("Parameters")]
    public string UnitName;
    public int currentLevel;
    public float expmarkplier = 1;
    public int currentRank;
    public int currentExp;
    public List<int> ClassID;
    public int[] ClassLevel;
    public Dictionary<int, int> ClassLearning = new();
    public SeriazableDictionary ClassLearningSerializable;
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
    [Header("Sistema de skill")]
    public List<string> skills;
    public List<string> skillInventory;
    public string equipedSoul;
    public bool equippedSoulIsAttack = true;
    public List<string> soulInventory;
    public float soul;
    public int maxsoul;
    public float soulgain;
    public float damageSoulGain =1;
    [Header("Status que não aparecem na UI")]
    public int damagereduction;
    public int lifesteal;
    public int armorpen;
    public int magicpen;
    [Header("Cooking")]
    public int cooking;
    public int position;
    [HideInInspector]
    public bool Pendure;
    public Animator animator;
    [HideInInspector]
    public bool Eendure;
    public TextMeshPro battleText;
    public GameObject battleTextObj;
    public List<int> growths;
    [Header("Learn Set")]
    public string classSkill;
    public string personalSkill;
    public string baseSkill;
    public string skill1;
    public string skill2;
    public string skill3;
    public string baseSoul;
    public string soul1;
    public string soul2;
    public string soul3;
    public string description = "classe não valida";
    public GameObject Icon;
    public TextMeshProUGUI damageTMP;

    public Vector3 startingPosition;

    public SkillManager SkillManager;
    public UnitData unitData;

    public string target;
    public int targetStat;
    void Start()
    {   //lembrar de manter o objeto de player acima do objeto do inimigo
        if(Weapon != null)
        {
        Weapon.holder = this;
        }
        
        battleManagerOBJ = GameObject.FindGameObjectWithTag("Battle Manager");
        Pendure = false;
        Eendure = false;
        defenses = new int[2]; defenses[0] = def; defenses[1] = mdef;
        if (battleManagerOBJ != null)
        {
            battleManager = battleManagerOBJ.GetComponent<BattleManager>();
        }
        if (enemy)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        ClassLearning = ClassLearningSerializable.ToDictionary();
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
    [Serializable]
    public class SeriazableDictionary
    {
        [SerializeField]
        SeriazableDictionaryItem[] thisSeriazableDictionaryItem;
        [SerializeField]
        public Dictionary<int,int> ToDictionary()
        {
            Dictionary<int,int> dict = new Dictionary<int,int>();

            foreach(var item in thisSeriazableDictionaryItem)
            {
                dict.Add(item.key, item.value);
            }
            return dict;
        }
    }
    [Serializable]
    public class SeriazableDictionaryItem
    {
        [SerializeField]
        public int key;
        [SerializeField]
        public int value;
    }

}
