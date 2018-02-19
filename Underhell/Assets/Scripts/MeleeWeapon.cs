using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class MeleeWeapon : MonoBehaviour {
    #region Variables
    // Fields //

    // Public Properties //
    public abstract int Damage { get; set; }
    public abstract GameObject Prefab { get; set; }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public abstract IEnumerator Attack(int isLookingRight, Transform parent);
    #endregion

    #region Private Methods
    #endregion
}

