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

    void Update() {

    }
    #endregion

    #region Public Methods
    public virtual void ApplyEffects(GameObject player)
    {
        if (utility != null)
        {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();

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

        }
    }
    public virtual void RevertEffects(GameObject player)
    {
        if (utility != null)
        {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();

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

        }
    }

    #endregion

    #region Private Methods
    #endregion
}

