using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Effect", menuName = "AttackEffect", order = 3)]
public class AttackEffect : ScriptableObject {
    #region Variables
    // Fields //
    public int Damage;
    public float DamageInterval;
    public float Duration;
    public DamageSource damageSource;
    // Public Properties //

    // Private Properties //

    // Public Data Structures //
    public enum DamageSource
    {
        Poison, Fire, Physical
    }
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public void ApplyEffect(HPModule target)
    {
        DamageOverTime myDamage = target.gameObject.AddComponent<DamageOverTime>();
        myDamage.DamageSource = damageSource;
        myDamage.Damage = Damage;
        myDamage.Duration = Duration;
        myDamage.DamageInterval = DamageInterval;
    }
    #endregion

    #region Private Methods
    #endregion
}

