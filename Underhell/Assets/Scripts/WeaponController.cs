using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponController : MonoBehaviour {
    #region Variables
    // Fields //

    // Public Properties //
    public static MeleeWeapon[] MeleeWeapons {get; set;}
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake()
    {
        MeleeWeapons = GetComponentsInChildren<MeleeWeapon>();
        foreach (MeleeWeapon mw in MeleeWeapons)
            mw.gameObject.SetActive(false);
    }
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

