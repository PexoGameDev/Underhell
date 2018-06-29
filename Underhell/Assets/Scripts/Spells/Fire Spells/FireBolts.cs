using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolts : ChanneledSpell {
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
    public override void Cast(Vector3 target)
    {
        Instantiate(FireballPrefab, Player.SpellsOrigin.position, Quaternion.identity).GetComponent<FireballEffect>().target = target;
    }

    public override void ChannelEnd(Vector3 target)
    {
    }
    #endregion

    #region Private Methods
    #endregion
}

