using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MeleeWeapon {
    #region Variables
    // Fields //
    [SerializeField] int damage = 1;
    // Public Properties //
    public override int Damage { get; set; }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        Damage = damage;
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHPModule>())
            other.GetComponent<EnemyHPModule>().GetHit(damage);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

