using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class GameSettings : MonoBehaviour
{
    //[Header("Shield Stats")]
  
    //public float MaxHp;
    //public float RegenHP;
    //public float RegenBonusHP;


    //[Header("Mana Stats")]
    //public float MaxMana;
    //[Header("Defense Stats")]
    //public float DamageReduction;
    //public float DamageReductionBonus;


    //[Header("Recovery Stats")]
    //public float RegenMana;
    //public float RegenBonusMana;

    [Serializable]
    public class ShieldStatsKey
    {

        [Header("Shield Stats")]

        public float MaxHp;
        public float RegenHP;
        public float RegenBonusHP;
        public float RegenPercentHP;
        public float HealingBonusHP;
        public float DamageReduction;
        public float DamageReductionBonus;


        [Header("Resistances")]
        public float ResistanceAll;
        public float ResistCurse;
        public float ResistMute;
        public float ResistStun;
        public float ResistPoison;
        public float ResistPetrify;
        public float ResistSlow;
        public float ResistCharm;
        public float ResistControl;




        [Header("Elemental Shield")]
        public bool ShieldElementFire;
        public bool ShieldElementWater;
        public bool ShieldElementEarth;
        public bool ShieldElementAir;

        public bool ShieldElementDeath;
        public bool ShieldElementLife;
        public bool ShieldElementChaos;
        public bool ShieldElementLaw;

        public float ElementDefenseRating;
        public float ElementDefenseRatingBonus;


        [Header("Triggered Effects")]
        public bool CanReflectMelee;
        public float ReflectChanceMelee;
        public float ReflectAmountMelee;

        public bool CanReflectRanged;
        public float ReflectChanceRanged;
        public float ReflectAmountRanged;

    }

    [Serializable]
    public class ShieldStatsValue
    {

        [Header("Shield Stats")]

        public float MaxHp;
        public float RegenHP;
        public float RegenBonusHP;
        public float RegenPercentHP;
        public float HealingBonusHP;
        public float DamageReduction;
        public float DamageReductionBonus;


        [Header("Resistances")]
        public float ResistanceAll;
        public float ResistCurse;
        public float ResistMute;
        public float ResistStun;
        public float ResistPoison;
        public float ResistPetrify;
        public float ResistSlow;
        public float ResistCharm;
        public float ResistControl;




        [Header("Elemental Shield")]
        public bool ShieldElementFire;
        public bool ShieldElementWater;
        public bool ShieldElementEarth;
        public bool ShieldElementAir;

        public bool ShieldElementDeath;
        public bool ShieldElementLife;
        public bool ShieldElementChaos;
        public bool ShieldElementLaw;

        public float ElementDefenseRating;
        public float ElementDefenseRatingBonus;


        [Header("Triggered Effects")]
        public bool CanReflectMelee;
        public float ReflectChanceMelee;
        public float ReflectAmountMelee;

        public bool CanReflectRanged;
        public float ReflectChanceRanged;
        public float ReflectAmountRanged;

    }


    [Serializable]
    public class DamageStats
    {

    }

    [Serializable]
    public class ManaStats
    {
        public float MaxMana;
        public float RegenMana;
        public float RegenBonusMana;
        public float RegenPercentMana;
        public float ManaCost;


        public bool CanFreeSpell;
        public bool ChanceFreeSpell;

        [Header("Mana Steal")]
        public float ManaSteal;
        public float ManaStealBonus;
        public float ManaStealRate;
        public float ManaStealProtection;



    }

    public ShieldStats ShieldAttributes = new ShieldStats();

    public ShieldStats listShield;
    

    public DamageStats DamageAttributes = new DamageStats();
    public ManaStats ManaAttributes = new ManaStats();



    //[Serializable]
    public Dictionary<ShieldStats, float> statTest;
    protected virtual void Start()
    {
        listShield[] = ShieldAttributes;

        statTest = new Dictionary<ShieldStats, float>;

        statTest.Add(ShieldAttributes, ShieldAttributes);



        };
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
