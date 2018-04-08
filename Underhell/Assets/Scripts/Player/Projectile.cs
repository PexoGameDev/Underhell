using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    #region Variables
    // Fields //
    PlayerAttackModule playerAttackModule;

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        Destroy(gameObject, 1f);
        playerAttackModule = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackModule>();
    }
	
	void FixedUpdate () {
        transform.position += Vector3.right * playerAttackModule.ProjectileSpeed;
	}

    void OnTriggerEnter(Collider other)
    {
        HPModule target;
        if (target = other.GetComponent<HPModule>())
        {
            switch (playerAttackModule.HitCombo)
            {
                case 2:
                    target.GetHit((int)(playerAttackModule.RangeDamage * playerAttackModule.ThirdAttackDamageMultiplier), playerAttackModule.RangeKnockBackForce, transform.position, AttackEffect.DamageSource.Physical);
                    break;

                default:
                    target.GetHit(playerAttackModule.RangeDamage, playerAttackModule.RangeKnockBackForce, transform.position, AttackEffect.DamageSource.Physical);
                    break;
            }

            for (int i = 0; i < playerAttackModule.RangeAttackEffects.Count; i++)
                playerAttackModule.RangeAttackEffects[i].ApplyEffect(target);
        }
        Destroy(gameObject);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

