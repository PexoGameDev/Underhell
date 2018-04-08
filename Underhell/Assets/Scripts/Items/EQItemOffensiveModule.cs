using UnityEngine;
public class EQItemOffensiveModule : MonoBehaviour {
    // Fields //
    public float hitComboResetDelay = 0f;
    public float ThirdAttackDamageMultiplier = 0f;

    //MELEE WEAPON
    public int MeleeAttackDamage = 0;
    public float MeleeKnockBackForce = 0f;
    public float meleeAttackSpeedMultiplier = 0f;
    public AttackEffect[] MeleeAttackEffects;

    //RANGE WEAPON
    public int RangeDamage = 0;
    public float RangeKnockBackForce = 0f;
    public float ProjectileSpeed = 0f;
    public float ProjectileCooldown = 0f;
    public float rangeAttackSpeedMultiplier = 0f;
    public AttackEffect[] RangeAttackEffects;
}

