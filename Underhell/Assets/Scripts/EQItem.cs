using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EQItem : MonoBehaviour {
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
    public abstract void ApplyEffects(GameObject player);
    public abstract void RevertEffects(GameObject player);

    #endregion

    #region Private Methods
    #endregion
}

