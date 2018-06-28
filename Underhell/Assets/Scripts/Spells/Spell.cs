using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {
    #region Variables
    // Fields //
    [Header("Spell")]
    public Vector3 target;
    public CastingType castingType;

    // Public Properties //

    // Private Properties //

    // Public Data Structures //
    public enum CastingType
    {
        Channel = 0,
        Instant = 1,
        Casting = 2
    }
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

