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
            switch(playerAttackModule.HitCombo)
            {
                case 2:
                    target.GetHit((int)(playerAttackModule.MeleeAttackDamage * 1.5f), playerAttackModule.MeleeKnockBackForce, transform.position);
                    break;

                default:
                    target.GetHit(playerAttackModule.MeleeAttackDamage, playerAttackModule.MeleeKnockBackForce * 0, transform.position);
                    break;
            }
            foreach (AttackEffect ae in playerAttackModule.MeleeAttackEffects)
                ae.ApplyEffect(target);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

