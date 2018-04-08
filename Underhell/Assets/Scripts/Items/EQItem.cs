using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQItem : MonoBehaviour {
    #region Variables
    // Fields //
    private EQItemUtilityModule utility;
    private EQItemDefensiveModule defensive;
    private EQItemOffensiveModule offensive;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start() {
        utility = GetComponent<EQItemUtilityModule>();
        defensive = GetComponent<EQItemDefensiveModule>();
        offensive = GetComponent<EQItemOffensiveModule>();
    }
    #endregion

    #region Public Methods
    public virtual void ApplyEffects(GameObject player)
    {
        if (utility != null)
        {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();
            movementModule.MovementSpeed += utility.movementSpeed;
            movementModule.JumpHeight += utility.jumpHeight;
            movementModule.JumpForce += utility.jumpForce;
            movementModule.DashDistance += utility.dashDistance;
            movementModule.DashCooldown += utility.dashCooldown;
            movementModule.pickingUpSpeedPercentage += utility.pickingUpSpeedPercentage;
            movementModule.slowPercentageWhenAttacking += utility.slowPercentageWhenAttacking;
        }

        if (offensive != null)
        {
            PlayerAttackModule attackModule = player.GetComponent<PlayerAttackModule>();
            attackModule.hitComboResetDelay += offensive.hitComboResetDelay;
            attackModule.ThirdAttackDamageMultiplier += offensive.ThirdAttackDamageMultiplier;
            attackModule.MeleeAttackDamage += offensive.MeleeAttackDamage;
            attackModule.MeleeKnockBackForce += offensive.MeleeKnockBackForce;
            attackModule.meleeAttackSpeedMultiplier += offensive.meleeAttackSpeedMultiplier;
            attackModule.RangeDamage += offensive.RangeDamage;
            attackModule.RangeKnockBackForce += offensive.RangeKnockBackForce;
            attackModule.ProjectileSpeed += offensive.ProjectileSpeed;
            attackModule.ProjectileCooldown += offensive.ProjectileCooldown;
            attackModule.rangeAttackSpeedMultiplier += offensive.rangeAttackSpeedMultiplier;
        }

        if (defensive != null)
        {
            HPModule hPModule = player.GetComponent<HPModule>();
            hPModule.InvulnerabilityDuration += defensive.InvulnerabilityDuration;
            hPModule.MaxHP += defensive.MaxHP;
            hPModule.HP += defensive.hP;
            hPModule.PoisonResistance += defensive.FireResistance;
            hPModule.FireResistance += defensive.FireResistance;
            hPModule.SlowResistance += defensive.SlowResistance;
            hPModule.SnareResistance = (defensive.SnareResistance) ? true : hPModule.SnareResistance;
            hPModule.ParalyzeResistance = (defensive.ParalyzeResistance) ? true : hPModule.ParalyzeResistance;
        }
    }

    public virtual void RevertEffects(GameObject player)
    {
        if (utility != null)
        {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();
            movementModule.MovementSpeed -= utility.movementSpeed;
            movementModule.JumpHeight -= utility.jumpHeight;
            movementModule.JumpForce -= utility.jumpForce;
            movementModule.DashDistance -= utility.dashDistance;
            movementModule.DashCooldown -= utility.dashCooldown;
            movementModule.pickingUpSpeedPercentage -= utility.pickingUpSpeedPercentage;
            movementModule.slowPercentageWhenAttacking -= utility.slowPercentageWhenAttacking;
        }

        if (offensive != null)
        {
            PlayerAttackModule attackModule = player.GetComponent<PlayerAttackModule>();
            attackModule.hitComboResetDelay -= offensive.hitComboResetDelay;
            attackModule.ThirdAttackDamageMultiplier -= offensive.ThirdAttackDamageMultiplier;
            attackModule.MeleeAttackDamage -= offensive.MeleeAttackDamage;
            attackModule.MeleeKnockBackForce -= offensive.MeleeKnockBackForce;
            attackModule.meleeAttackSpeedMultiplier -= offensive.meleeAttackSpeedMultiplier;
            attackModule.RangeDamage -= offensive.RangeDamage;
            attackModule.RangeKnockBackForce -= offensive.RangeKnockBackForce;
            attackModule.ProjectileSpeed -= offensive.ProjectileSpeed;
            attackModule.ProjectileCooldown -= offensive.ProjectileCooldown;
            attackModule.rangeAttackSpeedMultiplier -= offensive.rangeAttackSpeedMultiplier;
        }

        if (defensive != null)
        {
            HPModule hPModule = player.GetComponent<HPModule>();
            hPModule.InvulnerabilityDuration -= defensive.InvulnerabilityDuration;
            hPModule.MaxHP -= defensive.MaxHP;
            hPModule.PoisonResistance -= defensive.FireResistance;
            hPModule.FireResistance -= defensive.FireResistance;
            hPModule.SlowResistance -= defensive.SlowResistance;
            hPModule.SnareResistance = (defensive.SnareResistance) ? false : hPModule.SnareResistance; // POSIBBLE BUG IF OTHER ITEM GIVES IMMUNITY
            hPModule.ParalyzeResistance = (defensive.ParalyzeResistance) ? false : hPModule.ParalyzeResistance; // POSIBBLE BUG IF OTHER ITEM GIVES IMMUNITY
        }
    }

    #endregion

    #region Private Methods
    #endregion
}

