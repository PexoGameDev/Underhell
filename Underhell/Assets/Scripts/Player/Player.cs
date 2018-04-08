using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttackModule))]
[RequireComponent(typeof(EquipmentManager))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HPModule))]
public class Player : MonoBehaviour {
    #region Variables
    // Fields //
    private static GameObject player;
    // Public Properties //
    public static GameObject Entity
    {
        get { return player; }
        set { player = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake () {
        player = gameObject;
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

