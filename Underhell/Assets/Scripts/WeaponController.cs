using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponController : MonoBehaviour {
    #region Variables
    // Fields //

    // Public Properties //
    public static MeleeWeapon MeleeWeapon {get; set;}
    // Private Properties //
    public static Projectile Projectile { get; set; }
    #endregion

    #region Unity Methods
    void Awake()
    {
        Projectile = GetComponentInChildren<Projectile>();
        MeleeWeapon = GetComponentInChildren<MeleeWeapon>();

        Projectile.gameObject.SetActive(false);
        MeleeWeapon.gameObject.SetActive(false);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

