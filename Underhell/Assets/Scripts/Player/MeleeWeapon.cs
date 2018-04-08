using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeWeapon : MonoBehaviour {
    #region Variables
    // Fields //
    PlayerAttackModule playerAttackModule;
    // Public Properties //
    // Private Properties //
    #endregion
    void Awake()
    {
        playerAttackModule = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackModule>();
    }
    #region Unity Methods
    void OnTriggerEnter(Collider other)
    {
        HPModule target;
        if (target = other.GetComponent<HPModule>())
        {
            switch (playerAttackModule.HitCombo)
            {
                case 2:
                    target.GetHit((int)(playerAttackModule.MeleeAttackDamage * playerAttackModule.ThirdAttackDamageMultiplier), playerAttackModule.MeleeKnockBackForce, transform.position, AttackEffect.DamageSource.Physical);
                    break;

                default:
                    target.GetHit(playerAttackModule.MeleeAttackDamage, playerAttackModule.MeleeKnockBackForce * 0.5f, transform.position, AttackEffect.DamageSource.Physical);
                    break;
            }
            for (int i = 0; i < playerAttackModule.MeleeAttackEffects.Count; i++)
                playerAttackModule.MeleeAttackEffects[i].ApplyEffect(target);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

