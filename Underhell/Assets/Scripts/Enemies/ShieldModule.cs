using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private GameObject shieldGraphic;
    private GameObject actualShield;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        actualShield = Instantiate(shieldGraphic, transform);
    }
    private void OnDisable()
    {
        Destroy(actualShield);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

