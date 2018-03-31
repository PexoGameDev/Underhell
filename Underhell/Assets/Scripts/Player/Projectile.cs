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
            target.GetHit(playerAttackModule.RangeDamage, playerAttackModule.RangeKnockBackForce, transform.position);
            foreach (AttackEffect ae in playerAttackModule.DistanceAttackEffects)
                ae.ApplyEffect(target);
        }
        Destroy(gameObject);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

