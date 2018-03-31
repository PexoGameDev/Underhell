using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackEffect : MonoBehaviour {
    #region Variables
    // Fields //

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
    public abstract void ApplyEffect(HPModule target);
    #endregion

    #region Private Methods
    #endregion
}

