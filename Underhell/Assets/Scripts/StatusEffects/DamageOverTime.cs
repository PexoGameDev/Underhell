using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour {
    #region Variables
    // Fields //
    public AttackEffect.DamageSource DamageSource;
    public int Damage;
    public float DamageInterval;
    public float Duration;

    HPModule target;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        //MAKING SURE THAT TARGET IS ONLY "ON ONE FIRE" - NO STACKING ALLOWED
        DamageOverTime other;
        if ((other = gameObject.GetComponent<DamageOverTime>()) && other!=this)
            Destroy(other);

        target = gameObject.GetComponent<HPModule>();
        Destroy(this,Duration);
        InvokeRepeating("DamageTarget", 0f, DamageInterval);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void DamageTarget()
    {
        target.GetHit(Damage, 0f, Vector3.zero, AttackEffect.DamageSource.Fire);
    }
    #endregion
}

