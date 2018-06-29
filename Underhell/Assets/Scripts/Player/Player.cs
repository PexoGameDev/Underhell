using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Variables
    // Fields //
    private static GameObject player;
    // Public Properties //
    public static GameObject Entity
    {
        get { return player; }
        private set { player = value; }  
    }

    [SerializeField] private Transform spellsOrigin;
    // Public Properties //
    public static Transform SpellsOrigin
    {
        get; private set;
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake () {
        player = gameObject;
        SpellsOrigin = spellsOrigin;
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

