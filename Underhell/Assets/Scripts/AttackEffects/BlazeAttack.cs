using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeAttack : AttackEffect{
    #region Variables
    // Fields //
    [SerializeField] private int damage;
    [SerializeField] private float damageInterval;
    [SerializeField] private float duration;

    // Public Properties //
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float DamageInterval
    {
        get { return damageInterval; }
        set { damageInterval = value; }
    }

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public override void ApplyEffect(HPModule target)
    {
        OnFire myFire = target.gameObject.AddComponent<OnFire>();
        myFire.Damage = Damage;
        myFire.Duration = Duration;
        myFire.DamageInterval = DamageInterval;
    }

    #endregion

    #region Private Methods
    #endregion
}

