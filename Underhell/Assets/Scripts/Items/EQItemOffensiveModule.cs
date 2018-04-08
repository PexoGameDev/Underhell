using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQItemOffensiveModule {
    // Fields //
    public float hitComboResetDelay = 0.4f;
    public float ThirdAttackDamageMultiplier = 1.5f;

    //MELEE WEAPON
    public int MeleeAttackDamage = 10;
    public float MeleeKnockBackForce = 1f;
    public float meleeAttackSpeedMultiplier = 1f;

    //RANGE WEAPON
    public int RangeDamage = 5;
    public float RangeKnockBackForce = 5f;
    public float ProjectileSpeed = 1f;
    public float ProjectileCooldown = 0.5f;
    public float rangeAttackSpeedMultiplier = 1f;
}

