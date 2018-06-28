using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ChanneledSpell {
    #region Variables
    // Fields //
    [Header("Fireball")]
    public GameObject FireballPrefab;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public override bool Cast(Vector3 target)
    {
        Instantiate(FireballPrefab, Player.Entity.transform.position + Vector3.up*Player.Entity.transform.localScale.y*0.5f, Quaternion.identity).GetComponent<FireballEffect>().target = target;
        return true;
    }
    #endregion

    #region Private Methods
    #endregion
}

