using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {
    #region Variables
    // Fields //
    [Header("Spell")]
    public Vector3 target;
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
    public abstract bool Cast(Vector3 target);

    #endregion

    #region Private Methods
    #endregion
}

