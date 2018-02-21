using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackingModule))]
[RequireComponent(typeof(HPModule))]

public class Enemy : MonoBehaviour {
    #region Variables
    // Fields //
    private EnemyAttackingModule attackingModule;
    private HPModule hpModule;

    [SerializeField] private int scoreValue = 10;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public void Die()
    {
        PersistentData.Score += scoreValue;
        //add to objectpool
        //instead of destroying
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    #endregion
}

