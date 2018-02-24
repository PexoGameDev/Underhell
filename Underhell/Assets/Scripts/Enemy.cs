using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackingModule))]
[RequireComponent(typeof(HPModule))]
[RequireComponent(typeof(MovementModule))]
public class Enemy : MonoBehaviour {
    #region Variables
    // Fields //
    private HPModule hpModule;
    private MovementModule movementModule;
    private EnemyAttackingModule attackingModule;

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

