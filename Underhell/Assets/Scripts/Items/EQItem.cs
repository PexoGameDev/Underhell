using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EQ Item", menuName = "EQ Item", order = 5)]
public class EQItem : ScriptableObject {
    #region Variables
    // Fields //
    public string Name;
    public string Description;
    public Sprite Image;

    public float MovementSpeed;
    public float JumpHeight;
    public float JumpForce;
    public float DashDistance;
    public float DashCooldown;
    public float pickingUpSpeedPercentage;
    public float slowPercentageWhenAttacking;

    public float hitComboResetDelay;
    public float ThirdAttackDamageMultiplier;
    public int MeleeAttackDamage;
    public float MeleeKnockBackForce;
    public float meleeAttackSpeedMultiplier;
    public int RangeDamage;
    public float RangeKnockBackForce;
    public float ProjectileSpeed;
    public float ProjectileCooldown;
    public float rangeAttackSpeedMultiplier;
    public AttackEffect[] MeleeAttackEffects;
    public AttackEffect[] RangeAttackEffects;

    public float InvulnerabilityDuration;
    public int MaxHP;
    public int HP;
    public float FireResistance;
    public float PoisonResistance;
    public float SlowResistance;
    public bool SnareResistance;
    public bool ParalyzeResistance;

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public virtual void ApplyEffects(GameObject player)
    {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();
        movementModule.MovementSpeed += MovementSpeed;
        movementModule.JumpHeight += JumpHeight;
        movementModule.JumpForce += JumpForce;
        movementModule.DashDistance += DashDistance;
        movementModule.DashCooldown += DashCooldown;
        movementModule.pickingUpSpeedPercentage += pickingUpSpeedPercentage;
        movementModule.slowPercentageWhenAttacking += slowPercentageWhenAttacking;

            PlayerAttackModule attackModule = player.GetComponent<PlayerAttackModule>();
            attackModule.hitComboResetDelay += hitComboResetDelay;
            attackModule.ThirdAttackDamageMultiplier += ThirdAttackDamageMultiplier;
            attackModule.MeleeAttackDamage += MeleeAttackDamage;
            attackModule.MeleeKnockBackForce += MeleeKnockBackForce;
            attackModule.meleeAttackSpeedMultiplier += meleeAttackSpeedMultiplier;
            attackModule.RangeDamage += RangeDamage;
            attackModule.RangeKnockBackForce += RangeKnockBackForce;
            attackModule.ProjectileSpeed += ProjectileSpeed;
            attackModule.ProjectileCooldown += ProjectileCooldown;
            attackModule.rangeAttackSpeedMultiplier += rangeAttackSpeedMultiplier;

            for (int i = 0; i < MeleeAttackEffects.Length; i++)
                attackModule.MeleeAttackEffects.Add(MeleeAttackEffects[i]);

            for (int i = 0; i < RangeAttackEffects.Length; i++)
                attackModule.RangeAttackEffects.Add(RangeAttackEffects[i]);

            HPModule hPModule = player.GetComponent<HPModule>();
            hPModule.InvulnerabilityDuration += InvulnerabilityDuration;
            hPModule.MaxHP += MaxHP;
            hPModule.HP += HP;
            hPModule.PoisonResistance += FireResistance;
            hPModule.FireResistance += FireResistance;
            hPModule.SlowResistance += SlowResistance;
            hPModule.SnareResistance = (SnareResistance) ? true : hPModule.SnareResistance;
            hPModule.ParalyzeResistance = (ParalyzeResistance) ? true : hPModule.ParalyzeResistance;
    }

    public virtual void RevertEffects(GameObject player)
    {
            PlayerMovement movementModule = player.GetComponent<PlayerMovement>();
            movementModule.MovementSpeed -= MovementSpeed;
            movementModule.JumpHeight -= JumpHeight;
            movementModule.JumpForce -= JumpForce;
            movementModule.DashDistance -= DashDistance;
            movementModule.DashCooldown -= DashCooldown;
            movementModule.pickingUpSpeedPercentage -= pickingUpSpeedPercentage;
            movementModule.slowPercentageWhenAttacking -= slowPercentageWhenAttacking;


            PlayerAttackModule attackModule = player.GetComponent<PlayerAttackModule>();
            attackModule.hitComboResetDelay -= hitComboResetDelay;
            attackModule.ThirdAttackDamageMultiplier -= ThirdAttackDamageMultiplier;
            attackModule.MeleeAttackDamage -= MeleeAttackDamage;
            attackModule.MeleeKnockBackForce -= MeleeKnockBackForce;
            attackModule.meleeAttackSpeedMultiplier -= meleeAttackSpeedMultiplier;
            attackModule.RangeDamage -= RangeDamage;
            attackModule.RangeKnockBackForce -= RangeKnockBackForce;
            attackModule.ProjectileSpeed -= ProjectileSpeed;
            attackModule.ProjectileCooldown -= ProjectileCooldown;
            attackModule.rangeAttackSpeedMultiplier -= rangeAttackSpeedMultiplier;

            for (int i = 0; i < MeleeAttackEffects.Length; i++)
                attackModule.MeleeAttackEffects.Remove(MeleeAttackEffects[i]);

            for (int i = 0; i < RangeAttackEffects.Length; i++)
                attackModule.RangeAttackEffects.Remove(RangeAttackEffects[i]);


            HPModule hPModule = player.GetComponent<HPModule>();
            hPModule.InvulnerabilityDuration -= InvulnerabilityDuration;
            hPModule.MaxHP -= MaxHP;
            hPModule.PoisonResistance -= FireResistance;
            hPModule.FireResistance -= FireResistance;
            hPModule.SlowResistance -= SlowResistance;
            hPModule.SnareResistance = (SnareResistance) ? false : hPModule.SnareResistance; // POSIBBLE BUG IF OTHER ITEM GIVES IMMUNITY
            hPModule.ParalyzeResistance = (ParalyzeResistance) ? false : hPModule.ParalyzeResistance; // POSIBBLE BUG IF OTHER ITEM GIVES IMMUNITY
    }

    #endregion

    #region Private Methods
    #endregion
}

